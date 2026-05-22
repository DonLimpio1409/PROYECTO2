using System.Collections.Generic;
using UnityEngine;

public class JoyconReader : MonoBehaviour
{
    private List<Joycon> joycons;
    private Joycon joycon;

    void Start()
    {
        // Obtener la lista de Joy-Cons detectados por JoyconManager
        joycons = JoyconManager.Instance.j;

        if (joycons.Count > 0)
        {
            joycon = joycons[0]; // Usamos el primero
            Debug.Log("Joy-Con detectado correctamente.");
        }
        else
        {
            Debug.LogWarning("No se detectó ningún Joy-Con.");
        }
    }

    void Update()
    {
        if (joycon == null) return;

        // Datos del giroscopio
        Vector3 gyro = joycon.GetGyro();

        // Datos del acelerómetro
        Vector3 accel = joycon.GetAccel();

        // Botones
        bool botonShoulder1 = joycon.GetButton(Joycon.Button.SHOULDER_1);
        bool botonShoulder2 = joycon.GetButton(Joycon.Button.SHOULDER_2);

        Debug.Log($"Gyro: {gyro} | Accel: {accel} | Shoulder 1: {botonShoulder1} | Shoulder 2: {botonShoulder2}");
    }
}
