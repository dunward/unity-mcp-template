using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

public class UnityMCPWindow : EditorWindow
{
    private TcpListener server;
    private List<TcpClient> clients = new List<TcpClient>();

    [MenuItem("UnityMCP/Show Window")]
    public static void ShowWindow()
    {
        GetWindow<UnityMCPWindow>("UnityMCP");
    }

    private void OnGUI()
    {
        GUILayout.Space(10);
        UnityMCPGUI.Title();
        UnityMCPGUI.Connection(server, clients, StartServer, StopServer);
        GUILayout.Space(10);
        GUILayout.BeginHorizontal();
        GUILayout.EndHorizontal();
    }

    private async void StartServer()
    {
        server = new TcpListener(System.Net.IPAddress.Parse("127.0.0.1"), 6336);
        server.Start();

        Debug.LogError("Unity MCP ready");

        while (true)
        {
            var client = await server.AcceptTcpClientAsync();
            Debug.LogError($"{client.Client.RemoteEndPoint} is connected");
            lock (clients)
            {
                clients.Add(client);

                Debug.LogError("MCP connected");

                _ = HandleClientAsync(client);
            }
        }
    }

    private async Task HandleClientAsync(TcpClient client)
    {
        try
        {
            while (client.Connected)
            {
                var stream = client.GetStream();
                var buffer = new byte[1024];
                var bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);

                if (bytesRead == 0)
                {
                    Debug.LogError("Client disconnected.");
                    break;
                }

                var message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                Debug.LogError($"Message Received: {message}");

                var match = JsonUtility.FromJson<BaseTool>(message);

                Debug.LogError($"{match.name}, {match.format}");

                switch (match.name)
                {
                    case "create_object_tool":
                        await Result(client, CreateObjectTools.CreateObject(match.format));
                        break;
                    case "editor_mode_tool":
                        await Result(client, "ping");
                        break;
                }
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Error in client communication: {ex.Message}");
        }
        finally
        {
            clients.Remove(client);
            client.Close();
            Debug.LogError("Client connection closed.");
        }
    }

    private async Task Result(TcpClient client, string resultLog)
    {
            Debug.LogError($"Send to {client.Client.RemoteEndPoint}");
        var stream = client.GetStream();
        var buffer = Encoding.UTF8.GetBytes(resultLog);
        await stream.WriteAsync(buffer, 0, buffer.Length);
    }

    private void StopServer()
    {
        server.Stop();

        lock (clients)
        {
            foreach (var client in clients)
            {
                client.Close();
            }
            clients.Clear();
        }
    }
}
