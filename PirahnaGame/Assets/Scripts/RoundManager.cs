using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class RoundManager : MonoBehaviour
{
    public int roundNumber = 0;
    public int maxRounds = 10;
    public float moveAmount = 5.0f;
    public float turnAmount = 5.0f;
    public float restTime = 10f;
    public float timer = 0f;
    public bool isboss = false;
    public int numSpawners;
    public GameObject core;
    public GameObject[] availableSpawners;

    // Reference to TextMesh for displaying the message
    public TextMeshProUGUI roundCompleteText;
    public TextMeshProUGUI gameOverText;

    // Start is called before the first frame update
    void Start()
    {
        numSpawners = 0;
        timer = 0f;
        RoundStart();
    }

    // Update is called once per frame
    void Update()
    {
        if (roundNumber > maxRounds)
        {
            // Show "Round Complete" message for 5 seconds and then go to main menu
            StartCoroutine(ShowGameCompleteMessage());
        }

        if(roundNumber == 4 || roundNumber == 9 || roundNumber == 14)
        {
            isboss = true;
        }
        else 
        {
            isboss = false;
        }

        if (core == null)
        {
            // Lose Game if core is destroyed
            // Switch to losing screen
            StartCoroutine(ShowOverMessage());
        }

        // If all enemies are dead start next round after a certain amount of time
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
        } 
        else
        {
            maxSpawners = roundNumber * 2 + 3;
        }

        // Instantiate Spawners for round
        for (int i = 0; i < maxSpawners; i++)
        {
            SpawnSpawners();
        }
    }

    void SpawnSpawners()
    {
        int spawnerIndex;
        if(isboss)
        {
            spawnerIndex = 17;
            isboss = false;
        }
        else if (roundNumber == 1)
        {
            spawnerIndex = Random.Range(0, 2);
        } 
        else if (roundNumber <= 3)
        {
            spawnerIndex = Random.Range(0, 6);
        } 
        else if (roundNumber <= 5)
        {
            spawnerIndex = Random.Range(0, 12);
        }
        else if(isboss)
        {
            spawnerIndex = 17;
        }
        else 
        {
            spawnerIndex = Random.Range(0, 17);
        }

        var spawner = Instantiate(availableSpawners[spawnerIndex], transform);

        // Randomize placement of spawner's x and z values
        float x = Random.Range(100, 150);
        float z = Random.Range(100, 150);
        float p = Random.Range(0, 2);
        float s = Random.Range(0, 2);
        if(p == 1)
        {
            x = x * -1f;
        }
        if(s == 1)
        {
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

    // Coroutine to show the "Round Complete" message for 5 seconds and then load the main menu
    IEnumerator ShowGameCompleteMessage()
    {
        // Create and show the pop-up text
        roundCompleteText.gameObject.SetActive(true);
        roundCompleteText.text = "Congratulations!! \nYou have destroyed every cute creature \nand can now destroy the forest!!";

        // Wait for 5 seconds
        yield return new WaitForSeconds(8f);

        // Hide the text mesh after 5 seconds
        roundCompleteText.gameObject.SetActive(false);

        // Load the main menu (Scene 0)
        SceneManager.LoadScene(0);
    }

    IEnumerator ShowOverMessage()
    {
        // Create and show the pop-up text
        gameOverText.gameObject.SetActive(true);
        gameOverText.text = "The generator was destroyed.\nBetter luck next time.";

        // Wait for 5 seconds
        yield return new WaitForSeconds(8f);

        // Hide the text mesh after 5 seconds
        gameOverText.gameObject.SetActive(false);

        // Load the main menu (Scene 0)
        SceneManager.LoadScene(0);
    }
}
