using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    void Start()
    {
        // Set the target frame rate to 60 FPS which reduces frame drops and improves performance on lower end devices
        Application.targetFrameRate = 60;
    }
    
    public void PlayGame()
    {   
        //loads scene 5 - level 1
        SceneManager.LoadSceneAsync(5);
    }

    public void controls()
    {
        //Loads scene 2 - which is the controls screen
        SceneManager.LoadSceneAsync(2);
    }

    public void back()
    {
        //Loads scene 1 - which is the main menu
        SceneManager.LoadSceneAsync(1);
    }

    public void QuitGame()
    {
        // Quits application - only works if built does not work in unity editor
        Application.Quit();
    }
}
