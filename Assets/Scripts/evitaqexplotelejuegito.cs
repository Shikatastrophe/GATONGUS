using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class evitaqexplotelejuegito : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(initialize());
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
}
