using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] public LayerMask enemyLayerMask; // LayerMask to determine what can damage the player
    [SerializeField] public LayerMask voidLayerMask; // LayerMask to determine what can cause void damage
    [SerializeField] public LayerMask nextLevelLayerMask; // LayerMask to determine what can cause level change
    public Slider healthSlider; // Reference to the UI Slider that represents the player's health bar
    public int currentHealth; // Current health of the player
    private int maxHealth = 100; // Maximum health of the player
    public int damageAmount = 20; // Amount of damage the player takes when hit by an enemy
    public int voidDamageAmount = 75; // Amount of damage the player takes when touching the void
    private float waitTime = 1; // Time in seconds to wait before taking damage again

    // Checks if the player is touching an enemy by checking for a collision with the damging layers
    bool isEnemyTouching()
    {
        return Physics2D.OverlapCircle(transform.position, 3f, enemyLayerMask) != null;
    }

    bool isVoidTouching()
    {
        return Physics2D.OverlapCircle(transform.position, 1.5f, voidLayerMask) != null;
    }

    bool isLevelChangeTouching()
    {
        return Physics2D.OverlapCircle(transform.position, 1.5f, nextLevelLayerMask) != null;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (waitTime > 0)
        {
            waitTime = waitTime - Time.deltaTime;
        }
        else
        {
            TakeDamage();
            VoidDamage();
            ChangeLevel();
        }
    }
    
    public void die() // Loads the death screen 
    {
        SceneManager.LoadScene(3); // Load the death screen scene
    }

    public void VoidDamage()
    {
        bool isTouching = isVoidTouching();

        if (isTouching)
        {
            currentHealth -= voidDamageAmount;
            healthSlider.value = currentHealth;

            if (currentHealth <= 0)
            {
                die();
            }

            waitTime = 0.25f; // Reset the wait time to 0.25 seconds after taking damage
        }
    }

    public void TakeDamage()
    {
        bool isTouching = isEnemyTouching();

        if (isTouching)
        {
            currentHealth -= damageAmount;
            healthSlider.value = currentHealth;

            if (currentHealth <= 0)
            {
                die();
            }

            waitTime = 1; // Reset the wait time to 1 second after taking damage
        }
    }

    public void ChangeLevel()
    {
        bool isTouching = isLevelChangeTouching();
        

        if (isTouching)
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
    }
}