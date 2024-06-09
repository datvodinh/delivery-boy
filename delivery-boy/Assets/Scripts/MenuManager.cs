using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    public int gameStartScene;
    public void StartGame()
    {
        SceneManager.LoadSceneAsync("main");
    }

    public void QuitGame()
    {
        Application.Quit();
    }    
}