using UnityEngine;
using UnityEngine.UI;
using System.Net.Sockets;
using System.Text;

public class Informador : MonoBehaviour
{
    public string ipPC = "192.168.1.135";
    public int puerto = 455;

    public Button botonDefensa;

    UdpClient udp;
    bool defendiendo = false;

    void Start()
    {
        udp = new UdpClient();

        if (SystemInfo.supportsGyroscope)
            Input.gyro.enabled = true;

        botonDefensa.onClick.AddListener(() =>
        {
            defendiendo = true;
            Invoke(nameof(DesactivarDefensa), 0.3f);
        });
    }

    void DesactivarDefensa()
    {
        defendiendo = false;
    }

    void Update()
    {
        Quaternion rot = Input.gyro.attitude;
        Vector3 acc = Input.acceleration;

        string mensaje =
            rot.x + "," + rot.y + "," + rot.z + "," + rot.w + "," +
            acc.x + "," + acc.y + "," + acc.z + "," +
            defendiendo;

        byte[] datos = Encoding.UTF8.GetBytes(mensaje);
        udp.Send(datos, datos.Length, ipPC, puerto);
    }
}
