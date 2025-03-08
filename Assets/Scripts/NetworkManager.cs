using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkManager : MonoBehaviour
{
    public string id = "p1";
    int array;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(initialize());
        StartCoroutine(getText());
    }

    private void OnEnable()
    {
        GameManager.SwitchID += StartSwitch;
    }

    private void OnDisable()
    {
        GameManager.SwitchID -= StartSwitch;
    }


    public void StartSwitch(int arrpos)
    {
        array = arrpos+1;
        StartCoroutine(Tirada());
    }

    IEnumerator getText()
    {
        while (true)
        {

            UnityWebRequest www = UnityWebRequest.Get("http://localhost/gato.php?action=2&id="+id);
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
                Debug.Log(JsonUtility.FromJson<Gato>(www.downloadHandler.text).ToString());
                byte[] results = www.downloadHandler.data;
            }
            yield return new WaitForSeconds(1);
        }
    }

    IEnumerator initialize()
    {
        UnityWebRequest www = UnityWebRequest.Get("http://localhost/gato.php?action=1");
        yield return www.Send();

        if (www.isNetworkError)
        {
            Debug.Log(www.error);
        }
        else
        {
            //Debug.Log(www.downloadHandler.text);
            //JsonUtility.ToJson(www.downloadHandler.text);
            //JsonUtility.FromJson<GatoConstructor>(www.downloadHandler.text);
            //Debug.Log(JsonUtility.FromJson<Gato>(www.downloadHandler.text).ToString());
            byte[] results = www.downloadHandler.data;
        }
    }



    IEnumerator Tirada()
    {
        UnityWebRequest www = UnityWebRequest.Get("http://localhost/gato.php?action=3&id=" + id + "&pos=" + array);
        yield return www.Send();

        if (www.isNetworkError)
        {
            Debug.Log(www.error);
        }
        else
        {
            // Show results as text
            Debug.Log(www.downloadHandler.text);

            if (www.downloadHandler.text == "error")
            {
                // Se hace rojito
            }

            // Or retrieve results as binary data
            //byte[] results = www.downloadHandler.data;
        }

    }
}

    [Serializable]
public class Gato
{   //{"actual":0,"round":0,"score1":0,"board":[0,0,0,0,0,0,0,0,0]}
    public int actual;
    public int round;
    public string score1;
    public string score2;
    public int[] board;

    override public string ToString()
    {
        string data = "actual:" + actual + "\nround:" + round + "\nscore1" + score1 + "\nscore2" + score2 + "\nboard\n";
        foreach (var item in board)
        {
            data += item + "\n";
        }
        return data;
    }
}