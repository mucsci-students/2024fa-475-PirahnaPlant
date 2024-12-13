using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool paused = false;
    public GameObject PauseMenuCanvas;

    // Reference to the mouse cursor visibility and lock state
    private bool cursorWasLocked;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the time scale to normal (game running) and ensure cursor is locked initially
        //Time.timeScale = 1f;
        PauseMenuCanvas.SetActive(false);
        cursorWasLocked = Cursor.lockState == CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused)
            {
                Play();
            }
            else
            {
                Stop();
            }
        }
    }

    // Stop the game, show the pause menu, and unlock the cursor
    void Stop()
    {
        PauseMenuCanvas.SetActive(true);
        Time.timeScale = 0f;
        paused = true;
        
        // Unlock the cursor and make it visible
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Resume the game, hide the pause menu, and lock the cursor
    public void Play()
    {
        PauseMenuCanvas.SetActive(false);
        Time.timeScale = 1f;
        paused = false;

        // Restore the cursor lock state to what it was before the pause
        if (cursorWasLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public void QuitToMenuButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
