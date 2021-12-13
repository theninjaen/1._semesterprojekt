using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class TutorialPlayButton : MonoBehaviour
{
    public string gameScene;

    private void Start()
    {

    }

    public void PlayGame()
    {
        SceneManager.LoadScene(sceneName:gameScene);
    }
}
