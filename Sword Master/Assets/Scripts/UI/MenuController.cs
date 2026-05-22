using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject pausaMenu;
    public GameObject fondPause;    
    public Animator upPause;
    public Animator downPause;
    int timesInMenu;
    void Update()
    {
        AperarMenuPausa();
    }
    public void ReStartLevel()
    {
        Time.timeScale = 1;
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

    public void ResumeGame()
    {
        Debug.Log("Despause");
        StartCoroutine(WaitToGoDown());
        fondPause.SetActive(false);
        downPause.GetComponent<Animator>().SetBool("In", false);
        upPause.GetComponent<Animator>().SetBool("In", false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void AperarMenuPausa()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && timesInMenu == 0)
        {
            StartCoroutine(WaitToStopUp());
            fondPause.SetActive(true);
            upPause.GetComponent<Animator>().SetBool("In", true);
            downPause.GetComponent<Animator>().SetBool("In", true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    IEnumerator WaitToStopUp()
    {
        timesInMenu = 1;
        yield return new WaitForSeconds(0.85f);
        Time.timeScale = 0;
    }
    IEnumerator WaitToGoDown()
    {
        Time.timeScale = 1;
        yield return new WaitForSeconds(0.85f);
        //Debug.Log("Coorutine");
        timesInMenu = 0;
    }
}
