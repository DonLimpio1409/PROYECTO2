using UnityEditor.AdaptivePerformance.Editor;
using UnityEngine;

public class Sword : MonoBehaviour
{
    Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        Movement();
    }

    public void Movement()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 3f; // distancia desde la cámara

        transform.position = Camera.main.ScreenToWorldPoint(mousePosition);
    }
    public void Block()
    {
        if(Input.GetMouseButton(0))
        {

        }
        else
        {
 
        }
    }
}
