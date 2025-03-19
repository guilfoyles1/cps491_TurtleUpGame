using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public Slider mainVolumeSlider;
    public Slider musicVolumeSlider;

    // Main SFX sources
    public AudioSource forestSFX;
    public AudioSource waterfallSFX;
    public AudioSource fireSFX;
    public AudioSource walkSFX;

    // Music source
    public AudioSource musicSFX;

    // Reference to SeaSFXTrigger 
    private SeaSFXTrigger seaSFXTrigger;

    void Start()
    {
        seaSFXTrigger = FindObjectOfType<SeaSFXTrigger>();
        //save main volume and music volume separately to fix saving issues
        if (!PlayerPrefs.HasKey("mainVolume"))
        {
            mainVolumeSlider.value = 1f;
            PlayerPrefs.SetFloat("mainVolume", 1f);
            SaveSettings();
        }
        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            musicVolumeSlider.value = 1f;
            PlayerPrefs.SetFloat("musicVolume", 1f);
            SaveMusicSettings();
        }
        LoadSettings();
        CustomSoundRange();
    }

    public void ChangeVolume()
    {
        if (seaSFXTrigger != null)
        {
            seaSFXTrigger.playerSetVolume = mainVolumeSlider.value;
            seaSFXTrigger.setVolume();
        }
        forestSFX.volume = mainVolumeSlider.value;
        waterfallSFX.volume = mainVolumeSlider.value;
        fireSFX.volume = mainVolumeSlider.value;
        walkSFX.volume = mainVolumeSlider.value;
        SaveSettings();
        CustomSoundRange();
    }

    public void ChangeMusicVolume()
    {
        musicSFX.volume = musicVolumeSlider.value;
        SaveMusicSettings();
        CustomSoundRange();
    }

    private void LoadSettings()
    {
        mainVolumeSlider.value = PlayerPrefs.GetFloat("mainVolume");
        musicVolumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }

    private void SaveSettings()
    {
        PlayerPrefs.SetFloat("mainVolume", mainVolumeSlider.value);
        PlayerPrefs.Save();
    }

    private void SaveMusicSettings()
    {
        PlayerPrefs.SetFloat("musicVolume", musicVolumeSlider.value);
        PlayerPrefs.Save();
    }

    private void CustomSoundRange()
    {
        if (seaSFXTrigger != null)
        {
            seaSFXTrigger.playerSetVolume = mainVolumeSlider.value - .2f;
            seaSFXTrigger.setVolume();
        }
        forestSFX.volume = mainVolumeSlider.value;
        waterfallSFX.volume = mainVolumeSlider.value;
        fireSFX.volume = mainVolumeSlider.value;
        walkSFX.volume = mainVolumeSlider.value - .3f;
        musicSFX.volume = Mathf.Clamp(musicVolumeSlider.value * .25f, 0f, 1f);
    }
}
