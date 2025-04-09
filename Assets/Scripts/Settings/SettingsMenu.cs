<<<<<<< HEAD
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public Slider mainVolumeSlider;
    public Slider musicVolumeSlider;

    // Main SFX sources
    [SerializeField] AudioSource forestSFX;
    [SerializeField] AudioSource waterfallSFX;
    [SerializeField] AudioSource fireSFX;
    [SerializeField] AudioSource walkSFX;
    [SerializeField] AudioSource uiSFX;
    [SerializeField] AudioSource pickupSFX;

    // Music source
    [SerializeField] AudioSource musicSFX;

    // Reference to SeaSFXTrigger 
    private SeaSFXTrigger seaSFXTrigger;
    public static SettingsMenu Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;

        //DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        // PlayerPrefs.DeleteAll();
        // PlayerPrefs.Save();
        seaSFXTrigger = FindObjectOfType<SeaSFXTrigger>();
        // Save main volume and music volume separately to fix saving issues
        if (!PlayerPrefs.HasKey("mainVolume"))
        {
            mainVolumeSlider.value = 1f;
            PlayerPrefs.SetFloat("mainVolume", 1f);
            SaveSettings();
        }
        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            musicVolumeSlider.value = .5f;
            PlayerPrefs.SetFloat("musicVolume", .5f);
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
        uiSFX.volume = mainVolumeSlider.value;
        pickupSFX.volume = mainVolumeSlider.value;
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
            seaSFXTrigger.playerSetVolume = mainVolumeSlider.value - .4f;
            seaSFXTrigger.setVolume();
        }
        forestSFX.volume = mainVolumeSlider.value;
        waterfallSFX.volume = mainVolumeSlider.value;
        fireSFX.volume = mainVolumeSlider.value;
        walkSFX.volume = mainVolumeSlider.value - .2f;
        pickupSFX.volume = mainVolumeSlider.value;
        uiSFX.volume = mainVolumeSlider.value - .4f;
        musicSFX.volume = Mathf.Clamp(musicVolumeSlider.value * .25f, 0f, 1f);
    }
}
=======
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public Slider mainVolumeSlider;
    public Slider musicVolumeSlider;

    // Main SFX sources
    [SerializeField] AudioSource forestSFX;
    [SerializeField] AudioSource waterfallSFX;
    [SerializeField] AudioSource fireSFX;
    [SerializeField] AudioSource walkSFX;
    [SerializeField] AudioSource uiSFX;
    [SerializeField] AudioSource pickupSFX;

    // Music source
    [SerializeField] AudioSource musicSFX;

    // Reference to SeaSFXTrigger 
    private SeaSFXTrigger seaSFXTrigger;
    public static SettingsMenu Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;

        //DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        // PlayerPrefs.DeleteAll();
        // PlayerPrefs.Save();
        seaSFXTrigger = FindObjectOfType<SeaSFXTrigger>();
        // Save main volume and music volume separately to fix saving issues
        if (!PlayerPrefs.HasKey("mainVolume"))
        {
            mainVolumeSlider.value = 1f;
            PlayerPrefs.SetFloat("mainVolume", 1f);
            SaveSettings();
        }
        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            musicVolumeSlider.value = .5f;
            PlayerPrefs.SetFloat("musicVolume", .5f);
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
        uiSFX.volume = mainVolumeSlider.value;
        pickupSFX.volume = mainVolumeSlider.value;
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
            seaSFXTrigger.playerSetVolume = mainVolumeSlider.value - .4f;
            seaSFXTrigger.setVolume();
        }
        forestSFX.volume = mainVolumeSlider.value;
        waterfallSFX.volume = mainVolumeSlider.value;
        fireSFX.volume = mainVolumeSlider.value;
        walkSFX.volume = mainVolumeSlider.value - .2f;
        pickupSFX.volume = mainVolumeSlider.value;
        uiSFX.volume = mainVolumeSlider.value - .4f;
        musicSFX.volume = Mathf.Clamp(musicVolumeSlider.value * .25f, 0f, 1f);
    }
}
>>>>>>> 170d36684f19b92e12997d1a1e72fd5da00dcd84
