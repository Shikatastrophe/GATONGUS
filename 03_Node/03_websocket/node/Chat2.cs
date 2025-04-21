using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NativeWebSocket;

public class Chat : MonoBehaviour
{
    WebSocket websocket;
    public string strMessage = "xd";
    // Start is called before the first frame update
    async void Start()
    {
        websocket = new WebSocket("ws://172.16.48.133:8080");

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
            Debug.Log("OnMessage! "+ message);
        };
        //InvokeRepeating("SendWebSocketMessage", 0.0f, 0.3f);
        await websocket.Connect();
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_WEBGL || UNITY_EDITOR
        websocket.DispatchMessageQueue();
    }
#endif

    async void SendWebSocketMessage()
    {
        if (websocket.State == WebSocketState.Open)
        {
            await websocket.SendText("Plaintext message: "+strMessage);
        }
    }

    private async void OnApplicationQuit()
    {
        await websocket.Close();
    }

    public void wsSendMessage()
    {
        SendWebSocketMessage();
    }
}


