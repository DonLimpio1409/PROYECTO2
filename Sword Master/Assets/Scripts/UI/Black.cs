using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class Black : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(Disapear());
    }

    IEnumerator Disapear()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

}
