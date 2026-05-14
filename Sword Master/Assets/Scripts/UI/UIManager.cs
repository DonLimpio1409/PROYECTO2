using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    
    private Stack<GameObject> menuStack = new Stack<GameObject>();

    void Start()
    {
        PushMenu(mainMenu);
    }

    void Update() 
    {
        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            PopMenu();
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
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Application.Quit();
    }
}