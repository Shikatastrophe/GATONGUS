using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static event Action stopGame;

    private static GameManager instance;

    public bool isOdd;

    public int[] IDs;

    int turnNumber;

    bool hasWinner;

    public TextMeshProUGUI victext;

    Camera MainCamera;

    public Color p1Color;

    public Color p2Color;

    public static GameManager Instance
    {
        get
        {
            if(instance == null)
            {
                GameObject gamemanager = new GameObject();
                instance = gamemanager.AddComponent<GameManager>();
                gamemanager.name = "Game Manager Singleton";
            }
            return instance;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

        MainCamera = Camera.main;

        p1Color = new Vector4(0, 237, 255);

        p2Color = new Vector4(247, 0, 255);

        isOdd = true;

        IDs = new int[9];

        turnNumber = 0;

        victext = new TextMeshProUGUI();

        victext = GameObject.FindGameObjectWithTag("winnertext").GetComponent<TextMeshProUGUI>();

        MainCamera.backgroundColor = p1Color;
    }

    public void ChangeArr(int arrpos, int ply)
    {
        Debug.Log(turnNumber);
        IDs[arrpos] = ply;
        if(ply == 1)
        {
            MainCamera.backgroundColor = p2Color;
            CheckForWinner1();
        }
        if (ply == 2)
        {
            MainCamera.backgroundColor = p1Color;
            CheckForWinner2();
        }
    }

    public void CheckForWinner1()
    {
        if (IDs[0] == 1 && IDs[1] == 1 && IDs[2] == 1) { Victory(1); }
        if (IDs[3] == 1 && IDs[4] == 1 && IDs[5] == 1) { Victory(1); }
        if (IDs[6] == 1 && IDs[7] == 1 && IDs[8] == 1) { Victory(1); }
        if (IDs[0] == 1 && IDs[3] == 1 && IDs[6] == 1) { Victory(1); }
        if (IDs[1] == 1 && IDs[4] == 1 && IDs[7] == 1) { Victory(1); }
        if (IDs[2] == 1 && IDs[5] == 1 && IDs[8] == 1) { Victory(1); }
        if (IDs[0] == 1 && IDs[4] == 1 && IDs[8] == 1) { Victory(1); }
        if (IDs[2] == 1 && IDs[4] == 1 && IDs[6] == 1) { Victory(1); }
        if (turnNumber >= 8 && !hasWinner)
        {
            Victory(3);
            return;
        }
    }

    public void CheckForWinner2()
    {
        if (IDs[0] == 2 && IDs[1] == 2 && IDs[2] == 2) { Victory(2); }
        if (IDs[3] == 2 && IDs[4] == 2 && IDs[5] == 2) { Victory(2); }
        if (IDs[6] == 2 && IDs[7] == 2 && IDs[8] == 2) { Victory(2); }
        if (IDs[0] == 2 && IDs[3] == 2 && IDs[6] == 2) { Victory(2); }
        if (IDs[1] == 2 && IDs[4] == 2 && IDs[7] == 2) { Victory(2); }
        if (IDs[2] == 2 && IDs[5] == 2 && IDs[8] == 2) { Victory(2); }
        if (IDs[0] == 2 && IDs[4] == 2 && IDs[8] == 2) { Victory(2); }
        if (IDs[2] == 2 && IDs[4] == 2 && IDs[6] == 2) { Victory(2); }
        if (turnNumber >= 8 && !hasWinner)
        { 
            Victory(3);
            return;
        }
    }

    public void Victory(int ply)
    {
        stopGame?.Invoke();
        hasWinner = true;
        if (ply == 1)
        {
            victext.text = "P1 WINNER";
        }
        if (ply == 2)
        {
            victext.text = "P2 WINNER";
        }
        if (ply == 3)
        {
            victext.text = "TIE";
        }
    }


    public void switchBool()
    {
        isOdd = !isOdd;
        turnNumber++;
    }
}
