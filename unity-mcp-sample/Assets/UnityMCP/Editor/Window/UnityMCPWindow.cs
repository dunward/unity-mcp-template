using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

public class UnityMCPWindow : EditorWindow
{
    private TcpListener server;
    private TcpClient client;

    [MenuItem("UnityMCP/Show Window")]
    public static void ShowWindow()
    {
        GetWindow<UnityMCPWindow>("UnityMCP");
    }

    private void OnGUI()
    {
        GUILayout.Space(10);
        UnityMCPGUI.Title();
        UnityMCPGUI.Connection(server, client, StartServer, StopServer);
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
            client = await server.AcceptTcpClientAsync();

            Debug.LogError("MCP connected");

            _ = HandleClientAsync(client);
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
                        CreateObjectTools.CreateObject(match.format);
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
            client.Close();
            Debug.LogError("Client connection closed.");
        }
    }

    private void StopServer()
    {
        server.Stop();
    }
}
