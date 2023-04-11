using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using Prism.Commands;

namespace NP_PW_4_Client_WPF;

public sealed class ViewModel : INotifyPropertyChanged{
    private readonly TCPClientController _tcpClientController = new();

    private string _ipAddress;

    private string _port;

    private string _messageInput;
    
    private string _messages;

    private bool _isConnected;

    private bool _isDisconnected = true;

    private DelegateCommand _connectCommand;

    private DelegateCommand _disconnectCommand;
    
    private DelegateCommand _sendMessageCommand;

    public DelegateCommand ConnectCommand => _connectCommand ??= new DelegateCommand(ExecuteConnectToServer);

    public DelegateCommand DisconnectCommand => _disconnectCommand ??= new DelegateCommand(ExecuteDisconnectFromServer);
    
    public DelegateCommand SendMessageCommand => _sendMessageCommand ??= new DelegateCommand(ExecuteSendMessage);

    public string IpAddress{
        get => _ipAddress;
        set{
            _ipAddress = value;
            OnPropertyChanged();
        }
    }

    public string Port{
        get => _port;
        set{
            _port = value;
            OnPropertyChanged();
        }
    }

    public string MessageInput{
        get => _messageInput;
        set{
            _messageInput = value;
            OnPropertyChanged();
        }
    }

    public bool IsConnected{
        get => _isConnected;
        set{
            _isConnected = value;
            OnPropertyChanged();
        }
    }

    public bool IsDisconnected{
        get => _isDisconnected;
        set{
            _isDisconnected = value;
            OnPropertyChanged();
        }
    }
    
    public string Messages{
        get => _messages;
        set{
            _messages = value;
            OnPropertyChanged();
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    
    public ViewModel(){
        _tcpClientController.Connected += TcpClientControllerOnConnected;
        _tcpClientController.Disconnected += TcpClientControllerOnDisconnected;
        _tcpClientController.MessageReceived += TcpClientControllerOnMessageReceived;
        IpAddress = "127.0.0.1";
        Port = "13400";
    }
    
    private void TcpClientControllerOnConnected(){
        IsConnected = true;
        IsDisconnected = !IsConnected;
        Messages = "Connected.\n";
    }

    private void TcpClientControllerOnDisconnected(){
        IsDisconnected = true;
        IsConnected = !IsDisconnected;
    }

    private void TcpClientControllerOnMessageReceived(string message){
        Messages += message + "\n";
    }

    private void ExecuteConnectToServer(){
        try{
            _tcpClientController.ConnectToServerAsync(_ipAddress, int.Parse(_port));
        }
        catch (Exception exception){
            MessageBox.Show(exception.Message);
        }
    }

    private void ExecuteDisconnectFromServer(){
        _tcpClientController.DisconnectFromServerAsync();
    }

    private void ExecuteSendMessage(){
        try{
            _tcpClientController.SendMessageAsync(MessageInput);
            Messages += MessageInput + "\n";
            MessageInput = string.Empty;
        }
        catch (Exception exception){ }
    }
    private void OnPropertyChanged([CallerMemberName] string? propertyName = null){
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}