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

    void Start()
    {
        initialHandPos = Hand.localPosition;

        // Bloquear ratón
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
    }

    void UpdateHand()
    {
        Vector3 sway = new Vector3(mouseDelta.x, mouseDelta.y, 0) * swayAmount;
        Vector3 targetPos = initialHandPos + sway;

        Hand.localPosition = Vector3.Lerp(Hand.localPosition, targetPos, Time.deltaTime * swaySmooth);

        Quaternion targetRot = Quaternion.Euler(-mouseDir.y, mouseDir.x, 0);

        Hand.localRotation = Quaternion.Slerp(
            Hand.localRotation,
            targetRot,
            Time.deltaTime * handSmooth
        );
    }

    void UpdateSword()
    {
        swordTargetRot = Hand.rotation;

        // Rotar 90° para que el eje X mire hacia delante
        Quaternion corrected = swordTargetRot * Quaternion.Euler(0, 0, 90);

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            corrected,
            Time.deltaTime / swordDelay
        );
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
