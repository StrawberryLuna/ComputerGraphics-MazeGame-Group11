using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpawnCoinScript : MonoBehaviour
{
    public GameObject theCoin;
    public AudioSource collectionSoundEffect;
    public TextMeshProUGUI coinText;
    private int collectedCoins = 0;
    void Update()
    {
        // Makes the coin spin
        gameObject.transform.Rotate(0f, 1f, 0f, Space.Self);
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.tag == "Player") //original
        if (other.gameObject.CompareTag("Player"))
        {
            collectedCoins++;
            coinText.text = "Points: " + collectedCoins.ToString();
            Debug.Log("Collected coins: " + collectedCoins);

            collectionSoundEffect.Play();
            
            // Destroys coin
            Destroy(gameObject);

            // Instantiate a new coin at a random position
            Vector3 randomPosition = new Vector3(Random.Range(-5, 5), 1, Random.Range(-5, 5));
            Instantiate(theCoin, randomPosition, Quaternion.identity);
            
        }
    }
}