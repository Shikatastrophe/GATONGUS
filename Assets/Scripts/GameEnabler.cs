using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameEnabler : MonoBehaviour
{
    GameManager gameManager;
    public RawImage img;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
    }

    public void setP1()
    {
        gameManager.idAct = "p1";
        gameManager.id = 1;
        img.color = gameManager.p1Color;
        gameManager.hasGameStarted = true;
        disableGO();
    }

    public void setP2()
    {
        gameManager.idAct = "p2";
        gameManager.id = 2;
        img.color = gameManager.p2Color;
        gameManager.hasGameStarted = true;
        disableGO();
    }

    public void disableGO()
    {
        this.gameObject.SetActive(false);
    }
}
