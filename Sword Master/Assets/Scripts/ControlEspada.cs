using UnityEngine;

public class ControlEspada : MonoBehaviour
{
    public Recividor recividor;

    public float sensibilidadMovimiento = 2f;
    public float suavizado = 5f;
    public float velocidadDefensa = 8f;

    Vector3 velocidad;
    Quaternion rotacionDefensa;

    void Start()
    {
        rotacionDefensa = Quaternion.Euler(0, 0, 90);
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
            // ROTACIÓN
            Quaternion rotMovil = new Quaternion(
                -recividor.rotacionRecibida.x,
                -recividor.rotacionRecibida.y,
                 recividor.rotacionRecibida.z,
                 recividor.rotacionRecibida.w
            );

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                rotMovil,
                Time.deltaTime * suavizado
            );

            // TRASLACIÓN
            Vector3 aceleracion = recividor.aceleracionRecibida;
            velocidad += aceleracion * sensibilidadMovimiento * Time.deltaTime;

            transform.position += velocidad * Time.deltaTime;

            // Pequeño freno para que no se vaya al infinito
            velocidad *= 0.98f;
        }
    }
}
