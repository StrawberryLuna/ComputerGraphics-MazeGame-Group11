using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public GameObject pauseMenu;

    public void OnClickPause()
    {
        // on click, pause the game if running
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
        }
    }
}