using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100; // Maximum health of the enemy
    private int currentHealth; // Current health of the enemy
    public Slider enemyHealthBar; // Reference to the UI Slider that represents the enemy's health bar
    [SerializeField] public LayerMask bulletLayerMask; // LayerMask to determine what can damage the enemy
    public int damageAmount = 10; // Amount of damage the enemy takes when hit by a bullet
    public int points; // Points awarded to the player when the enemy is defeated
    
    void Start()
    {
        gameObject.SetActive(true); // Ensure the enemy is active at the start
        currentHealth = maxHealth;
        enemyHealthBar.maxValue = maxHealth; // Set the max value of the health bar to the max health
        enemyHealthBar.value = currentHealth; // Set the current value of the health bar to the current health
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the thing that hit us is on the bullet layer
        if (((1 << other.gameObject.layer) & bulletLayerMask) != 0)
        {
            TakeDamage();
            Destroy(other.gameObject); // destroy the bullet
        }
    }

    public void TakeDamage() // Checks if the enemy can take damage and applies the damage to the enemy's health
    {
        currentHealth -= damageAmount;
        enemyHealthBar.value = currentHealth;
        if (currentHealth <= 0)
        {
            StartCoroutine(die());
        }
    }

    public IEnumerator die()
    {
        yield return new WaitForSeconds(0.2f); // Wait for 0.2 seconds before disabling the enemy
        gameObject.SetActive(false); // Disable the enemy game object
        ScoreManager.instance.AddPoint(points); // Add points to the player's score when the enemy is defeated
    }
}

   