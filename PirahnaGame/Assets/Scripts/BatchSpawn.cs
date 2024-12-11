using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Spawn that came with Weapons works on a timer
// This script spawns a batch of enemies when it is activated
// Use both for rounds?
public class BatchSpawn : MonoBehaviour
{
    public GameObject prefabToSpawn;                // The prefab that should be spawned
    public int spawnCount;

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
        if (prefabToSpawn != null)
        {
            // Instantiate the prefab
            Instantiate(prefabToSpawn, transform.position, Quaternion.identity);
        }
    }
}
