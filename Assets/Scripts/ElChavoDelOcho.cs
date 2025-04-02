using System.Collections;
using System.Collections.Generic;
using NativeWebSocket;
using UnityEngine;

public class ElChavoDelOcho : MonoBehaviour
{
    WebSocket webSocket;
    public string messagestr = "Gaming";

    async void Start()
    {
        webSocket = new WebSocket("ws://localhost:8080");

        webSocket.OnOpen += () =>
        {
            Debug.Log("Connection Open");
        };

        webSocket.OnError += (e) =>
        {
            Debug.Log("Connection error" + e);
        };

        webSocket.OnClose += (e) =>
        {
            Debug.Log("Connection Closed");
        };

        webSocket.OnMessage += (bytes) =>
        {
            var message = System.Text.Encoding.UTF8.GetString(bytes);
            Debug.Log(message);
        };

        await webSocket.Connect();
    }

    // Update is called once per frame
    void Update()
    {
#if !UNITY_WEBGL || UNITY_EDITOR 
        webSocket.DispatchMessageQueue();
#endif
    }

    async void SendWebSocketMessage()
    {
        if (webSocket.State == WebSocketState.Open) 
        {
            await webSocket.SendText(messagestr);
        }
    }

    private async void OnApplicationQuit()
    {
        await webSocket.Close();
    }
}
