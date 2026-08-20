using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {   
        //loads scene 1 - level 1
        SceneManager.LoadSceneAsync(2);
    }

    public void controls()
    {
        //Loads scene 10 - which is the control screen
        SceneManager.LoadSceneAsync(1);
    }

    public void back()
    {
        //Loads scene 0 - which is the main menu
        SceneManager.LoadSceneAsync(0);
    }

    // Update is called once per frame
    public void QuitGame()
    {
        // Quits application - only works if built does not work in unity editor
        Application.Quit();
    }
}
