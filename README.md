![](https://badge.mcpx.dev 'MCP') ![GitHub License](https://img.shields.io/github/license/dunward/unity-mcp-template)
# Unity MCP Template
![mcp-template-ezgif com-resize](https://github.com/user-attachments/assets/eb51b904-3301-4c88-9f7d-8ca3333024f3)

This is a simple example project demonstrating interaction between a TypeScript-based MCP server and Unity. You can build and configure your own tools to expand and develop further.

Create more tools your own!

## Quick Start
unity-mcp-sample is a Unity example project. To minimize version-related issues and ensure compatibility with legacy versions, it does not use packages like NewtonsoftJSON. Additionally, the EditorWindow is implemented using IMGUI. The MCP can be managed through the UnityMCP-ShowWindow at the top.

<img src="https://github.com/user-attachments/assets/48a247b6-29ac-466a-ac7a-afdad377a84f" width=30%, height=30%>

### Build MCP Server
within the unity-mcp-server
```
npm install
npm run build
```

### Add MCP in Claude Desktop
Open Claude Desktop Settings, and Developer-Edit Config
```
{
    "mcpServers": {
      "unity-mcp": {
        "command": "node",
        "args":["F:/unity-mcp-template/unity-mcp-server/dist/index.js"]
      }
    }
  }
```

### Create your own tools!
Both input data structure should be same.
#### Unity
Refer to CreateObjectTools and create the tool you want.

#### Typescript
Refer to createObject and create the tool you want.

## TODO List
- [x] Enable Unity to send result messages to MCP
- [ ] [Discussion here](https://github.com/dunward/unity-mcp-template/issues/1) ~~Change TCP structure (currently implemented with client-server reversed due to a bug in the TypeScript SDK)~~
  - ~~Unity (Current : TCP Server, TODO : TCP Client)~~
  - ~~MCP (Current : TCP Client, TODO : TCP Server)~~
  

