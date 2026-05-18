using System.Collections;
using UnityEngine;
public class TLevel1 : MonoBehaviour
{

    public bool trialEndTutorial;
    public GameObject black;
    public GameObject player;
    void Awake()
    {
        black.GetComponent<Animator>().SetBool("Out", true);
    }

    void Start()
    {
        StartCoroutine(WaitForScrollEnd());
    }

    void Update()
    {
        if(trialEndTutorial && Input.GetMouseButtonDown(0))
        {
            player.GetComponent<Animator>().SetBool("DoneLevel1", false);
        }
    }

    public IEnumerator WaitAnimation()
    {
        yield return new WaitForSeconds(9.3f);
        trialEndTutorial = true;
    }

    public IEnumerator WaitForScrollEnd()
    {
        yield return new WaitForSeconds(1.5f);
        black.GetComponent<Animator>().SetBool("Out", false);
        player.GetComponent<Animator>().SetBool("DoneLevel1", true);
        StartCoroutine(WaitAnimation());
    }
}
