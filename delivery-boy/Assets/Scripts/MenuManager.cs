using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private string mainSceneName = "main";
    private string mainMenuSceneName = "main_menu";
    private string pauseScreenName = "pause_screen";

    public void StartGame()
    {
        SwitchScene(mainSceneName);
        Time.timeScale = 1f; // Resume the game
    }

    public void MainMenu()
    {
        SwitchScene(mainMenuSceneName);
    }

    public void ResumeGame()
    {
        StartCoroutine(UnloadPauseScreen());
        Time.timeScale = 1f; // Resume the game
    }

    private IEnumerator UnloadPauseScreen()
    {
        AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync(pauseScreenName);
        while (!asyncUnload.isDone)
        {
            yield return null;
        }
    }

    public void PauseGame()
    {
        SceneManager.LoadScene(pauseScreenName, LoadSceneMode.Additive);
        Time.timeScale = 0f; // Pause the game
    }

    public void LevelScreen()
    {
        SwitchScene("level_screen");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SwitchScene(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
    }
}