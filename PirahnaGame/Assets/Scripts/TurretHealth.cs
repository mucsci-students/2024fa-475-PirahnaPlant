using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretHealth : MonoBehaviour
{
    public float maxHealth = 300f;
    public float currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object that entered the collider has the 'enemy' tag
        if (other.CompareTag("Enemy"))
        {
            // Call the function to take damage
            TakeDamage(50f);  // Here, 50f is the damage per collision, adjust as needed
        }
    }

    // Function to handle the turret taking damage
    void TakeDamage(float damage)
    {
        // Decrease the current health by the damage amount
        currentHealth -= damage;

        // Check if the turret's health is zero or less
        if (currentHealth <= 0)
        {
            // Destroy the turret object
            Destroy(gameObject);
        }
    }
}
