using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    public Transform Player; // Reference to the player GameObject
    public void LoadNextLevel()
    {
        // Get the current scene index
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Load the next scene based on the current scene index
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
    
}
