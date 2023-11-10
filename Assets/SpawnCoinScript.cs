using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCointScript : MonoBehaviour
{
    public GameObject coinPrefab;

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Vector3 randomPosition = new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10));
            Instantiate(coinPrefab, randomPosition, Quaternion.identity);
            // Optionally, you can destroy the coin after spawning to prevent multiple spawns in rapid succession.
            Destroy(gameObject);
        }
    }
}
