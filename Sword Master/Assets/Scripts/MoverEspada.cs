using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverEspada : MonoBehaviour
{
    private Recividor recividor;

    // Start is called before the first frame update 
    void Start()
    {
        recividor = FindObjectOfType<Recividor>();
    }

    // Update is called once per frame 
    void Update()
    {
        if (recividor != null)
        {
            transform.rotation = new Quaternion(-recividor.ultimaRotacion.x, -recividor.ultimaRotacion.z, -recividor.ultimaRotacion.y, recividor.ultimaRotacion.w);
        }
    }
}
