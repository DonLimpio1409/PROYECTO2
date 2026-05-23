using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private List<GameObject> settingsSubMenus;  
    
    public GameObject black;
    public GameObject tempBlack;
    private Stack<GameObject> menuStack = new Stack<GameObject>();
    
    void Start()
    {
        PushMenu(mainMenu);
        StartCoroutine(WaitToTransition());
    }

    public void PushMenu(GameObject nextMenu)
    {
        if (nextMenu == null) return;

        if (menuStack.Count > 0 && menuStack.Peek() == nextMenu) return;

        if (menuStack.Count > 0)
        {
            menuStack.Peek().SetActive(false);
        }

        menuStack.Push(nextMenu);
        nextMenu.SetActive(true);
    }

    public void PopMenu()
    {
        if (menuStack.Count <= 1) return;

        GameObject current = menuStack.Pop();
        current.SetActive(false);

        menuStack.Peek().SetActive(true);
    }

    public void OpenSubMenu(GameObject subMenuToOpen)
    {
        foreach (GameObject subMenu in settingsSubMenus)
        {
            subMenu.SetActive(false);
        }
        subMenuToOpen.SetActive(true);
    }

    public void Play()
    {
        StopCoroutine(WaitTransition());
        black.GetComponent<Animator>().SetBool("In", true);
        StartCoroutine(WaitTransition());
    }

    public void Quit()
    {
        Application.Quit();
    }
    IEnumerator WaitTransition()
    {
        Debug.Log("Culo limpio");
        yield return new WaitForSeconds(2f);
        Debug.Log("Culo sucio");
        black.GetComponent<Animator>().SetBool("In", false);
        DOTween.KillAll();
        SceneManager.LoadScene(2);
    }

    public IEnumerator WaitToTransition()
    {
        black.GetComponent<Animator>().SetBool("Out", true);
        yield return new WaitForSeconds(1.2f);
        tempBlack.SetActive(false);
    }
}