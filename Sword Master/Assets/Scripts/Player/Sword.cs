using UnityEngine;

public class Sword : MonoBehaviour
{
    [Header("Hand")]
    public Transform Hand;
    public float swayAmount = 0.05f;
    public float swaySmooth = 8f;
    public float handSmooth = 12f;
    private Vector3 initialHandPos;

    [Header("Sword")]
    public float sensitivity = 2f;
    public float swordDelay = 0.06f;
    private Quaternion swordTargetRot;

    private Vector2 mouseDir;
    private Vector2 mouseDelta;

    private bool blocking = false;

    void Start()
    {
        initialHandPos = Hand.localPosition;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        CheckBlock();
        CheckCalibration();

        if (!blocking)
            mouseDir += mouseDelta * sensitivity;

        UpdateHand();
        UpdateSword();
    }

    // ---------------- BLOQUEO ----------------

    void CheckBlock()
    {
        if (Input.GetMouseButtonDown(1))
            blocking = true;

        if (Input.GetMouseButtonUp(1))
            blocking = false;
    }

    // ---------------- MANO ----------------

    void UpdateHand()
    {
        if (blocking)
        {
            // Mano congelada durante el bloqueo
            Hand.localPosition = Vector3.Lerp(
                Hand.localPosition,
                initialHandPos,
                Time.deltaTime * 10f
            );

            Hand.localRotation = Quaternion.Slerp(
                Hand.localRotation,
                Quaternion.identity,
                Time.deltaTime * 10f
            );

            return;
        }

        // Sway normal
        Vector3 sway = new Vector3(mouseDelta.x, mouseDelta.y, 0) * swayAmount;
        Vector3 targetPos = initialHandPos + sway;

        Hand.localPosition = Vector3.Lerp(
            Hand.localPosition,
            targetPos,
            Time.deltaTime * swaySmooth
        );

        // Rotación estilo Wii
        Quaternion targetRot = Quaternion.Euler(-mouseDir.y, mouseDir.x, 0);

        Hand.localRotation = Quaternion.Slerp(
            Hand.localRotation,
            targetRot,
            Time.deltaTime * handSmooth
        );
    }

    // ---------------- ESPADA ----------------

    void UpdateSword()
    {
        if (blocking)
        {
            // Rotación de bloqueo RELATIVA a la mano (localRotation)
            Quaternion blockRot = Quaternion.Euler(0, 0, -90);

            transform.localRotation = Quaternion.Slerp(
                transform.localRotation,
                blockRot,
                Time.deltaTime * 12f
            );

            return;
        }

        // Movimiento normal estilo Wii Sports Resort
        swordTargetRot = Hand.rotation;

        // Corrección para que el eje X de la espada mire hacia delante
        Quaternion corrected = swordTargetRot * Quaternion.Euler(0, 0, 90);

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            corrected,
            Time.deltaTime / swordDelay
        );
    }

    // ---------------- CALIBRACIÓN ----------------

    void CheckCalibration()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            mouseDir = Vector2.zero;
            Hand.localRotation = Quaternion.identity;
            transform.localRotation = Quaternion.identity;
        }
    }
}
