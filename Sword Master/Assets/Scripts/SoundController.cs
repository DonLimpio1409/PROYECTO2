using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class SoundController : MonoBehaviour
{
    public static SoundController Instance;

    [Header("AudioSources")]
    public AudioSource SFXAudioSource;
    public AudioSource musicAudioSource;
    public AudioSource footstepAudioSource;
    public AudioSource mexicanAudioSource;

    // Cooldown general para evitar spam
    public float globalCooldown = 0.05f;

    // Tiempo del último sonido reproducido
    private Dictionary<AudioClip, float> lastPlayTime = new Dictionary<AudioClip, float>();

    [Header("Music")]
    public AudioClip TutorialMusic;
    public AudioClip Level1Music;
    public AudioClip Level2Music;

    [Header("SFX")]
    public AudioClip swingSound;
    public AudioClip dizzySound;
    public AudioClip buttonIn;
    public AudioClip buttonOut;
    public AudioClip parryAtZan;
    public AudioClip landSteps;
    public AudioClip roadSteps;
    public AudioClip flatSteps;
    public AudioClip parryAtEnemy;
    public AudioClip getHitted;
    public AudioClip hitEnemy;

    [Header("Utiles")]
    private float originalMusicVolume;
    public float fadeSpeed = 2f; 
    public Transform cameraTransform;


    void Awake()
    {
        Instance = this;
        originalMusicVolume = musicAudioSource.volume;
    }

    void Update()
    {
        float distance = Vector3.Distance(cameraTransform.position, mexicanAudioSource.transform.position);

        bool playerHearsMexicanMusic = distance <= mexicanAudioSource.maxDistance;

        if (playerHearsMexicanMusic)
        {
            // Fade out de la música global
            musicAudioSource.volume = Mathf.Lerp(
                musicAudioSource.volume,
                0f,
                Time.deltaTime * fadeSpeed
            );
        }
        else
        {
            // Fade in cuando el jugador sale del área
            musicAudioSource.volume = Mathf.Lerp(
                musicAudioSource.volume,
                originalMusicVolume,
                Time.deltaTime * fadeSpeed
            );
        }
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

        SFXAudioSource.PlayOneShot(clip);
        lastPlayTime[clip] = tiempoActual;
    }

    public void PlayMusic(AudioClip clip)
    {
        if (clip == null) return;

        musicAudioSource.clip = clip;
        musicAudioSource.loop = true;
        musicAudioSource.Play();
    }

    public void Playfootstep(AudioClip clip)
    {
        if (clip == null) return;
        footstepAudioSource.clip = clip;
        footstepAudioSource.Play();
    }

    public void DecideFootstepSound()
    {
        if(SceneManager.GetActiveScene().name == "1 Tutorial")
        {
            Playfootstep(landSteps);
        }
        else if(SceneManager.GetActiveScene().name == "2 Level 1")
        {
            Playfootstep(flatSteps);
        }
        else if(SceneManager.GetActiveScene().name == "3 Level 2")
        {
            Playfootstep(roadSteps);
        }
    }
}
