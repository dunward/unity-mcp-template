import { MCPTool } from "mcp-framework";
import { z } from "zod";
import * as UnityConnection from "../unity/unityConnection.js";

interface EditorModeInput {
  message: string;
}

class EditorModeTool extends MCPTool<EditorModeInput> {
  name = "editor_mode_tool";
  description = "Unity Editor play mode control tool";

  schema = {
    message: {
      type: z.string(),
      description: "Control unity editor play mode state",
      enum: ["start", "stop", "pause"],
    },
  };

  async execute(input: EditorModeInput) {
    return UnityConnection.sendToUnity(input.message);
  }
}

export default EditorModeTool;