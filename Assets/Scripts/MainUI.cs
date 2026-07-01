using UnityEngine;
using UnityEngine.SceneManagement;

public class MainUI : MonoBehaviour
{
    public void playGame()
    {
        SceneManager.LoadScene(1); // Load the scene with index 1
    }

    public void back()
    {
        SceneManager.LoadScene(0); 
    }

    public void quitGame()
    {
        Application.Quit(); // Quit the application
    }

}
