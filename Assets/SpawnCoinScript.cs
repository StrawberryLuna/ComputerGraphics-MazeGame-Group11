using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpawnCoinScript : MonoBehaviour
{
    //public AudioSource collectionSoundEffect;
    public TextMeshProUGUI CoinText;
    private int coin = 0;
    void Update()
    {
        // Makes the coin spin
        gameObject.transform.Rotate(0f, 1f, 0f, Space.Self);
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.tag == "Player") //original
        if(other.transform.tag == "CoinTag")
        {
            coin++;
            CoinText.text = "Points: " + coin.ToString();
            Debug.Log("Collected coins: " + coin);

            //collectionSoundEffect.Play();

            Destroy(other.gameObject);


        }
    }
}