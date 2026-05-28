using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Context : MonoBehaviour
{
    Collider colliderContext;

    public Collider ObjectCollider { get { return colliderContext; } }
    Rigidbody rb;

    public Rigidbody ObjectRb { get { return rb; } }


    void Start()
    {
        colliderContext = transform.Find("Colisionador")?.GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();
        rb.AddForce(Random.onUnitSphere * Random.Range(1f, 5f), ForceMode.Impulse);
    }


    public void Move(Vector3 force)
    {
        if (force != Vector3.zero)
        {
            rb.AddForce(force, ForceMode.VelocityChange);
        }
        if (rb.linearVelocity.sqrMagnitude > 0.1f)
        {
            rb.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(rb.linearVelocity), Time.deltaTime * 5f);
        }
    }


}