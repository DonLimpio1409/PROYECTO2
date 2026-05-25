using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject pausaMenu;
    public GameObject fondPause;
    public GameObject tempBlackDie;
    public Animator dieMenu;  
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
        SoundController.Instance.PlaySFX(SoundController.Instance.buttonIn);
        tempBlackDie.SetActive(true);
        dieMenu.SetBool("ReStart", true);
        StartCoroutine(WaitLoadScene());
    }
    public void ExitGame()
    {
        SoundController.Instance.PlaySFX(SoundController.Instance.buttonOut);
        Application.Quit();
    }

    public void TurnToMainMenu()
    {
        SoundController.Instance.PlaySFX(SoundController.Instance.buttonOut);
        SceneManager.LoadScene(1);

    }

    public void ResumeGame()
    {
        SoundController.Instance.PlaySFX(SoundController.Instance.buttonOut);
        SoundController.Instance.DecideFootstepSound();
        fondPause.SetActive(false);
        downPause.GetComponent<Animator>().SetBool("In", false);
        upPause.GetComponent<Animator>().SetBool("In", false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1;
        timesInMenu = 0;
    }

    void AperarMenuPausa()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && timesInMenu == 0)
        {
            SoundController.Instance.PlaySFX(SoundController.Instance.buttonIn);
            SoundController.Instance.footstepAudioSource.Stop();
            fondPause.SetActive(true);
            upPause.GetComponent<Animator>().SetBool("In", true);
            downPause.GetComponent<Animator>().SetBool("In", true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            timesInMenu = 1;
            Time.timeScale = 0;
        }
    }

    public void Lvl1()
    {
        Time.timeScale = 1;
        SoundController.Instance.PlaySFX(SoundController.Instance.buttonIn);
        SceneManager.LoadScene("1 Tutorial");
    }
    public void Lvl2()
    {
        Time.timeScale = 1;
        SoundController.Instance.PlaySFX(SoundController.Instance.buttonIn);
        SceneManager.LoadScene("2 Level 1");
    }
    public void Lvl3()
    {
        Time.timeScale = 1; 
        SoundController.Instance.PlaySFX(SoundController.Instance.buttonIn);
        SceneManager.LoadScene("3 Level 2");
    }
    IEnumerator WaitLoadScene()
    {
        yield return new WaitForSeconds(1f);
        int activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(activeSceneIndex);
        tempBlackDie.SetActive(true);
    }
}
