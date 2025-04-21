using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NativeWebSocket;
using System;
using TMPro;

public class Chat2 : MonoBehaviour
{
    WebSocket websocket;
    public string strMessage = "xd";
    public string chavdelocho;
    int array;
    public Gato3MasGatoQueNunca gato = new Gato3MasGatoQueNunca();
    public TextMeshProUGUI debvug;

    private void OnEnable()
    {
        GameManager.SwitchID += StartSwitch;
    }

    private void OnDisable()
    {
        GameManager.SwitchID -= StartSwitch;
    }

    public void StartSwitch(int arrpos, int id)
    {
        SendWebSocketMessage("2|" + id + "|" + arrpos);
    }

    // Start is called before the first frame update
    async void Start()
    {
        websocket = new WebSocket(chavdelocho);

        websocket.OnOpen += () =>
        {
            Debug.Log("Connection open!");
        };

        websocket.OnError += (e) =>
        {
            Debug.LogError("Error! "+e);
        };

        websocket.OnClose += (e) =>
        {
            Debug.Log("Connection closed!");
        };

        websocket.OnMessage += (bytes) => { 
            var message = System.Text.Encoding.UTF8.GetString(bytes);
            debvug.text = message;
            var newmesage = message.Split("|");
            int actualround = Convert.ToInt32(newmesage[2]);
            var boardmoment = newmesage[3].Split(",");
            int[] boardint = Array.ConvertAll(boardmoment, s => Convert.ToInt32(s));
            Debug.Log("Round: " + actualround);
            Debug.Log(boardint);

            gato.p1string = newmesage[0];
            gato.p2string = newmesage[1];
            gato.actual = actualround;
            gato.board = boardint;
        };
        //InvokeRepeating("SendWebSocketMessage", 0.0f, 0.3f);
        Invoke(nameof(StartGame), 0.05f);

        await websocket.Connect();
    }

    public void StartGame()
    {
        if (strMessage != "init")
        {
            SendWebSocketMessage("0|0|0");
            StartCoroutine(GetText());
        }
    }


    public void ResetBoard()
    {
        SendWebSocketMessage("0|0|0");
    }

    // Update is called once per frame
    void Update()
    {
        websocket.DispatchMessageQueue();
    }

    async void SendWebSocketMessage(string text)
    {
        if (websocket.State == WebSocketState.Open)
        {
            await websocket.SendText(text);
        }
    }

    private async void OnApplicationQuit()
    {
        await websocket.Close();
    }

    IEnumerator GetText()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);

            SendWebSocketMessage("1|0|0");
        }
    }

}

[System.Serializable]
public class Gato3MasGatoQueNunca
{

    public string p1string;
    public string p2string;
    public int actual;
    public int[] board;
}

