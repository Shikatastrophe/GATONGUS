const express = require('express');
const app = express();
const port = 8080;

var $db="game.db";

var     $p1, // username
        $p2, // username2
        $actual,
        $round,
        $score1,
        $score2,
        $board; // array

app.get('/action/init',(req,res) =>{
    $board = [0,0,0,0,0,0,0,0,0];
    $p1 = "id1";
    $p2 = "id2";
    $actual = 0;
    $round = 0;
    $score1 = 0;
    $score2 = 0;
    $data = toJson();
    res.send($data);
})


app.get('/action/check',(req,res) =>{
    $data = toJson();
    res.send($data);
})


function toJson(){
    $data = {
        "p1":$p1,
        "p2":$p2,
        "actual":$actual,
        "round":$round,
        "score1":$score1,
        "score2":$score2,
        "board":$board
    }
    return $data;
}

app.get('/action/turn/:player/:arrpos',(req,res) =>{
    let player = req.params["player"];
    let arraypos = req.params["arrpos"];
    turn(player,arraypos);
    res.send("Player is "+player);
})

app.get('/',(req,res) =>{
    //res.send("Hello World");
})


function turn(id,pos){
    $board[pos] = id;
}

app.listen(port,()=>{
    console.log(`Server init: ${port}`)
})