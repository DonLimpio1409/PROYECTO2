using System.Collections.Generic;
using UnityEngine;

public class JoyconDemo : MonoBehaviour
{
    private List<Joycon> joycons;
    private Joycon j;

    private bool joyconConnected = true;

    [Header("Hand")]
    public Transform Hand;
    public float handSmooth = 12f;
    private Vector3 handVelocity;
    private Vector3 handPosition;

    [Header("Movement Multiplier")]
    public float movementMultiplier = 1f;

    [Header("Sword")]
    public float swordDelay = 0.05f;
    private Quaternion lastRotation;
    private Vector3 normalLocalPos;

    [Header("Blocking")]
    public bool blocking = false;
    public float blocktime = 0f;
    public float cooldownBlock = 1.5f;

    private float swingLockTimer = 0f;

    [Header("Fast Swing Settings")]
    public float fastSwingThreshold = 500f;
    public float extendAmount = 0.25f;
    public float extendSmooth = 12f;
    public GameObject trail;

    private float angularSpeed;

    [Header("Otros")]
    public Animator blueScreen;

    // Joycon orientation
    private Quaternion joyconRot;
    private Quaternion calibrationOffset = Quaternion.identity;

    // POSICIÓN Y ROTACIÓN FIJA DE BLOQUEO
    private readonly Vector3 blockPosition = new Vector3(-0.00999999978f, 0.0700000003f, 2.03999996f);
    private readonly Vector3 blockRotationEuler = new Vector3(321.086365f, 269.524414f, 180.755463f);

    void Start()
    {
        joycons = JoyconManager.Instance.j;

        if (joycons.Count < 1)
        {
            Debug.LogError("NO HAY JOYCONS CONECTADOS");
            enabled = false;
            return;
        }

        j = joycons[0];

        handPosition = Hand.localPosition;
        normalLocalPos = transform.localPosition;

        lastRotation = transform.rotation;
    }

    void Update()
    {
        CheckJoyconConnection();

        if (!joyconConnected)
            return;

        if (j == null) return;

        joyconRot = j.GetVector();

        if (swingLockTimer > 0f)
            swingLockTimer -= Time.deltaTime;

        CheckCalibration();
        CheckBlock();

        if (!blocking)
            UpdateHand();

        UpdateSword();
    }

    // ---------------- DETECCIÓN DE CONEXIÓN ----------------
    void CheckJoyconConnection()
    {
        bool isConnected = joycons != null && joycons.Count > 0 && joycons[0] != null;

        if (isConnected && !joyconConnected)
        {
            joyconConnected = true;
            Time.timeScale = 1f;
            Debug.Log("JOYCON RECONECTADO → Reanudando juego");

            j = joycons[0];
        }
        else if (!isConnected && joyconConnected)
        {
            joyconConnected = false;
            Time.timeScale = 0f;
            Debug.Log("JOYCON DESCONECTADO → Pausando juego");

            j = null;
        }
    }

    // ---------------- CALIBRACIÓN ----------------
    void CheckCalibration()
    {
        if (j.GetButtonDown(Joycon.Button.DPAD_UP) || j.GetButtonDown(Joycon.Button.SHOULDER_1))
        {
            calibrationOffset = Quaternion.Inverse(joyconRot);
            Hand.localRotation = Quaternion.identity;
            transform.localRotation = Quaternion.identity;

            handVelocity = Vector3.zero;
            handPosition = Vector3.zero;
            Hand.localPosition = Vector3.zero;
        }
    }

    // ---------------- BLOQUEO ----------------
    void CheckBlock()
    {
        bool blockPressed = j.GetButtonDown(Joycon.Button.SHOULDER_2) || j.GetButtonDown(Joycon.Button.SHOULDER_1);

        if (blockPressed && cooldownBlock <= 0f)
        {
            blocking = true;
            blueScreen.SetBool("Bloking", true);
            cooldownBlock = 1.5f;
        }

        if (blocking)
        {
            blocktime += Time.deltaTime;

            if (blocktime >= 0.5f)
            {
                blocking = false;
                blocktime = 0f;
                blueScreen.SetBool("Bloking", false);

                swingLockTimer = 0.2f;
            }
        }
        else
        {
            if (cooldownBlock > 0f)
                cooldownBlock -= Time.deltaTime;
        }
    }

    // ---------------- MANO ----------------
    void UpdateHand()
    {
        Quaternion correctedRot = calibrationOffset * joyconRot;

        Hand.rotation = Quaternion.Slerp(
            Hand.rotation,
            correctedRot,
            Time.deltaTime * handSmooth
        );

        Vector3 accel = j.GetAccel();
        Vector3 worldAccel = new Vector3(accel.x, accel.y, accel.z);

        handVelocity += worldAccel * movementMultiplier * Time.deltaTime;
        handVelocity *= 0.92f;

        handPosition += handVelocity * Time.deltaTime;

        Hand.localPosition = handPosition;
    }

    // ---------------- ESPADA ----------------
    void UpdateSword()
    {
        if (blocking)
        {
            transform.localPosition = blockPosition;

            Quaternion blockRot = Quaternion.Euler(blockRotationEuler);

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                blockRot,
                Time.deltaTime * 12f
            );

            return;
        }

        Quaternion targetRot = Hand.rotation * Quaternion.Euler(0, 0, 90);

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRot,
            Time.deltaTime / swordDelay
        );

        Quaternion delta = transform.rotation * Quaternion.Inverse(lastRotation);
        delta.ToAngleAxis(out float angle, out _);

        if (angle > 180f) angle = 360f - angle;

        angularSpeed = angle / Time.deltaTime;
        lastRotation = transform.rotation;

        if (swingLockTimer <= 0f && angularSpeed > fastSwingThreshold)
        {
            Vector3 swingDir = Hand.forward;

            transform.localPosition = Vector3.Lerp(
                transform.localPosition,
                normalLocalPos + swingDir.normalized * extendAmount,
                Time.deltaTime * extendSmooth
            );

            trail.SetActive(true);
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
}
