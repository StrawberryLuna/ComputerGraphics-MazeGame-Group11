using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class setGoal : MonoBehaviour
{
    public GameObject goalText;
    public GameObject player;
    public GameObject pauseButton;
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            // show GoalText
            goalText.GetComponent<Text>();
            goalText.SetActive(true);
            // hide Player
            player.SetActive(false);
            // hide PauseButton
            pauseButton.SetActive(false);
            // pause the game
            Time.timeScale = 0;
        }
    }
}
