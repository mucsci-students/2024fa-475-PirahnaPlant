using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnScript : MonoBehaviour
{
    public GameObject player;          // The player GameObject to respawn
    public Transform respawnPoint;     // The position where the player should respawn

    void Start()
    {
        if (player == null)
        {
            Debug.LogError("Player object not assigned in the Inspector!");
        }
        if (respawnPoint == null)
        {
            Debug.LogError("Respawn point not assigned in the Inspector!");
        }
    }

    // Call this method when the player needs to respawn (e.g., upon death)
    public void RespawnPlayer()
    {
        if (player != null && respawnPoint != null)
        {
            // Reset the player's position to the respawn point
            player.transform.position = respawnPoint.position;
            player.GetComponent<Health>().currentHealth = player.GetComponent<Health>().maxHealth;
        }
        else
        {
            Debug.LogWarning("Player or Respawn Point not set.");
        }
    }

}
