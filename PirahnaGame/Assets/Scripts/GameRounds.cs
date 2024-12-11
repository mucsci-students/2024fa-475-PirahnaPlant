using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class GameRounds : MonoBehaviour
{

    public int roundNumber = 0;
    public int maxRounds = 10;

    public float restTime = 60f;
    private float timer = 60f;
    // List of active enemies
    private List<GameObject> enemies { get; set; }

    public GameObject SpawnerSpawner;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (roundNumber > maxRounds)
        {
            // Win (Winning Screen)
        }
        // If all enemies are dead start next round after certain amount of time
        if (enemies.Count == 0)
        {
            if(timer == 0f)
            {
                RoundStart();
                timer = restTime;
            } else
            {
                timer -= Time.deltaTime;
            }
        }

    }
void RoundStart()
    {
        roundNumber++;
        // Instantiate Spawners for round

    }
}
