const express = require('express');
const app = express();
const port = 8080;
var count=0;

app.get('/',(req,res)=>{
    res.send('Hello world');
});

app.get('/action/count',(req,res)=>{
    count++;
    res.send('Contador: '+count);
});

app.get('/action/init',(req,res)=>{
    res.send('Inicialización de gato...');
});

app.get('/action/status/:player',(req,res)=>{
    res.send('Rturn status of player '+req.params["player"]);
});

app.get('/action/turn/:player/:pos',(req,res)=>{
    let player = "";
    switch(req.params['player']){
        case "1":
            player = "player01";
            break;
        
        case "2":
            player = "player02";
            break;

        default:
            player = "error";
            break;
    }
    res.send('El player'+player+' ha tirado');
});

app.listen(port, () => {
    console.log(`Server init: ${port}`);
});

