using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Globalization;

public class Receiver : MonoBehaviour
{
    public int port = 161;

    UdpClient udp;
    Thread hilo;

    public Quaternion rotationReceived;
    public Vector3 acelerationReceived;
    public bool defending;

    void Start()
    {
        udp = new UdpClient(port);

        hilo = new Thread(RecivedData);
        hilo.IsBackground = true;
        hilo.Start();
    }

    void RecivedData()
    {
        IPEndPoint anyIP = new IPEndPoint(IPAddress.Any, port);

        while (true)
        {
            byte[] data = udp.Receive(ref anyIP);
            string message = Encoding.UTF8.GetString(data);
            string[] v = message.Split(',');

            rotationReceived = new Quaternion(
                float.Parse(v[0], CultureInfo.InvariantCulture),
                float.Parse(v[1], CultureInfo.InvariantCulture),
                float.Parse(v[2], CultureInfo.InvariantCulture),
                float.Parse(v[3], CultureInfo.InvariantCulture)
            );
            Debug.Log(rotationReceived);

            acelerationReceived = new Vector3(
                float.Parse(v[4], CultureInfo.InvariantCulture),
                float.Parse(v[5], CultureInfo.InvariantCulture),
                float.Parse(v[6], CultureInfo.InvariantCulture)
            );
            Debug.Log(acelerationReceived);

            defending = bool.Parse(v[7]);
        }
    }
}
