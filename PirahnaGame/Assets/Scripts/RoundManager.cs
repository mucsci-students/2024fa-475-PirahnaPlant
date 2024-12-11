using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class RoundManager : MonoBehaviour
{

    public int roundNumber = 0;
    public int maxRounds = 10;
    public float moveAmount = 5.0f;                 // The amount to move
    public float turnAmount = 5.0f;                 // The amount to turn
    public float restTime = 10f;
    public float timer = 0f;
    // List of active enemies
    public int numSpawners;

    public GameObject[] availableSpawners;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0f;
        RoundStart();
    }

    // Update is called once per frame
    void Update()
    {
        if (roundNumber > maxRounds)
        {
            // Win (Add Winning Screen)
        }
        // If all enemies are dead start next round after certain amount of time
        if (numSpawners == 0)
        {
            if (timer >= restTime)
            {
                RoundStart();
                timer = 0f;
            }
            else
            {
                // Move round manager to randomize where spawners are placed
                timer += Time.deltaTime;
                transform.Translate(0, 0, moveAmount);
                transform.Rotate(0, turnAmount, 0);
            }
        }
    }
    void RoundStart()
    {
        roundNumber++;
        int maxSpawners = roundNumber;
        // Instantiate Spawners for round
        // Move and turn so that boxes don't keep spawning in the same spots
        for (int i = 0; i <= maxSpawners; i++)
        {
            SpawnSpawners();
        }
        
    }

    void SpawnSpawners()
    {
        int rand = Random.Range(0, 17); // Change range later to specialize rounds later
        var spawner = Instantiate(availableSpawners[rand], transform);
        // Randomize placement of spawner's x and z values
        float x = Random.Range(-50, 50);
        float z = Random.Range(-50, 50);
        Vector3 pos = new Vector3(x, 5, z);
        spawner.transform.position = pos;
        spawner.transform.SetParent(this.transform);
        ++numSpawners;
    }

    public void SpawnerDestroyed()
    {
        numSpawners--;
    }
}