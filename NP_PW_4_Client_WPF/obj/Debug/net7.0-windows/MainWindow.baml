   M S B A M L   `   `   `  ����M  INP_PW_4_Client_WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:NP_PW_4_Client_WPFNP_PW_4_Client_WPF  5   	   #    NP_PW_4_Client_WPF.MainWindow   5   	   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   2local clr-namespace:NP_PW_4_Client_WPF     5   	     � Title$  
MainWindow��5   	   $	��450��6   $	��800��6)   $	��300��6d   $	��450��6t    � DataContext 65   ��  �� ElementName$ 
_viewModel��.��5	      �  � RowDefinitions 5
   
   �� 5      $��48 �6   �� 5      $��* �6   �� 5      $��48 �6   .+�5   
   �  � ColumnDefinitions 5      �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      .+�5      �� $��0��6   $
��6 12q�6(   $
��IP: ��66   $��Right=�6B   �� 5      $��1��6   $
��6 12q�6&   ��64   �� *	IpAddress+�� 5      $��2��6   $
��6 12q�6(   $��Port: ��66   $��Right=�6D   �� 5      $��3��6   $
��6 12q�6&   ��64   �� *Port+�� 5      $��4��6   $
��6 12q�6%   $��Connect��63   {�6E   �� *IsDisconnected+��5      �� *ConnectCommand+�� 5      $��5��6   $
��6 12q�6%   $��
Disconnect��63   {�6H   �� *IsConnected+��5      �� *DisconnectCommand+5       �� 5!   
   $��1��6    �� 
IsReadOnly$
 True��6   $	��6 0q�61   ��6>   �� *
Messages+� 5"   
   $��2��6    5#      �� 5$      $��5* �6#   �� 5%      .+�5'      �� $
��6 12q�6   ��6$   �� *MessageInput+�� 5(      $��1��6   $
��6 12q�6%   $
��Send��63   {�6B   �� *IsConnected+��5)      �� *SendMessageCommand+5*      5+      5,      