using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {   
        //loads scene 1 - level 1
        SceneManager.LoadSceneAsync(1);
    }

    // Update is called once per frame
    public void QuitGame()
    {
        // Quits application - only works if built does not work in unity editor
        Application.Quit();
    }
}
