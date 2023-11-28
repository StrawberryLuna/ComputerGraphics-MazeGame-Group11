using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    public GameObject pauseMenu;

    public void OnClickHome()
    {
        SceneManager.LoadScene("StartMenu");
        if (Time.timeScale == 0)
            Time.timeScale = 1;
    }

    public void OnClickPlay()
    {
        SceneManager.LoadScene("MazeScene");
    }

    public void OnClickPause()
    {
        // on click, pause the game if running
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
        }
    }

    public void OnClickResume()
    {
        // on click, resume the game
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
        }
    }

    public void OnClickRestart()
    {
        // reset everthing to the MazeScene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    public void OnClickQuit()
    {
        // terminate the game
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
