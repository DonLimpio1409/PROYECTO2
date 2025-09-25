using System.Net.Sockets;
using System.Text;
using UnityEngine;
using System;
using System.Net;

public class Recividor : MonoBehaviour
{
    public UdpClient udp;
    public IPEndPoint remoteEndPoint;

    public Quaternion ultimaRotacion { get; private set; }
    public Transform posicion { get; private set; }

    void Start()
    {
        udp = new UdpClient(5055);
        remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);
    }

    void Update()
    {
        //if (udp.Available > 0)
        //{
            byte[] data = udp.Receive(ref remoteEndPoint);
            string mensaje = Encoding.UTF8.GetString(data);

            Debug.Log("" + mensaje);

            MotionData m = JsonUtility.FromJson<MotionData>(mensaje);
            ultimaRotacion = new Quaternion(m.x, m.y, m.z, m.w);
        //}
    }

    [System.Serializable]

    public class MotionData
    {
        public float x, y, z, w;
        public float ax, ay, az;
    }
}
