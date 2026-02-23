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

    public Vector3 rotacionRecibida;
    public bool defendiendo;

    void Start()
    {
        udp = new UdpClient(puerto);

        hilo = new Thread(new ThreadStart(RecibirDatos));
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

            string[] valores = mensaje.Split(',');

            rotacionRecibida = new Vector3(
                float.Parse(valores[0]),
                float.Parse(valores[1]),
                float.Parse(valores[2])
            );

            defendiendo = bool.Parse(valores[3]);
        }
    }

    void OnApplicationQuit()
    {
        if (hilo != null)
            hilo.Abort();

        if (udp != null)
            udp.Close();
    }
}
