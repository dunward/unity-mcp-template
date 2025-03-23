import * as Net from 'net';

var unity: Net.Socket | null = null;

export function setupUnityConnection(port: number) {

    unity = new Net.Socket();
    
    unity.connect(port, '127.0.0.1', () => {
        console.error('[MCP] Connected to TCP server');
    });

    unity.on('error', (error) => {
        console.error('[MCP] TCP Client error:', error);
    });

    unity.on('close', () => {
        console.error('[MCP] Connection closed');
    });
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