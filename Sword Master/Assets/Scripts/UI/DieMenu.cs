using UnityEngine;
using UnityEngine.SceneManagement;

public class DieMenu : MonoBehaviour
{
    public void ReStartLevel()
    {
        int activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(activeSceneIndex);
    }
    public void ExitGame()
    {
        Application.Quit();
    }

    public void TurnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
