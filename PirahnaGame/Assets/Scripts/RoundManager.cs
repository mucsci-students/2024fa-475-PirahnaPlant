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
    public bool isboss = false;
    // List of active enemies
    public int numSpawners;

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
        if(roundNumber == 5 || roundNumber == 10){
            isboss = true;
        }
        else {
            isboss = false;
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
        roundNumber++;
        int maxSpawners = (3 * roundNumber) - roundNumber;
        // Instantiate Spawners for round
        // Move and turn so that boxes don't keep spawning in the same spots
        for (int i = 0; i <= maxSpawners; i++)
        {
            SpawnSpawners();
        }
        
    }

    void SpawnSpawners()
    {
        int rand = Random.Range(0, 16); // Change range later to specialize rounds later
        if(isboss){
            rand = 17;
        }
        var spawner = Instantiate(availableSpawners[rand], transform);
        
        // Randomize placement of spawner's x and z values
        float x = Random.Range(100, 125);
        float z = Random.Range(100, 125);
        float p = Random.Range(0,1);
        float s = Random.Range(0,1);
        if(p == 1){
            x = x * -1f;
        }
        if(s == 1){
            s = s * -1f;
        }
        //Vector3 parentPosition = this.transform.parent.position;
        //Vector3 pos = new Vector3((parentPosition.x + x), 100,( parentPosition.z + z));
    
        spawner.transform.Translate(x, 1, z);
        spawner.transform.SetParent(this.transform);
        ++numSpawners;
    }

    public void SpawnerDestroyed()
    {
        numSpawners--;
    }
}