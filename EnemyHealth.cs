using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public Slider enemyHealthBar;
    [SerializeField] public LayerMask bulletLayerMask;
    public int damageAmount = 10;
    
    void Start()
    {
        currentHealth = maxHealth;
        enemyHealthBar.maxValue = maxHealth;
        enemyHealthBar.value = currentHealth;
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

    public void TakeDamage()
    {
        currentHealth -= damageAmount;
        enemyHealthBar.value = currentHealth;
        if (currentHealth <= 0)
        {
            die();
        }
    }

    void die()
    {
        Destroy(gameObject);
    }
}

   