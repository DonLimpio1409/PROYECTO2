using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System.Collections;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    
    public GameObject black;
    private Stack<GameObject> menuStack = new Stack<GameObject>();

    void Start()
    {
        PushMenu(mainMenu);
    }

    void Update() 
    {
        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (menuStack.Count > 1)
            {
                PopMenu();
            }
        }
    }

    public void PushMenu(GameObject nextMenu)
    {
        if (nextMenu == null) return;

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

    public void Play()
    {
        black.GetComponent<Animator>().SetBool("In", true);
        StartCoroutine(WaitTransition());
    }

    public void Quit()
    {
        Application.Quit();
    }
    IEnumerator WaitTransition()
    {
        yield return new WaitForSeconds(2f);
        black.GetComponent<Animator>().SetBool("In", false);
        SceneManager.LoadScene(1);
    }
}