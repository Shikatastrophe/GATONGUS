using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static event Action<int,int> SwitchID;

    public static event Action stopGame;

    public static event Action<int, int> UpdateValue;

    private static GameManager instance;

    public bool isOdd;

    public int[] IDs;

    int turnNumber;

    bool hasWinner;

    public TextMeshProUGUI victext;

    Camera MainCamera;

    public Color p1Color;

    public Color p2Color;
    public bool hasGameStarted;
    public GameObject eventoSistema;
    public Chat2 Chat2;
    public string idAct;


    public int id;
    public static GameManager Instance{ get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this) // if we are the instance this is fine
        {
            Destroy(this);
            return;
        }

        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        MainCamera = Camera.main;

        p1Color = new Vector4(0, 237, 255, 100);

        p2Color = new Vector4(247, 0, 255, 100);

        isOdd = true;

        IDs = new int[9];

        turnNumber = 0;

        victext = new TextMeshProUGUI();

        eventoSistema = new GameObject();

        eventoSistema = GameObject.FindGameObjectWithTag("sistemaevento");

        victext = GameObject.FindGameObjectWithTag("winnertext").GetComponent<TextMeshProUGUI>();

        //StartCoroutine(eventoApagar());

        MainCamera.backgroundColor = Color.blue;

        StartCoroutine(GetValuesOrSomethingidfkanymore());

        hasGameStarted = false;
    }

    public IEnumerator eventoApagar(){
        yield return new WaitForSeconds(0.1f);

        eventoSistema.SetActive(false);
    }

    public IEnumerator GetValuesOrSomethingidfkanymore()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            for (int i = 0; i < IDs.Length; i++)
            {
                //Debug.Log(IDs[i]);
                //Debug.Log(Chat2.gato.board[i]);
                IDs[i] = Chat2.gato.board[i];
                UpdateValue?.Invoke(i, Chat2.gato.board[i]);
                
            }
            if (Chat2.gato.actual == 1)
            {
                //MainCamera.backgroundColor = p1Color;
                isOdd = true;
            }
            else
            {
                //MainCamera.backgroundColor = p1Color;
                isOdd = false;
            }
            yield return new WaitForSeconds(0.5f);
            CheckForWinner1();
            CheckForWinner2();
        }
    }
    void activarEvento(){
        string id = idAct;

        if (!hasGameStarted)
        {
            return;
        }

        if(Chat2.gato.actual%2 != 0)
        {
            MainCamera.backgroundColor = p1Color;
        }
        else
        {
            MainCamera.backgroundColor = p2Color;
        }

        if (id == "p1"){
            if(Chat2.gato.actual%2 != 0){
                eventoSistema.SetActive(true);
                
            }
            else
            {
                eventoSistema.SetActive(false);
            }
        }
        if(id == "p2"){
            if(Chat2.gato.actual%2 == 0){
                eventoSistema.SetActive(true);
            }
            else
            {
                eventoSistema.SetActive(false);
            }
        }
    }
    public void ChangeArr(int arrpos, int ply)
    {
        Debug.Log(turnNumber);
        IDs[arrpos] = ply;
        if(ply == 1)
        {
            SwitchID?.Invoke(arrpos,id);
            CheckForWinner1();
        }
        if (ply == 2)
        {
            SwitchID?.Invoke(arrpos,id);
            CheckForWinner2();
        }
    }

    private void Update()
    {
        activarEvento();
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
