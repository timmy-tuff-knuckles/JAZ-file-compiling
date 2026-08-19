using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] public LayerMask enemyLayerMask;
    public Slider healthSlider;
    public int currentHealth;
    private int maxHealth = 100;
    public int damageAmount = 20;
    private float waitTime = 1;

    bool isEnemyTouching()
    {
        return Physics2D.OverlapCircle(transform.position, 1f, enemyLayerMask) != null;
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
                Destroy(gameObject);
            }

            waitTime = 1; // Reset the wait time to 1 second after taking damage
        }
    }
}