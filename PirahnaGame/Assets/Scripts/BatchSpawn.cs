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

    public float moveAmount = 5.0f;                 // The amount to move
    public float turnAmount = 5.0f;					// The amount to turn

    public int numEnemies;

    GameObject RoundManager;
    RoundManager script;

    // Use this for initialization
    void Start()
    {
        RoundManager = GameObject.Find("RoundManager");
        script = RoundManager.GetComponent<RoundManager>();
        for (int i = 0; i < spawnCount; i++)
        {
            Spawn();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (numEnemies == 0)
        {
            Destroy(gameObject);
        }
    }

    void Spawn()
    {
        // First check to see if the prefab hasn't been set
        if (prefabToSpawn != null)
        {
            // Instantiate the prefab
            var enemy = Instantiate(prefabToSpawn, transform.position, Quaternion.identity);
            enemy.transform.SetParent(this.transform);
            numEnemies++;
            // Move and turn so that boxes don't keep spawning in the same spots
            transform.Translate(0, 0, moveAmount);
            transform.Rotate(0, turnAmount, 0);
        }
    }

    public void EnemyKilled()
    {
        numEnemies--;
    }

    private void OnDestroy()
    {
        script.SpawnerDestroyed();
    }
}
