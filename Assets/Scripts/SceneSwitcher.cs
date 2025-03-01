using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public void IniciarJuego(string name)
    {
        SceneManager.LoadScene(name);
    }
}
