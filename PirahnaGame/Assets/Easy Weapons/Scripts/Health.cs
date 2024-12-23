﻿/// <summary>
/// Health.cs
/// Author: MutantGopher
/// This is a sample health script.  If you use a different script for health,
/// make sure that it is called "Health".  If it is not, you may need to edit code
/// referencing the Health component from other scripts
/// </summary>

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
	//public FirstPersonCharacter player;
	public GameObject moneyManager;
	public RespawnScript respawnScript;
	public int mobMoneyValue1 = 5;
	public int mobMoneyValue2 = 0;
	public bool canDie = true;					// Whether or not this health can die
	public float startingHealth = 100.0f;		// The amount of health to start with
	public float maxHealth = 100.0f;			// The maximum amount of health
	public float currentHealth;				// The current ammount of health

	public bool replaceWhenDead = false;		// Whether or not a dead replacement should be instantiated.  (Useful for breaking/shattering/exploding effects)
	public GameObject deadReplacement;			// The prefab to instantiate when this GameObject dies
	public bool makeExplosion = false;			// Whether or not an explosion prefab should be instantiated
	public GameObject explosion;				// The explosion prefab to be instantiated

	public bool isCube = false;
	public bool isTurret = false;
	public bool isPlayer = false;               // Whether or not this health is the player
	public bool isEnemy = false;                // Whether or not this health is an enemy
	public bool isCore = false;					// Whether or not this health is the core
	public GameObject deathCam;					// The camera to activate when the player dies

	private bool diedFalling = false;
	private bool dead = false;                  // Used to make sure the Die() function isn't called twice

	[SerializeField] Slider healthbar;

	private EnemyAnimations animScript;

	private Animator anim;

	// Used to access the parent spawner's script of the enemy
	private BatchSpawn parentBatchSpawner;
	private RandomBatchSpawner parentRandBatchSpawner;
	private RandomSpawner parentRandSpawner;
	private Spawner parentSpawner;

	// Use this for initialization
	void Start()
	{
		// Initialize the currentHealth variable to the value specified by the user in startingHealth
		currentHealth = maxHealth;
		if (GetComponent<Slider>() != null)
		{
            healthbar.maxValue = maxHealth;
            healthbar.value = currentHealth;
        }
		if (GetComponent<Animator>() != null)
		{
			animScript = GetComponent<EnemyAnimations>();
			anim = GetComponent<Animator>();
		}
		// Get Enemies parent spawner's script
        if (transform.parent.GetComponent<BatchSpawn>() != null)
        {
            parentBatchSpawner = transform.parent.GetComponent<BatchSpawn>();
        }
        if (transform.parent.GetComponent<RandomBatchSpawner>() != null)
		{
			parentRandBatchSpawner = transform.parent.GetComponent<RandomBatchSpawner>();
		}
		if (transform.parent.GetComponent<RandomSpawner>() != null)
		{
			parentRandSpawner = transform.parent.GetComponent<RandomSpawner>();
		}
        if (transform.parent.GetComponent<Spawner>() != null)
		{
            parentSpawner = transform.parent.GetComponent<Spawner>();
        }

    }

	void Update(){
		if (GetComponent<Slider>() != null)
		{
            healthbar.maxValue = maxHealth;
            healthbar.value = currentHealth;
        }
		if ((transform.position.y <= -10) && isEnemy)
		{
			diedFalling = true;
			Die();
		}
	}
	public void ChangeHealth(float amount)
	{
		if (isCore)
		{
            if (amount > 0)
            {
                currentHealth += amount;
                if (healthbar != null)
                {
                    healthbar.value = currentHealth;
                }
            }
            else
            {
                // Do Nothing
            }
        }
		else if (isTurret)
		{
			if (amount > 0)
			{
                currentHealth += amount;
                if (healthbar != null)
                {
                    healthbar.value = currentHealth;
                }
            } else
			{
                // Do Nothing
            }
        }
		else if (isPlayer)
		{
			// Do Nothing
		} 
		else
		{
			// Change the health by the amount specified in the amount variable
			currentHealth += amount;
			if (healthbar != null)
			{
				healthbar.value = currentHealth;
			}
			// If the health runs out, then Die.
			if (currentHealth <= 0 && !dead && canDie)
				Die();

			else if (currentHealth <= 0 && isPlayer)
			{
				if (respawnScript != null)
				{
					currentHealth = maxHealth;
					respawnScript.RespawnPlayer();  // Respawn at the designated respawn point
				}

			}			
		}
        // Make sure that the health never exceeds the maximum health
        if (currentHealth > maxHealth)
		{
            currentHealth = maxHealth;
        }
            
    }

	public void Die()
	{
		// This GameObject is officially dead.  This is used to make sure the Die() function isn't called again
		dead = true;
		// Make death effects
		if (replaceWhenDead)
			Instantiate(deadReplacement, transform.position, transform.rotation);
		if (makeExplosion)
			Instantiate(explosion, transform.position, transform.rotation);
		if(isEnemy && !diedFalling){
			if (mobMoneyValue1 == 0)
        {
            MoneyScript.Instance.updateMoney(mobMoneyValue2);  // Update money for mobMoneyValue2
        }
        else if (mobMoneyValue2 == 0)
        {
            MoneyScript.Instance.updateMoney(mobMoneyValue1);  // Update money for mobMoneyValue1
        }
		}

		if (isPlayer && deathCam != null)
			deathCam.SetActive(true);
		if (isEnemy && !(isCube))
		{
			anim.SetBool("Dead", true);
			Invoke("DestroyObject", 0.5f);
		} else
		{
            // Remove this GameObject from the scene
            Destroy(gameObject);
        }
	}

	private void DestroyObject()
	{
        // Remove this GameObject from the scene
        Destroy(gameObject);
    }

	public float getCurrentHealth()
	{
		return currentHealth;
	}

	// When enemy is destroyed call EnemyKilled in the parent spawner
    private void OnDestroy()
    {
		
		if (parentSpawner != null)
		{
			parentSpawner.EnemyKilled();
		} else if (parentRandBatchSpawner != null)
		{
			parentRandBatchSpawner.EnemyKilled();
		} else if (parentBatchSpawner != null)
		{
            parentBatchSpawner.EnemyKilled();
        } else if(parentRandSpawner != null)
		{
			parentRandSpawner.EnemyKilled();
		}
    }

	public void CoreDamage(float amount)
	{
		// Change the health by the amount specified in the amount variable
		currentHealth += amount;
		if (healthbar != null)
		{
			healthbar.value = currentHealth;
		}
		// If the health runs out, then Die.
		if (currentHealth <= 0 && !dead && canDie) { 
			Die();
        }
		// Make sure that the health never exceeds the maximum health
		if (currentHealth > maxHealth)
		{
			currentHealth = maxHealth;
		}
    }

	public void TurretDamage(float amount)
	{
        // Change the health by the amount specified in the amount variable
        currentHealth += amount;
        if (healthbar != null)
        {
            healthbar.value = currentHealth;
        }
        // If the health runs out, then Die.
        if (currentHealth <= 0 && !dead && canDie)
        {
            Die();
        }
        // Make sure that the health never exceeds the maximum health
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

	public void PlayerDamage(float amount)
	{
        // Change the health by the amount specified in the amount variable
        currentHealth += amount;
        if (healthbar != null)
        {
            healthbar.value = currentHealth;
        }
        // If the health runs out, then Die.
        if (currentHealth <= 0 && !dead && canDie)
            Die();

        else if (currentHealth <= 0 && isPlayer)
        {
            if (respawnScript != null)
            {
                currentHealth = maxHealth;
                respawnScript.RespawnPlayer();  // Respawn at the designated respawn point
            }
        }
        // Make sure that the health never exceeds the maximum health
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }
}
