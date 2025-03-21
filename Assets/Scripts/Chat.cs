using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Chat : MonoBehaviour
{
    public TMP_InputField UserName;
    public TMP_InputField yext;
    public TextMeshProUGUI chat;
    string gamner;
    public string baseUrl = "http://chat_u3d.test/chat.php";

    public void getRooms ()
    {
        StartCoroutine(GetRequest(baseUrl+"?action=1"));
    }

    public void getMessages(string room)
    {
        chat.text = gamner;
        StartCoroutine(GetRequest(baseUrl + "?action=2&room="+room));
    }

    public void sendAnimeMessage()
    {
        string room = "anime",
            message = yext.text,
            user = UserName.text;
        StartCoroutine(GetRequest(baseUrl + "?action=3&room="+room+"&username="+user+"&message="+message));
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
                    Debug.LogError(uri+"\nError: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(uri + "\nHTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log(uri + "\nReceived: " + webRequest.downloadHandler.text);
                    gamner = webRequest.downloadHandler.text;
                    break;
            }
        }
    }
}
