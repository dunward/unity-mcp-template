import * as Net from 'net';

var unity: Net.Socket | null = null;

export function setupUnityConnection(port: number) {

    unity = new Net.Socket();
    
    unity.connect(port, '127.0.0.1', () => {
        console.error('[MCP] Connected to TCP server');
    });

    unity.on('data', (data) => {
        console.error('[MCP] Received: ' + data.toString());
    });

    unity.on('error', (error) => {
        console.error('[MCP] TCP Client error:', error);
    });

    unity.on('close', () => {
        console.error('[MCP] Connection closed');
    });
}

export function sendToUnity(message: string) {
    if (unity) {
        unity.write(message);
        return `[MCP] Successfully sent message to Unity: ${message}`;
    } else {
        return "[MCP] Unity Connection not available";
    }
}