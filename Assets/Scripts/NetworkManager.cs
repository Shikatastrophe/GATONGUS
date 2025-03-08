using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(getText());   
    }

    IEnumerator getText()
    {
        UnityWebRequest www = UnityWebRequest.Get("http://localhost/gato.php?action=2&id=id1");
        yield return www.Send();

        if (www.isNetworkError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
            //JsonUtility.ToJson(www.downloadHandler.text);
            //JsonUtility.FromJson<GatoConstructor>(www.downloadHandler.text);
            Debug.Log(JsonUtility.FromJson<GatoConstructor>(www.downloadHandler.text).score1);
            byte[] results = www.downloadHandler.data;
        }
    }
}

public class GatoConstructor
{
    public int p1;
    public int p2;
    public int actual;
    public int round;
    public int score1;
    public int score2;
    public int[] board = new int[8];
}