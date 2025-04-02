using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ChatRenovadoTerrorOzuna : MonoBehaviour
{
    public string baseUrl = "http://chat_u3d.test/chat.php";
    public static GatoGod gatopro = new GatoGod();
    int array;
    public enum chamba { init, initnt};

    public chamba chambium;

    private void OnEnable()
    {
        GameManager.SwitchID += StartSwitch;
    }

    private void OnDisable()
    {
        GameManager.SwitchID -= StartSwitch;
    }

    public void StartSwitch(int arrpos,int id)
    {
        array = arrpos;
        StartCoroutine(GetRequest(baseUrl + "/action/turn/"+id+"/"+array));
    }


    

    void Start()
    {
        if (chambium == chamba.init)
            StartCoroutine(GetRequest(baseUrl + "/action/init"));
        else
            StartCoroutine(GetText());
    }

    public IEnumerator GetText()
    {
        while (true)
        {
            StartCoroutine(GetRequest(baseUrl + "/action/check"));
            yield return new WaitForSeconds(0.1f);
            
        }
    }

    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(uri + "\nError: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(uri + "\nHTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    //Debug.Log(uri + "\nReceived: " + webRequest.downloadHandler.text);
                    //Debug.Log(webRequest.downloadHandler.text);
                    gatopro = JsonUtility.FromJson<GatoGod>(webRequest.downloadHandler.text);
                    break;
            }
        }
    }
}

[System.Serializable]
public class GatoGod
{
    public string p1;
    public string p2;
    public int actual;
    public int round;
    public int score1;
    public int score2;
    public int[] board;
}