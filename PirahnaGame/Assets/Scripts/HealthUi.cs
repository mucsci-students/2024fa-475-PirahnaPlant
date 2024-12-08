using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthUI : MonoBehaviour
{
    public TextMeshProUGUI healthText;      // UI element to display health
    public Health playerHealth;            // Reference to the player's Health component

    void Start()
    {
        if (playerHealth == null)
        {
            Debug.LogError("No Health component assigned to HealthUI.");
            return;
        }

        if (healthText == null)
        {
            Debug.LogError("No health UI Text element assigned.");
            return;
        }

        UpdateHealthUI();  // Initialize the health UI at the start
    }

    void Update()
    {
        UpdateHealthUI();  // Continuously update the health UI in every frame
    }

    public void UpdateHealthUI()
    {
        // Update the health UI text
        if (playerHealth != null)
        {
            // Ensure the current health is not negative
            healthText.text = "Health: " + Mathf.Max(0, (int)playerHealth.currentHealth).ToString();
        }
    }
}