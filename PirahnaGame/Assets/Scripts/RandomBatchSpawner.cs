using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBatchSpawner : MonoBehaviour
{
    public GameObject[] SpawnablePrefabs;                // Array of spawnable prefabs
    public int spawnCount;

    public float moveAmount = 5.0f;                 // The amount to move
    public float turnAmount = 5.0f;					// The amount to turn

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            Spawn();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Spawn()
    {
        // First check to see if the prefab hasn't been set
        if (SpawnablePrefabs != null)
        {
            int rand = Random.Range(0, 8);
            // Instantiate the prefab
            Instantiate(SpawnablePrefabs[rand], transform.position, Quaternion.identity);
            // Move and turn so that boxes don't keep spawning in the same spots
            transform.Translate(0, 0, moveAmount);
            transform.Rotate(0, turnAmount, 0);
        }
    }
}
