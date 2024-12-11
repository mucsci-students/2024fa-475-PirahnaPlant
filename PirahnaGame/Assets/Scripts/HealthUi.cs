using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthUI : MonoBehaviour
{
    public TextMeshProUGUI healthText;      
    public Health playerHealth;            

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

        UpdateHealthUI();  
    }

    void Update()
    {
        UpdateHealthUI();  
    }

    public void UpdateHealthUI()
    {
       
        if (playerHealth != null)
        {
            
            healthText.text = "Health: " + Mathf.Max(0, (int)playerHealth.currentHealth).ToString();
        }
    }
}