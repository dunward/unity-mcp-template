import { MCPServer } from "mcp-framework";
import * as UnityConnection from "./unity/unityConnection.js";

const server = new MCPServer();

server.start();
await UnityConnection.setupUnityConnection(6336);