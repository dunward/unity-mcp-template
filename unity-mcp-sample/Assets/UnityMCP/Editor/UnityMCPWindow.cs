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

    private static async void StartServer()
    {
        server = new TcpListener(System.Net.IPAddress.Parse("127.0.0.1"), 6336);
        server.Start();

        var client = await server.AcceptTcpClientAsync();

        Debug.LogError("Client connected");

        while (client.Connected)
        {
            Debug.LogError("Waiting for message...");
            var stream = client.GetStream();
            byte[] buffer = new byte[1024];
            int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
            string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
            Debug.LogError(message);
        }
    }

    private static void StopServer()
    {
        server.Stop();
    }
}
