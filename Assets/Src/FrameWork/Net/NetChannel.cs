﻿using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

 namespace HG
{
    public class NetChannel
    {
        public string Name { get; private set; }
        private Socket _socket;
        private NetSend _netSend;

        public const int ConnectSucced = 10000;
        public const int SendSucced = 10001;
        public const int SendError = 10002;
        

        public NetChannel(string name = "")
        {
            Name = name;
            _netSend = new NetSend();
        }
        
        public void Connect(string ip, int port)
        {
            CloseF();
            
            IPAddress ipAddress = IPAddress.Parse(ip);
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, port);

            _socket = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            _socket.BeginConnect(ipEndPoint, ConnectCB, new ConnectState(_socket));
        }

        private void ConnectCB(IAsyncResult ar)
        {
            var connectState = (ConnectState) ar.AsyncState;

            try
            {
                connectState.Socket.EndConnect(ar);
            }
            catch (Exception e)
            {
                Loger.Error(e);
                throw;
            }

            EventMgr.Instance.Notify(ConnectSucced, Name);
            DoReceive();
        }

        public void Send(string msg)
        {
            var bytes = Encoding.UTF8.GetBytes(msg);
            
            _netSend.SetBuffer(bytes);

            Send();
        }

        private void Send()
        {
            _socket.BeginSend(_netSend.Buffer, _netSend.Pos, _netSend.Length, SocketFlags.None, SendCB, _socket);
        }

        private void SendCB(IAsyncResult ar)
        {
            var sendLen = _socket.EndSend(ar);

            if (sendLen == 0)
            {
                EventMgr.Instance.Notify(SendError);
                CloseF();
                return;
            }

//            var socket = (Socket) ar.AsyncState;
            _netSend.Pos += sendLen;

            if (_netSend.Pos < _netSend.Length)
            {
                Send();
            }
            else
            {
                EventMgr.Instance.Notify(SendSucced, _netSend.Buffer);
                _netSend.Reset();
            }
        }

        private void DoReceive()
        {
//            _socket.BeginReceive()
        }

        public void Close()
        {
            CloseF();
        }

        private void CloseF()
        {
            if (_socket != null)
            {
                _socket.Close();
                _socket = null;
            }
        }
    }
}