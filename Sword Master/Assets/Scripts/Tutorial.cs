using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class Tutorial : MonoBehaviour
{
    bool enterTutorial = false;
    bool joke = true;
    public bool tutorialDone = false;

    public GameObject instruc1;
    public GameObject instruc2;
    public GameObject instruc3;
    public GameObject instruc4;
    public GameObject instruc5;

    Queue<GameObject> lifeList = new Queue<GameObject>();

    void Start()
    {
        lifeList.Enqueue(instruc1);
        lifeList.Enqueue(instruc2); 
        lifeList.Enqueue(instruc3);
        lifeList.Enqueue(instruc4);
        lifeList.Enqueue(instruc5);
    }

    // Update is called once per frame
    void Update()
    {
        OnTutorial();
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
        if (lifeList.Count > 0 && !lifeList.Peek().activeSelf)
        {
            lifeList.Peek().SetActive(true);
        }

        // Avanzar al siguiente paso
        if (Input.GetMouseButtonDown(0))
        {
            if (lifeList.Count > 0)
            {
                // Apagar el actual
                lifeList.Peek().SetActive(false);
                lifeList.Dequeue();
            }

            // Encender el siguiente si existe
            if (lifeList.Count > 0)
            {
                lifeList.Peek().SetActive(true);
            }
            else
            {
                tutorialDone = true;
            }
        }
    }

}
