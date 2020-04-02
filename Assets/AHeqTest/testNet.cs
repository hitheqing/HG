﻿using System.Collections.Generic;
using HG;
using UnityEngine;

namespace AHeqTest
{
    public class testNet:MonoBehaviour
    {
        Dictionary<string,NetChannel> clients = new Dictionary<string, NetChannel>();
        private ServerHost host;

        private void Start()
        {
            
        }

        private void OnGUI()
        {
            if (GUILayout.Button("start sever", GUILayout.Width(400), GUILayout.Height(200)))
            {
                host = new ServerHost();
            
                host.Begin();
            }
            
            if (GUILayout.Button("Client A Connect", GUILayout.Width(400), GUILayout.Height(200)))
            {
                NetChannel client = new NetChannel("Client A");
                clients["Client A"] = client;
                client.Connect("127.0.0.1", 4567);
            }
            
            if (GUILayout.Button("Client A Send", GUILayout.Width(400), GUILayout.Height(200)))
            {
                clients["Client A"].Send("hello world");
                Loger.Color("Client A" + "say-->hello world", "yellow");
            }
        }

        private void OnApplicationQuit()
        {
            if (host != null)
            {
                host.Exit();
            }
        }
    }
}