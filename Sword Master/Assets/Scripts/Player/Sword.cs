using Unity.VisualScripting;
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

    [Header("Blocking")]
    public bool blocking = false;
    public float blocktime = 0f;
    public float cooldonwBlock = 2f;
    public GameObject Shield;

    // Cooldown para evitar swing tras bloquear
    private float swingLockTimer = 0f;

    // ---------------- SWING RÁPIDO ----------------
    private Quaternion lastRotation;
    private float angularSpeed;

    [Header("Fast Swing Settings")]
    public float fastSwingThreshold = 500f;
    public float extendAmount = 0.3f;
    public float extendSmooth = 10f;
    public GameObject trail;

    private Vector3 normalLocalPos;

    [Header("Rotation Limits")]
    public float maxUp = 1f;
    public float maxDown = 60f;
    public float maxLeft = 100f;
    public float maxRight = 100f;

    [Header("Otros")]
    public Animator blueScreen;


    void Start()
    {
        initialHandPos = Hand.localPosition;
        normalLocalPos = transform.localPosition;

        lastRotation = transform.rotation;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        // Reducir cooldown de swing
        if (swingLockTimer > 0f)
            swingLockTimer -= Time.deltaTime;

        CheckBlock();
        CheckCalibration();

        if (!blocking)
        {
            mouseDir += mouseDelta * sensitivity;

            mouseDir.x = Mathf.Clamp(mouseDir.x, -maxLeft, maxRight);
            mouseDir.y = Mathf.Clamp(mouseDir.y, -maxUp, maxDown);
        }

        UpdateHand();
        UpdateSword();
    }

    // ---------------- BLOQUEO ----------------

    void CheckBlock()
    {
        if (Input.GetMouseButton(1) && cooldonwBlock <= 0f)
        {
            blocking = true;
            blueScreen.SetBool("Bloking", true);
            cooldonwBlock = 1f;
            Shield.SetActive(true);
        }
        
        if (blocking)
        {
            blocktime += Time.deltaTime;

            if (blocktime >= 0.5f)
            {
                blueScreen.SetBool("Bloking", false);
                blocking = false;
                blocktime = 0f;
                cooldonwBlock = 1f;
                Shield.SetActive(false);

                // ACTIVAR COOLDOWN DE SWING
                swingLockTimer = 0.2f;
            }
        }
        else
        {
            if (cooldonwBlock > 0f)
                cooldonwBlock -= Time.deltaTime;
        }
    }

    // ---------------- MANO ----------------

    void UpdateHand()
    {
        if (blocking)
        {
            Hand.localPosition = Vector3.Lerp(Hand.localPosition, initialHandPos, Time.deltaTime * 10f);
            Hand.localRotation = Quaternion.Slerp(Hand.localRotation, Quaternion.identity, Time.deltaTime * 10f);
            return;
        }

        // Sway
        Vector3 sway = new Vector3(mouseDelta.x, mouseDelta.y, 0) * swayAmount;
        Vector3 targetPos = initialHandPos + sway;

        Hand.localPosition = Vector3.Lerp(Hand.localPosition, targetPos, Time.deltaTime * swaySmooth);

        // Rotación corregida
        float yaw = Hand.parent.rotation.eulerAngles.y;

        Quaternion playerYaw = Quaternion.Euler(0, yaw, 0);
        Quaternion handPitchYaw = Quaternion.Euler(-mouseDir.y, mouseDir.x, 0);

        Hand.rotation = Quaternion.Slerp(Hand.rotation, playerYaw * handPitchYaw, Time.deltaTime * handSmooth);
    }

    // ---------------- ESPADA ----------------

    void UpdateSword()
    {
        if (blocking)
        {
            Quaternion blockRot = Quaternion.Euler(0, 0, -90);

            transform.localRotation = Quaternion.Slerp(transform.localRotation, blockRot, Time.deltaTime * 12f);
            transform.localPosition = Vector3.Lerp(transform.localPosition, normalLocalPos, Time.deltaTime * extendSmooth);

            return;
        }

        // Movimiento normal estilo Wii Sports Resort
        swordTargetRot = Hand.rotation;

        Quaternion corrected = swordTargetRot * Quaternion.Euler(0, 0, 90);

        transform.rotation = Quaternion.Slerp(transform.rotation, corrected, Time.deltaTime / swordDelay);

        // Cálculo de velocidad angular
        Quaternion delta = transform.rotation * Quaternion.Inverse(lastRotation);

        delta.ToAngleAxis(out float angle, out Vector3 axis);

        if (angle > 180f) angle = 360f - angle;

        angularSpeed = angle / Time.deltaTime;

        lastRotation = transform.rotation;

        // EXTENSIÓN POR SWING RÁPIDO
        Vector3 forwardLocal = transform.parent.InverseTransformDirection(Hand.parent.forward);

        if (swingLockTimer <= 0f && angularSpeed > fastSwingThreshold)
        {
            transform.localPosition = Vector3.Lerp(
                transform.localPosition,
                normalLocalPos + forwardLocal.normalized * extendAmount,
                Time.deltaTime * extendSmooth
            );

            trail.SetActive(true);
            SoundController.Instance.PlaySFX(SoundController.Instance.swingSound);
        }
        else
        {
            transform.localPosition = Vector3.Lerp(
                transform.localPosition,
                normalLocalPos,
                Time.deltaTime * extendSmooth
            );

            trail.SetActive(false);
        }
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
