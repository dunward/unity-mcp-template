import { WebSocketServer, WebSocket } from 'ws';

var client: WebSocket | null = null;

export async function setupUnityConnection(port: number) {
    const wsServer = new WebSocketServer({ port: port });

    wsServer.on('listening', () => {
        console.error(`Unity Connection listening on ${port}`);
    });
    
    wsServer.on('connection', (ws) => {
        client = ws;
        ws.on('error', (error) => {
            console.error('Unity Connection Error:', error);
        });
        ws.on('close', () => {
            client = null;
            console.error('Unity Connection disconnected');
        });

        console.error('Unity Connection connected');
    });

    wsServer.on('error', (error) => {
        console.error('Unity Connection Error!', error);
    });

    await new Promise<void>((resolve) => {
        wsServer.once('listening', () => {
            console.error(`Unity Connection listening on ${port}`);
            resolve();
        });
    });
}

export function sendToUnity(message: string) {
    if (client) {
        client.send(message);
        return "Successfully sent message to Unity";
    } else {
        return "Unity Connection not available";
    }
}