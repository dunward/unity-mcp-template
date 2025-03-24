import * as Net from 'net';

var unity: Net.Socket | null = null;
var reconnectInterval = 3000;
var isReconnecting = false;

function connectToUnity(port: number) {
    isReconnecting = false;
    
    unity = new Net.Socket();

    console.error('[MCP] Try to connect unity')

    unity.connect(port, '127.0.0.1', () => {
        console.error('[MCP] Connected to TCP server');
        isReconnecting = false;
    });

    unity.on('error', (error) => {
        console.error('[MCP] TCP Client error:', error);
        scheduleReconnect(port);
    });

    unity.on('close', () => {
        console.error('[MCP] Connection closed');
        scheduleReconnect(port);
    });
}

function scheduleReconnect(port: number) {
    if (isReconnecting) return;
    isReconnecting = true;

    if (unity) {
        unity.destroy();
        unity = null;
    }
    setTimeout(() => connectToUnity(port), reconnectInterval);
}

export function setupUnityConnection(port: number) {
    connectToUnity(port);
}

export function sendToUnity(message: string): Promise<string> {
    return new Promise((resolve, reject) => {
        if (!unity) {
            reject("[MCP] Unity Connection not available");
            return;
        }

        const onData = (data: Buffer) => {
            resolve(`[MCP] Response from Unity: ${data.toString()}`);
        };
    
        unity.once('data', onData);
        unity.write(message, (err) => {
            if (err) {
                reject(`[MCP] Failed to send message: ${err.message}`);
            }
        });
    });
}