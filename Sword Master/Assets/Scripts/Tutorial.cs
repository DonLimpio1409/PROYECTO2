using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class Tutorial : MonoBehaviour
{
    bool enterTutorial = false;
    public bool tutorialDone = false;

    [Header("Instrucs Tutorial")]
    public GameObject instruc1;
    public GameObject instruc2;
    public GameObject instruc3;
    public GameObject instruc4;
    public GameObject instruc5;
    public GameObject instruc6;
    public GameObject instruc7;

    Queue<GameObject> tutorialList = new Queue<GameObject>();
    bool trialEndTutorial;
    public GameObject black;
    public GameObject player;
    public GameObject sword;

    void Awake()
    {
        black.GetComponent<Animator>().SetBool("Out", true);
    }
    void Start()
    {
        tutorialList.Enqueue(instruc1);
        tutorialList.Enqueue(instruc2); 
        tutorialList.Enqueue(instruc3);
        tutorialList.Enqueue(instruc4);
        tutorialList.Enqueue(instruc5);
        tutorialList.Enqueue(instruc6);
        tutorialList.Enqueue(instruc7);
        SoundController.Instance.PlayMusic(SoundController.Instance.TutorialMusic);

        StartCoroutine(WaitForScrollEnd());
    }
    void Update()
    {
        if(trialEndTutorial)
        {
            OnTutorial();
        }
        if(Input.GetMouseButtonDown(1) && trialEndTutorial)
        {
            tutorialDone = true;   
        }
        if(tutorialDone && tutorialList.Count > 0)
        {
            tutorialList.Peek().SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            enterTutorial = true;
        }
    } 

    void OnTutorial()
    {
        if (!enterTutorial) return;

        // Si es la primera vez, activa el primer elemento
        if (tutorialList.Count > 0 && !tutorialList.Peek().activeSelf)
        {
            tutorialList.Peek().SetActive(true);
        }

        // Avanzar al siguiente paso
        if (Input.GetMouseButtonDown(0))
        {
            if (tutorialList.Count > 0)
            {
                // Apagar el actual
                tutorialList.Peek().SetActive(false);
                tutorialList.Dequeue();
            }

            // Encender el siguiente si existe
            if (tutorialList.Count > 0)
            {
                tutorialList.Peek().SetActive(true);
            }
            else
            {
                tutorialDone = true;
            }
        }
    }

    public IEnumerator WaitAnimation()
    {
        yield return new WaitForSeconds(4f);
        sword.SetActive(true);
        yield return new WaitForSeconds(1f);
        trialEndTutorial = true;
    }

    public IEnumerator WaitForScrollEnd()
    {
        yield return new WaitForSeconds(1.5f);
        black.GetComponent<Animator>().SetBool("Out", false);
        player.GetComponent<Animator>().SetBool("DoneTutorial", true);
        StartCoroutine(WaitAnimation());
    }

}
