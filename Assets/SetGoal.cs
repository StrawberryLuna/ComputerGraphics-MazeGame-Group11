using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class setGoal : MonoBehaviour
{
    public GameObject goalText;

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            goalText.GetComponent<Text>();
            goalText.SetActive(true);

            Debug.Log("GOAL!");
        }
    }
}
