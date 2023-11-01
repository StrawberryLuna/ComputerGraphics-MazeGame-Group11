using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResumeGame : MonoBehaviour
{
    public GameObject pauseMenu;
    public void OnClickResume()
    {
        // on click, resume the game
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
        }
    }
}