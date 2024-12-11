using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class RoundManager : MonoBehaviour
{

    public int roundNumber = 0;
    public int maxRounds = 10;

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
                timer += Time.deltaTime;
            }
        }
    }
    void RoundStart()
    {
        roundNumber++;
        
        int maxSpawners = roundNumber;
        // Instantiate Spawners for round
        for (int i = 0; i < maxSpawners; i++)
        {
            SpawnSpawners();
        }
        
    }

    void SpawnSpawners()
    {
        int rand = Random.Range(0, 18); // Change range later to specialize rounds later
        var spawner = Instantiate(availableSpawners[19], transform);
        spawner.transform.SetParent(this.transform);
        ++numSpawners;
    }

    public void SpawnerDestroyed()
    {
        numSpawners--;
    }
}