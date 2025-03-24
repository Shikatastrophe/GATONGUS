const express = require('express')
const app = express()
const port = 1234
var count = 0;


app.get('/', (req, res) => { 
    res.send('Hello world');
});


app.get('/count', (req, res) => { 
    count++;
    res.send('Contador: ' + count);
});

app.get('/action/init', (req, res) => { 
    res.send('Inicialización de gato');
});


app.get('/action/status/:player', (req, res) => { 
    //Changes will reset cpunter
    res.send('Return status os player: ' + req.params['player'] + '<br> contador: ' + count);
});


app.get('/action/turn/:player/:pos', (req, res) => { 
    let player = "";
    let pos = req.params['pos'];

    switch (req.params['player'])
    {
        case '1':
            player = "player01";
            break;

        case '2':
            player = "player02";
            break;
        
        default:
            player = "error";
            break;
    }

    if(pos >= 1 && pos <= 9)
    {
        res.send('El player ' + player + ' ha tirado en: ' + pos);
    }
    else
    {
        res.send('El player ' + player + ' ha tirado en: ERROR');
    }
});


app.listen(port, () => {
    console.log(`Server init: ${port}`);
});