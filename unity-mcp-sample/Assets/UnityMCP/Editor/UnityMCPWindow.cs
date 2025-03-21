using System;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using UnityEditor;
using UnityEngine;

public class UnityMCPWindow : EditorWindow
{
    private static TcpListener server;


    [MenuItem("UnityMCP/Show Window")]
    public static void ShowWindow()
    {
        GetWindow<UnityMCPWindow>("UnityMCP");
    }

    private void OnGUI()
    {
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Start Server"))
        {
            StartServer();
        }
        if (GUILayout.Button("Stop Server"))
        {
            StopServer();
        }
        GUILayout.EndHorizontal();
    }

    private static void StartServer()
    {
        server = new TcpListener(System.Net.IPAddress.Parse("127.0.0.1"), 6336);
        server.Start();
    }

    private static void StopServer()
    {
        server.Stop();
    }
}
