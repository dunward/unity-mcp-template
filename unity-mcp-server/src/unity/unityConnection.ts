import * as Net from 'net';

var unity: Net.Socket | null = null;

export function setupUnityConnection(port: number) {

    unity = new Net.Socket();
    
    unity.connect(port, '127.0.0.1', () => {
        console.log('Connected to TCP server');
    });

    // 서버로부터 메시지를 받았을 때
    unity.on('data', (data) => {
        console.log('Received: ' + data.toString());
    });

    // 오류 처리
    unity.on('error', (error) => {
        console.error('TCP Client error:', error);
    });

    // 연결 종료
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