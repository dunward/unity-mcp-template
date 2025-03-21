import * as Net from 'net';

var unity: Net.Socket | null = null;

export function setupUnityConnection(port: number) {

    unity = new Net.Socket();
    
    unity.connect(port, '127.0.0.1', () => {
        console.log('Connected to TCP server');
    });

    unity.on('data', (data) => {
        console.log('Received: ' + data.toString());
    });

    unity.on('error', (error) => {
        console.error('TCP Client error:', error);
    });

    unity.on('close', () => {
        console.log('Connection closed');
    });
}

export function sendToUnity(message: string) {
    if (unity) {
        unity.write(message);
        return "Successfully sent message to Unity";
    } else {
        return "Unity Connection not available";
    }
}