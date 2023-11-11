using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCoinScript : MonoBehaviour
{
    public GameObject theCoin;
    public GameObject Player;

    // Update is called once per frame
    void Update()
    {
        // Rotate the game object around its local Y-axis
        gameObject.transform.Rotate(0f, 1f, 0f, Space.Self);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("In collision");
            // Destroy the current coin
            Destroy(gameObject);

            // Instantiate a new coin at a random position
            Vector3 randomPosition = new Vector3(Random.Range(-5, 5), 1, Random.Range(-5, 5));
            Instantiate(theCoin, randomPosition, Quaternion.identity);
        }
    }
}
