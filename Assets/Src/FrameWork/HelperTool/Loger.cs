﻿#define IN_UNITY

using System;
using UnityEngine;

namespace HG
{
    public static class Loger
    {
        public static string Now => DateTime.Now.ToString() + "    ";

        public static bool UseLog = true;
#if IN_UNITY
        public static void Error(object o)
        {
            if(UseLog)
                Debug.LogError(Now + o);
        }
        
        public static void Fatal(object o)
        {
            Debug.LogError(Now + o);
            Application.Quit();
        }

        public static void Warn(object o)
        {
            if(UseLog)
                Debug.LogWarning(Now + o);
        }

        public static void Info(object o)
        {
            if(UseLog)
                Debug.Log(Now + o);
        }
        
        public static void Log(object o)
        {
            Info(o);
        }
        
        public static void Color(object o,string color = "green")
        {
            if (UseLog)
                Debug.LogFormat(Now + "<color={0}>{1}</color>", color, o);
        }  
#else
    public static void Error(object o)
        {
            if (UseLog)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(Now + o);
                Console.ResetColor();
            }
        }

        public static void Warn(object o)
        {
            if (UseLog)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine(Now + o);
                Console.ResetColor();
            }
        }

        public static void Info(object o)
        {
            if (UseLog)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(Now + o);
                Console.ResetColor();
            }
        }
        
        public static void Log(object o)
        {
            Info(o);
        }
        
        public static void Color(object o,string color = "green")
        {
            if (UseLog)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(Now + o);
                Console.ResetColor();
            }
        }
#endif 
    }
}