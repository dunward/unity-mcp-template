import { MCPServer } from "mcp-framework";
import * as UnityConnection from "./unity/unityConnection.js";

const server = new MCPServer(
    {
        name: "unity-mcp",
        version: "0.0.1"
    }
);

server.start();
await UnityConnection.setupUnityConnection(6336);