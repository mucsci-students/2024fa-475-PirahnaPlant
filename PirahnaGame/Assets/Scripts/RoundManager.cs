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

    public GameObject core;

    public GameObject[] availableSpawners;

    // Start is called before the first frame update
    void Start()
    {   numSpawners = 0;
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
        if (core == null)
        {
            // Lose Game if core is destroyed
            // Switch to losing screen
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
                transform.Translate(0, 0, 0);
                transform.Rotate(0, 0, 0);
            }
        }
    }
    void RoundStart()
    {
        int maxSpawners;
        roundNumber++;
        if (roundNumber == 1)
        {
            maxSpawners = 3;
        } else
        {
            maxSpawners = roundNumber * 2 + 3;
        }
        // Instantiate Spawners for round
        // Move and turn so that boxes don't keep spawning in the same spots
        for (int i = 0; i < maxSpawners; i++)
        {
            SpawnSpawners();
        }
        
    }

    void SpawnSpawners()
    {
        int spawnerIndex;
        if (roundNumber == 1)
        {
            spawnerIndex = Random.Range(0, 2);
        } else if (roundNumber <= 3)
        {
            spawnerIndex = Random.Range(0, 6);
        } else if (roundNumber <= 5)
        {
            spawnerIndex = Random.Range(0, 12);
        }
        else
        {
            spawnerIndex = Random.Range(0, 18);
        }
        var spawner = Instantiate(availableSpawners[spawnerIndex], transform);
        // Randomize placement of spawner's x and z values
        float x = Random.Range(100, 150);
        float z = Random.Range(100, 150);
        float p = Random.Range(0,2);
        float s = Random.Range(0,2);
        if(p == 1){
            x = x * -1f;
        }
        if(s == 1){
            z = z * -1f;
        }
    
        spawner.transform.Translate(x, 1, z);
        spawner.transform.SetParent(this.transform);
        ++numSpawners;
    }

    public void SpawnerDestroyed()
    {
        numSpawners--;
    }
}