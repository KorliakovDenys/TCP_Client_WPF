using System;
using System.Buffers.Binary;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace NP_PW_4_Client_WPF;

public class TCPClientController{
    private TcpClient _tcpClient;

    private Channel<string> _channel;

    private NetworkStream _networkStream;

    private Socket _client;

    private Task _readingTask;

    private Task _writingTask;

    private bool disposed;

    public delegate void ConnectionHandler();

    public delegate void MessageHandler(string message);

    public event ConnectionHandler Connected;

    public event ConnectionHandler Disconnected;

    public event MessageHandler MessageReceived;

    public TCPClientController(){ }

    public async Task ConnectToServerAsync(string ip, int port){
        try{
            _tcpClient = new TcpClient();

            await _tcpClient.ConnectAsync(IPAddress.Parse(ip), port);
            _client = _tcpClient.Client;
            _networkStream = _tcpClient.GetStream();
            _channel = Channel.CreateUnbounded<string>();

            _writingTask = RunWritingLoop();
            _readingTask = RunReadingLoop();
            Connected?.Invoke();
        }
        catch (Exception e){
            MessageReceived?.Invoke(e.Message);
        }
    }

    public async Task DisconnectFromServerAsync(){
        _channel.Writer.Complete();
        _networkStream.Close();
    }

    private async Task RunReadingLoop(){
        try{
            var headerBuffer = new byte[sizeof(int)];
            while (true){
                var bytesReceived = await _networkStream.ReadAsync(headerBuffer);
                if (bytesReceived != sizeof(int))
                    break;
                var length = BinaryPrimitives.ReadInt32LittleEndian(headerBuffer);
                var buffer = new byte[length];
                var count = 0;
                while (count < length){
                    bytesReceived = await _networkStream.ReadAsync(buffer.AsMemory(count, buffer.Length - count));
                    count += bytesReceived;
                }

                var message = Encoding.UTF8.GetString(buffer);
                MessageReceived?.Invoke($"<< {_client.RemoteEndPoint}: {message}");
            }

            MessageReceived?.Invoke($"Server closed connection.");
            _networkStream.Close();
        }
        catch (IOException){
            MessageReceived?.Invoke($"Connection closed.");
        }
        catch (Exception ex){
            MessageReceived?.Invoke(ex.GetType().Name + ": " + ex.Message);
        }

        Disconnected?.Invoke();
    }

    public async Task SendMessageAsync(string message){
        await _channel.Writer.WriteAsync(message);
    }

    private async Task RunWritingLoop(){
        var header = new byte[sizeof(int)];
        await foreach (var message in _channel.Reader.ReadAllAsync()){
            var buffer = Encoding.UTF8.GetBytes(message);
            BinaryPrimitives.WriteInt32LittleEndian(header, buffer.Length);
            await _networkStream.WriteAsync(header, 0, header.Length);
            await _networkStream.WriteAsync(buffer, 0, buffer.Length);
        }
    }
}