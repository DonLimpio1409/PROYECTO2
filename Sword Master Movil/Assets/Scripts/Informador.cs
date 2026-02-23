using UnityEngine;
using UnityEngine.UI;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class Informador : MonoBehaviour
{
    public string ipPC = "192.168.1.1";
    public int puerto = 5005;

    public Button botonDefensa;

    private UdpClient udp;
    private bool defendiendo = false;

    void Start()
    {
        udp = new UdpClient();

        if (SystemInfo.supportsGyroscope)
            Input.gyro.enabled = true;

        botonDefensa.onClick.AddListener(ActivarDefensa);
    }

    void ActivarDefensa()
    {
        defendiendo = true;
        Invoke("DesactivarDefensa", 0.5f); // defensa medio segundo
    }

    void DesactivarDefensa()
    {
        defendiendo = false;
    }

    void Update()
    {
        Vector3 rotacion = Input.gyro.rotationRateUnbiased;

        string mensaje = rotacion.x + "," + rotacion.y + "," + rotacion.z + "," + defendiendo;

        byte[] datos = Encoding.UTF8.GetBytes(mensaje);
        udp.Send(datos, datos.Length, ipPC, puerto);
    }
}
