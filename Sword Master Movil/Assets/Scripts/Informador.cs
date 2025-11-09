using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using NewInput = UnityEngine.InputSystem; // alias para el nuevo input (solo acelerómetro)

public class Informador : MonoBehaviour
{
    UdpClient udp;
    IPEndPoint remoteEndPoint;

    [System.Serializable]
    public class MotionData
    {
        public float x, y, z, w;   // Rotación
        public float ax, ay, az;   // Aceleración
    }

    void Start()
    {//gsoufegbd
        string ipPC = "192.168.1.135";
        int puerto = 9000;

        udp = new UdpClient();
        remoteEndPoint = new IPEndPoint(IPAddress.Parse(ipPC), puerto);

        // Activar giroscopio del sistema viejo
        if (SystemInfo.supportsGyroscope)
            Input.gyro.enabled = true;
    }

    void Update()
    {
        MotionData m = new MotionData();

        Quaternion q = Input.gyro.attitude;
        m.x = q.x;
        m.y = q.y;
        m.z = q.z;
        m.w = q.w;

        if (NewInput.Accelerometer.current != null)
        {
            Vector3 a = NewInput.Accelerometer.current.acceleration.ReadValue();
            m.ax = a.x;
            m.ay = a.y;
            m.az = a.z;
        }

        // Enviar por UDP
        string mensaje = JsonUtility.ToJson(m);
        byte[] data = Encoding.UTF8.GetBytes(mensaje);
        udp.Send(data, data.Length, remoteEndPoint);

        Debug.Log("Enviando: " + mensaje);
    }
}
