const WebSocket = require('ws');

const clients = [];

const wss = new WebSocket.Server({port:8080},()=>{
    console.log("Server started");
});

wss.on('connection', function connection(ws){
    console.log('Se conecto un cliente...');
    clients.push(ws);

    ws.on('open', (data) => {
        console.log('New Connection');
    });

    ws.on('message', (data) => {
        console.log('Data recived: %s',data);

        ws.send("The server response: "+data);
    });
});

wss.on('listening',()=>{
    console.log('listening on 8080');

    
});