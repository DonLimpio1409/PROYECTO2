using UnityEngine;
using System.Collections.Generic;

public class SoundController : MonoBehaviour
{
    public static SoundController Instance;
    public AudioSource audioSource;

    // Cooldown general para evitar spam
    public float globalCooldown = 0.05f;

    // Tiempo del último sonido reproducido
    private Dictionary<AudioClip, float> lastPlayTime = new Dictionary<AudioClip, float>();

    [Header("SFX")]
    public AudioClip swingSound;

    void Awake()
    {
        Instance = this;
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip == null) return;

        float tiempoActual = Time.time;

        // Si nunca se ha reproducido este sonido, lo añadimos
        if (!lastPlayTime.ContainsKey(clip))
            lastPlayTime[clip] = -999f;

        // Evita que el sonido se reproduzca demasiadas veces seguidas
        if (tiempoActual - lastPlayTime[clip] < globalCooldown)
            return;

        audioSource.PlayOneShot(clip);
        lastPlayTime[clip] = tiempoActual;
    }
}
