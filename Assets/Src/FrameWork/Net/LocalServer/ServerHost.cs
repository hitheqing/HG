﻿using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace HG
{
    public class ServerHost
    {
        private Socket _socket;
        
        Dictionary<string,Socket>  clientMap=new Dictionary<string, Socket>();
        private Thread listenConnect;
        private Thread receiveThread;

        public ServerHost()
        {
            
        }
        
        public void Begin()
        {
            if (_socket != null)
            {
                _socket.Close();
                _socket = null;
            }
            
            IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
            IPEndPoint point = new IPEndPoint(ipAddress, 4567);
            
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            
            _socket.Bind(point);
            
            _socket.Listen(10);
            
            Loger.Color("start server!!!");
            
            listenConnect = new Thread(Recv);
            listenConnect.IsBackground = true;
            listenConnect.Start(_socket);
            
            Thread thread2 = new Thread(ttt);
            thread2.IsBackground = true;
            thread2.Start();
        }

        public void Exit()
        {
            if (_socket != null)
            {
                _socket.Close();
                _socket = null;
            }
            
            listenConnect.Abort();
            receiveThread.Abort();
        }

        private void ttt()
        {
            var i = 0;
            while (true)
            {
               
                if (i > 10000)
                {
                    Loger.Color("!!!");
                    i = 0;
                }

                i++;
            }
        }

        private void Recv(object obj)
        {
            Socket socket = obj as Socket;
            while (true)
            {
                Socket  tSocket = socket.Accept();
                string point = tSocket.RemoteEndPoint.ToString();
                Loger.Color(point + "连接成功！");
                clientMap.Add(point, tSocket);
                
                receiveThread = new Thread(ReceiveMsg);
                receiveThread.IsBackground = true;
                receiveThread.Start(tSocket);
            }
        }
        
        private static void ReceiveMsg(object o)
        {
            Socket client = o as Socket;

            while (true)
            {
                byte[] buffer = new byte[1024 * 1024];
                int n = client.Receive(buffer);
                string words = Encoding.UTF8.GetString(buffer, 0, n);
                Loger.Color("受到消息来自："+ client.RemoteEndPoint+ ":" + words, "blue");
            }
           
        }
    }
}