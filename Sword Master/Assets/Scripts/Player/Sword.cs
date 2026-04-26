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
        mouseDir += mouseDelta * sensitivity;

        UpdateHand();
        UpdateSword();
        CheckCalibration();
        CheckBlock();
    }

    void CheckBlock()
    {
        if (Input.GetMouseButtonDown(1))
            blocking = true;

        if (Input.GetMouseButtonUp(1))
            blocking = false;
    }

    void UpdateHand()
    {
        Vector3 sway = new Vector3(mouseDelta.x, mouseDelta.y, 0) * swayAmount;
        Vector3 targetPos = initialHandPos + sway;

        Hand.localPosition = Vector3.Lerp(Hand.localPosition, targetPos, Time.deltaTime * swaySmooth);

        Quaternion targetRot = Quaternion.Euler(-mouseDir.y, mouseDir.x, 0);

        Hand.localRotation = Quaternion.Slerp(Hand.localRotation, targetRot, Time.deltaTime * handSmooth);
    }

    void UpdateSword()
    {
        if (blocking)
        {
            // Espada horizontal (bloqueo)
            Quaternion blockRot = Quaternion.Euler(0, 0, -90);

            transform.rotation = Quaternion.Slerp(transform.rotation, blockRot, Time.deltaTime * 12f);

            return;
        }

        // Movimiento normal estilo Wii
        swordTargetRot = Hand.rotation;

        Quaternion corrected = swordTargetRot * Quaternion.Euler(0, 0, 90);

        transform.rotation = Quaternion.Slerp(transform.rotation, corrected, Time.deltaTime / swordDelay);
    }

    void CheckCalibration()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            mouseDir = Vector2.zero;
            Hand.localRotation = Quaternion.identity;
            transform.rotation = Quaternion.identity;
        }
    }
}
