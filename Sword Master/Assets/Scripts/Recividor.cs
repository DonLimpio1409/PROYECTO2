using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class Recividor : MonoBehaviour
{
    public int puerto = 5005;

    UdpClient udp;
    Thread hilo;

    public Quaternion rotacionRecibida;
    public Vector3 aceleracionRecibida;
    public bool defendiendo;

    void Start()
    {
        udp = new UdpClient(puerto);

        hilo = new Thread(RecibirDatos);
        hilo.IsBackground = true;
        hilo.Start();
    }

    void RecibirDatos()
    {
        IPEndPoint anyIP = new IPEndPoint(IPAddress.Any, puerto);

        while (true)
        {
            byte[] datos = udp.Receive(ref anyIP);
            string mensaje = Encoding.UTF8.GetString(datos);
            string[] v = mensaje.Split(',');

            rotacionRecibida = new Quaternion(
                float.Parse(v[0]),
                float.Parse(v[1]),
                float.Parse(v[2]),
                float.Parse(v[3])
            );

            aceleracionRecibida = new Vector3(
                float.Parse(v[4]),
                float.Parse(v[5]),
                float.Parse(v[6])
            );

            defendiendo = bool.Parse(v[7]);
        }
    }
}