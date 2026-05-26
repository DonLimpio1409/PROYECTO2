using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    public bool tutorialDone = false;
    public bool lvl1PresentationDone = false;
    public bool lvl2PresentationDone = false;

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
    public GameObject zanTalkAudioSource;

    void Awake()
    {
        black.GetComponent<Animator>().SetBool("Out", true);
        sword.SetActive(false);
    }
    void Start()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "1 Tutorial":
                tutorialList.Enqueue(instruc1);
                tutorialList.Enqueue(instruc2); 
                tutorialList.Enqueue(instruc3);
                tutorialList.Enqueue(instruc4);
                tutorialList.Enqueue(instruc5);
                tutorialList.Enqueue(instruc6);
                tutorialList.Enqueue(instruc7);
                SoundController.Instance.PlayMusic(SoundController.Instance.TutorialMusic);

                StartCoroutine(WaitForScrollEndTutorial());
            break;

            case "2 Level 1":
                tutorialList.Enqueue(instruc1);
                SoundController.Instance.PlayMusic(SoundController.Instance.Level1Music);
                StartCoroutine(WaitForScrollEndLevel1());
            break;

            case "3 Level 2":
                tutorialList.Enqueue(instruc1);
                SoundController.Instance.PlayMusic(SoundController.Instance.Level2Music);
                StartCoroutine(WaitForScrollEndLevel2());
            break;
        }
    }
    void Update()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "1 Tutorial":
                if(trialEndTutorial)
                {
                    OnTutorial();
                }
                if(Input.GetKeyDown(KeyCode.Space) && trialEndTutorial)
                {
                    tutorialDone = true;   
                }
                if(tutorialDone && tutorialList.Count > 0)
                {
                    tutorialList.Peek().SetActive(false);
                }
            break;

            case "2 Level 1":
                if(trialEndTutorial)
                {
                    OnTutorial();
                }
                if(Input.GetMouseButtonDown(1) && trialEndTutorial)
                {
                    lvl1PresentationDone = true;   
                }
                if(lvl1PresentationDone && tutorialList.Count > 0)
                {
                    tutorialList.Peek().SetActive(false);
                }

                if(trialEndTutorial && Input.GetMouseButtonDown(0))
                {
                    player.GetComponent<Animator>().SetBool("DoneLevel1", false);
                    lvl1PresentationDone = true;
                }
            break;

            case "3 Level 2":
                if(trialEndTutorial)
                {
                    OnTutorial();
                }
                if(Input.GetMouseButtonDown(1) && trialEndTutorial)
                {
                    lvl2PresentationDone = true;   
                }
                if(lvl2PresentationDone && tutorialList.Count > 0)
                {
                    tutorialList.Peek().SetActive(false);
                }

                if(trialEndTutorial && Input.GetMouseButtonDown(0))
                {
                    player.GetComponent<Animator>().SetBool("DoneLevel2", false);
                    lvl2PresentationDone = true;
                }
            break;
        }

        if(tutorialDone)
        {
            zanTalkAudioSource.SetActive(false);
        }
    }

    void OnTutorial()
    {
        zanTalkAudioSource.SetActive(true);
        if (tutorialList.Count > 0 && !tutorialList.Peek().activeSelf)
        {
            tutorialList.Peek().SetActive(true);
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (tutorialList.Count > 0)
            {
                tutorialList.Peek().SetActive(false);
                tutorialList.Dequeue();
            }

            if (tutorialList.Count > 0)
            {
                tutorialList.Peek().SetActive(true);
            }
            else
            {
                tutorialDone = true;
                lvl1PresentationDone = true;
            }
        }
    }

    public IEnumerator WaitAnimationTutorial()
    {
        yield return new WaitForSeconds(4f);
        sword.SetActive(true);
        yield return new WaitForSeconds(1f);
        trialEndTutorial = true;
    }
    public IEnumerator WaitAnimationLevel1()
    {
        yield return new WaitForSeconds(8.3f);
        sword.SetActive(true);
        yield return new WaitForSeconds(1f);
        trialEndTutorial = true;
    }
    public IEnumerator WaitAnimationLevel2()
    {
        yield return new WaitForSeconds(7.3f);
        sword.SetActive(true);
        yield return new WaitForSeconds(1f);
        trialEndTutorial = true;
    }

    public IEnumerator WaitForScrollEndTutorial()
    {
        yield return new WaitForSeconds(1.5f);
        black.GetComponent<Animator>().SetBool("Out", false);
        player.GetComponent<Animator>().SetBool("DoneTutorial", true);
        StartCoroutine(WaitAnimationTutorial());
    }
    public IEnumerator WaitForScrollEndLevel1()
    {
        yield return new WaitForSeconds(1.5f);
        black.GetComponent<Animator>().SetBool("Out", false);
        player.GetComponent<Animator>().SetBool("DoneLevel1", true);
        StartCoroutine(WaitAnimationLevel1());
    }
    public IEnumerator WaitForScrollEndLevel2()
    {
        yield return new WaitForSeconds(1.5f);
        black.GetComponent<Animator>().SetBool("Out", false);
        player.GetComponent<Animator>().SetBool("DoneLevel2", true);
        StartCoroutine(WaitAnimationLevel2());
    }
}
