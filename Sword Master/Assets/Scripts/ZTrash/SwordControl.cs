using UnityEngine;

public class SwordControl : MonoBehaviour
{
    public Receiver receiver;

    public float moveSensitivity = 2f;
    public float smoothed = 5f;
    public float defenseSpeed = 8f;

    Vector3 speed;
    Quaternion defenseRotation;

    void Start()
    {
        defenseRotation = Quaternion.Euler(0, 0, 90);
    }

    void Update()
    {
        if (receiver == null) return;

        if (receiver.defending)
        {
            transform.rotation = Quaternion.Lerp(
                transform.rotation,
                defenseRotation,
                Time.deltaTime * defenseSpeed
            );
        }
        else
        {
            // ROTACIÓN
            Quaternion rotMovil = receiver.rotationReceived;

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                rotMovil,
                Time.deltaTime * smoothed
            );

            // TRASLACIÓN
            Vector3 aceleracion = receiver.acelerationReceived;
            speed += aceleracion * moveSensitivity * Time.deltaTime;

            transform.position += speed * Time.deltaTime;

            // Pequeño freno para que no se vaya al infinito
            speed *= 0.98f;
        }
    }
}
