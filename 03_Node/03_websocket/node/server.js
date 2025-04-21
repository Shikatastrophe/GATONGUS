const WebSocket = require('ws');

const clients = []; 

let actual, board,p1,p2;

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
        let info = data.toString().split('|');
        //console.log(clients.length)
        switch(info[0])
        {
            case '0':
                console.log("Game Reset");
                reset();
                break;
            case '1':
                clients.forEach(client =>{
                    if(client.readyState === WebSocket.OPEN){
                        let values = check();
                        client.send(values);
                        console.log(values);
                    }
                })
                break;
            case '2':
                turn(info[1],info[2]);
        }
    });

    ws.on('close', () => { 
		let index = clients.indexOf(ws);
		if(index > -1)
		{
			clients.splice(index, 1);
			ws.send("User disconnected");
		}
	});
});

wss.on('listening',()=>{
    console.log('listening on 8080');
});

function reset(){
    p1 = 1;
    p2 = 2;
    actual = 1;
    board = [0,0,0,0,0,0,0,0,0];
}

function check(){
    let values = p1+"|"+p2+"|"+actual+"|"+board;
    return values;
}

function turn(player1, arrpos1){
    let player = player1;
    let arrpos = arrpos1;
    board[arrpos] = player;
    actual ++;
}
