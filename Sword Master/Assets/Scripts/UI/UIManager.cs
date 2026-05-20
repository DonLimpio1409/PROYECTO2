using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System.Collections;
using JetBrains.Annotations;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private List<GameObject> settingsSubMenus;  
    
    public GameObject black;
    private Stack<GameObject> menuStack = new Stack<GameObject>();
    public static UIManager Instance;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
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