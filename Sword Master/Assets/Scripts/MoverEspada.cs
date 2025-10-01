using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverEspada : MonoBehaviour
{
    private Recividor recividor;
    private Vector3 velocidad; // para integrar la aceleración
    private Vector3 posicion;  // posición calculada

    public float sensibilidad = 1f; // ajusta para escalar el movimiento
    public float suavizado = 0.9f;  // factor de amortiguación para evitar que se dispare

    void Start()
    {
        recividor = FindObjectOfType<Recividor>();
        posicion = transform.position;
    }

    void Update()
{
    if (recividor != null)
    {
        transform.SetPositionAndRotation(recividor.ultimaPosicion, recividor.ultimaRotacion);
    }
}
}

