using UnityEditor.AdaptivePerformance.Editor;
using UnityEngine;

public class Sword : MonoBehaviour
{
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
}
