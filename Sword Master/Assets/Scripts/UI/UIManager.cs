using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
}