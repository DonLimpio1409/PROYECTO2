using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class SettingsManager : MonoBehaviour
{
    [SerializeField]private Toggle fullscreenToggle;
    [SerializeField]private TMP_Dropdown resolutionDropdown;
    [SerializeField]private Slider masterSlider;
    [SerializeField]private Slider sfxSlider;
    [SerializeField]private Slider musicSlider;
    
    private AudioMixer audioMixer;
    private Resolution[] resolutions;
    private int defaultFullscreen = 1;
    private float defaultMasterVolume = 1f;
    private float defaultSFXVolume = 1f;
    private float defaultMusicVolume = 1f;

    private void Start()
    {
        if (audioMixer == null)
        {
            audioMixer = Resources.Load<AudioMixer>("MainMixer");
        }

        resolutions = new Resolution[]
        {
            new Resolution { width = 1280, height = 720 },
            new Resolution { width = 1920, height = 1080 },
            new Resolution { width = 2560, height = 1440 }
        };
        
        PopulateResolutionDropdown();
        LoadSettings();
    }

    private void PopulateResolutionDropdown()
    {
        if (resolutionDropdown == null) return;

        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        if (resolutions == null || resolutionIndex < 0 || resolutionIndex >= resolutions.Length) return;

        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        PlayerPrefs.SetInt("ResolutionIndex", resolutionIndex);
        PlayerPrefs.Save();
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        PlayerPrefs.SetInt("Fullscreen", isFullscreen ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat("Master", Mathf.Log10(volume > 0.0001f ? volume : 0.0001f) * 20f);
        PlayerPrefs.SetFloat("MasterVolume", volume);
        PlayerPrefs.Save();
    }

    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFX", Mathf.Log10(volume > 0.0001f ? volume : 0.0001f) * 20f);
        PlayerPrefs.SetFloat("SFXVolume", volume);
        PlayerPrefs.Save();
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("Music", Mathf.Log10(volume > 0.0001f ? volume : 0.0001f) * 20f);
        PlayerPrefs.SetFloat("MusicVolume", volume);
        PlayerPrefs.Save();
    }

    private void LoadSettings()
    {
        bool savedFullscreen = PlayerPrefs.GetInt("Fullscreen", defaultFullscreen) == 1;
        float savedMaster = PlayerPrefs.GetFloat("MasterVolume", defaultMasterVolume);
        float savedSFX = PlayerPrefs.GetFloat("SFXVolume", defaultSFXVolume);
        float savedMusic = PlayerPrefs.GetFloat("MusicVolume", defaultMusicVolume);

        int defaultResolutionIndex = 1; 
        for (int i = 0; i < resolutions.Length; i++)
        {
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                defaultResolutionIndex = i;
                break;
            }
        }
        int savedResolution = PlayerPrefs.GetInt("ResolutionIndex", defaultResolutionIndex);

        SetFullscreen(savedFullscreen);
        SetResolution(savedResolution);
        SetMasterVolume(savedMaster);
        SetSFXVolume(savedSFX);
        SetMusicVolume(savedMusic);

        if (fullscreenToggle != null) fullscreenToggle.isOn = savedFullscreen;
        if (resolutionDropdown != null) resolutionDropdown.value = savedResolution;
        if (masterSlider != null) masterSlider.value = savedMaster;
        if (sfxSlider != null) sfxSlider.value = savedSFX;
        if (musicSlider != null) musicSlider.value = savedMusic;
    }
}