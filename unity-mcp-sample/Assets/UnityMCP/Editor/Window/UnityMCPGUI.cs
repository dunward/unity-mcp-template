using System.Collections.Generic;

using UnityEngine;
using UnityEditor;

public static class UnityMCPGUI
{
    public static void Title()
    {
        var labelStyle = new GUIStyle(GUI.skin.label);
        labelStyle.alignment = TextAnchor.MiddleCenter;
        labelStyle.fontStyle = FontStyle.Bold;
        labelStyle.fontSize = 22;

        GUILayout.Label("Unity MCP", labelStyle);
    }

    public static void Connection(System.Net.Sockets.TcpListener server, List<System.Net.Sockets.TcpClient> clients, System.Action startServer, System.Action stopServer)
    {
        var groupStyle = new GUIStyle(GUI.skin.box);
        groupStyle.padding = new RectOffset(10, 10, 10, 10);
        groupStyle.margin = new RectOffset(10, 10, 10, 10);
        
        var subTitle = new GUIStyle(GUI.skin.label);
        subTitle.fontStyle = FontStyle.Bold;
        subTitle.fontSize = 16;

        var serverState = new GUIStyle(GUI.skin.label);
        serverState.fontStyle = FontStyle.Bold;
        serverState.fontSize = 12;
        serverState.richText = true;

        var buttonStyle = new GUIStyle(GUI.skin.button);
        buttonStyle.fontSize = 14;
        buttonStyle.fontStyle = FontStyle.Bold;
        buttonStyle.alignment = TextAnchor.MiddleCenter;
        buttonStyle.fixedHeight = 28;

        GUILayout.BeginVertical(groupStyle);
        GUILayout.Label("Connection", subTitle);
        if (server != null && server.Server.IsBound)
        {
            GUILayout.Label("Unity Status : <color=green>Online</color>", serverState);
        }
        else
        {
            GUILayout.Label("Unity Status : <color=red>Offline</color>", serverState);
        }

        if (clients.Count > 0)
        {
            GUILayout.Label($"MCP Status : <color=green>{clients.Count} MCPs Connected</color>", serverState);
        }
        else
        {
            GUILayout.Label("MCP Status : <color=red>0 MCPs</color>", serverState);
        }

        GUILayout.BeginHorizontal();
        GUI.enabled = server == null || !server.Server.IsBound;
        if (GUILayout.Button("Start Server", buttonStyle))
        {
            startServer();
        }
        GUI.enabled = server != null && server.Server.IsBound;
        if (GUILayout.Button("Stop Server", buttonStyle))
        {
            stopServer();
        }
        GUILayout.EndHorizontal();
        GUILayout.EndVertical();
    }
}