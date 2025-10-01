using System.Net.Sockets;
using System.Text;
using UnityEngine;
using System;
using System.Net;

public class Recividor : MonoBehaviour
{
    public UdpClient udp;
    public IPEndPoint remoteEndPoint;

    public Vector3 ultimaPosicion { get; private set; }
    public Quaternion ultimaRotacion { get; private set; }

    void Start()
    {
        udp = new UdpClient(443);
        remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);
    }

    void Update()
    {
        if (udp.Available > 0)
        {
            byte[] data = udp.Receive(ref remoteEndPoint);
            string mensaje = Encoding.UTF8.GetString(data);

            MotionData m = JsonUtility.FromJson<MotionData>(mensaje);

            ultimaPosicion = new Vector3(m.px, m.py, m.pz);
            ultimaRotacion = new Quaternion(m.qx, m.qy, m.qz, m.qw);
        }
    }

    [System.Serializable]
    public class MotionData
    {
        public float px, py, pz;
        public float qx, qy, qz, qw;
    }
}