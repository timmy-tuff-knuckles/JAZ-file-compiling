//player health
using system.collections;
using system.collections.generic;
using UnityEngine;
using UnityEngine.UI;





public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public Healthbar healthbar;

    void Start()
    {
        currentHealth = maxHealth;

        healthbar.SetMaxHealth(maxHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // Prevent health going below 0
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        // Update the health bar
        healthbar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        currentHealth += amount;

        // Prevent health going above max health
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        healthbar.SetHealth(currentHealth);
    }

    void Die()
    {
        Debug.Log("Player died!");

        // Put your death code here
        // Example:
        // Destroy(gameObject);
    }
}
