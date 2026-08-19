// respawn button
using system.collections;
using system.collections.generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour
{
    public GameObject Britboi;
    public PlayerHealth playerHealth;
    public Transform respawnPoint;

    public void Restart()
    {
        //bring player back(operation lasuraus)
        instantiate.gameObject.Britboi;
        // Move player back to the respawn point
        playerHealth.transform.position = respawnPoint.position;

        // Reset player health
        playerHealth.currentHealth = playerHealth.maxHealth;

        // Update the health bar
        playerHealth.healthbar.SetHealth(playerHealth.maxHealth);

        Debug.Log("Game restarted!");
    }
}
