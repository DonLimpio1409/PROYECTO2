using UnityEngine;

public class ControlEspada : MonoBehaviour
{
    public Recividor recividor;

    public float sensibilidad = 100f;
    public float velocidadDefensa = 5f;

    Quaternion rotacionInicial;
    Quaternion rotacionDefensa;

    void Start()
    {
        rotacionInicial = transform.rotation;
        rotacionDefensa = Quaternion.Euler(0, 0, 90); // horizontal
    }

    void Update()
    {
        if (recividor == null) return;

        if (recividor.defendiendo)
        {
            transform.rotation = Quaternion.Lerp(
                transform.rotation,
                rotacionDefensa,
                Time.deltaTime * velocidadDefensa
            );
        }
        else
        {
            Vector3 rot = recividor.rotacionRecibida * sensibilidad * Time.deltaTime;

            transform.Rotate(rot.x, rot.y, rot.z);
        }
    }
}
