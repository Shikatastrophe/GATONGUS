using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IDAssigner : MonoBehaviour
{
    public int ID;

    public Color p1Color, p2Color;

    Image image;
    Button button;
    GameManager GManager;

    private void OnEnable()
    {
        GameManager.stopGame += disableButton;
        GameManager.UpdateValue += updateValue;
    }

    private void OnDisable()
    {
        GameManager.stopGame -= disableButton;
        GameManager.UpdateValue += updateValue;
    }

    private void Start()
    {
        image = gameObject.GetComponent<Image>();
        button = gameObject.GetComponent<Button>();
        GManager = GameManager.Instance;
        ID = ID - 1;
    }

    public void updateValue(int id, int val)
    {
        if (id == ID)
        {
            switch (val)
            {
                case 0:
                    button.interactable = true;
                    image.color = Color.black;
                    break;
                case 1:
                    image.color = p1Color;
                    disableButton();
                    break;
                case 2:
                    image.color = p2Color;
                    disableButton();
                    break;
            }
        }
    }

    public void ChangeColor()
    {
        if (GManager.isOdd)
        {
            image.color = p1Color;
            UpdateGM(1);
        }
        else if (!GManager.isOdd)
        {
            image.color = p2Color;
            UpdateGM(2);
        }
    }

    public void UpdateGM(int ply)
    {
        GManager.ChangeArr(ID,ply);
        GManager.switchBool();
        //update the value to the gamemanager
        disableButton();
    }

    public void disableButton()
    {
        button.interactable = false;
    }
}
