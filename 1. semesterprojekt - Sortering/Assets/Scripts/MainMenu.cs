using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    public void NewGame()
    {
        SceneManager.LoadScene("10 Special test 1");
    }

    public void StartTurorial()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
