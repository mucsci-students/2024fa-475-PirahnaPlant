using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public GameObject[] SpawnablePrefabs;                // The prefab that should be spawned
    public float spawnFrequency = 6.0f;             // The time (in seconds) between spawns
    public bool spawnOnStart = false;               // Whether or not one instance of the prefab should be spawned on Start()
    public bool move = true;                        // Move this spawn spot around
    public float moveAmount = 5.0f;                 // The amount to move
    public float turnAmount = 5.0f;                 // The amount to turn

    private float spawnTimer = 0.0f;
    // Destroy Spawner when all enemies have been spawned
    public int maxSpawn = 10;
    private int spawnCounter = 0;

    // Track enemies Spawned
    public int numEnemies;

    public GameObject roundManager;
    private RoundManager script;

    // Use this for initialization
    void Start()
    {
        roundManager = GameObject.Find("RoundManager");
        script = roundManager.GetComponent<RoundManager>();
        if (spawnOnStart)
        {
            Spawn();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Update the spawning timer
        spawnTimer += Time.deltaTime;

        // Spawn a prefab if the timer has reached spawnFrequency
        if (spawnTimer >= spawnFrequency && spawnCounter <= maxSpawn)
        {
            // First reset the spawn timer to 0
            spawnTimer = 0.0f;
            Spawn();
        }
        /*
        // Move and turn so that boxes don't keep spawning in the same spots
        transform.Translate(0, 0, moveAmount);
        transform.Rotate(0, turnAmount, 0);
        */
        if (spawnCounter >= maxSpawn && numEnemies == 0)
        {
            Destroy(gameObject);
        }
    }

    void Spawn()
    {
        // First check to see if the prefab hasn't been set
        if (SpawnablePrefabs != null)
        {
            int rand = Random.Range(0, 8);
            // Instantiate the prefab
            
            var enemy = Instantiate(SpawnablePrefabs[rand], transform.position, Quaternion.identity);
            enemy.transform.SetParent(this.transform);
            numEnemies++;
            
            //Instantiate(SpawnablePrefabs[rand], transform.position, Quaternion.identity);
            spawnCounter++;
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
