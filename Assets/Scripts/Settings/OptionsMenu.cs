<<<<<<< HEAD
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] GameObject optionsPanel;
    [SerializeField] GameObject settingsPanel;

    [SerializeField] GameObject optionsButton;

    [SerializeField] AudioSource uiSounds;
    [SerializeField] AudioClip swipeSound;
    [SerializeField] AudioClip clickSound;

    private bool isMenuOpen = false;

    void Start()
    {
        if (optionsPanel != null)
            optionsPanel.SetActive(false);
        if (settingsPanel != null)
            settingsPanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMenu();
        }
    }

    public void ToggleMenu()
    {
        if (optionsPanel != null)
        {
            optionsButton.SetActive(isMenuOpen);
            isMenuOpen = !isMenuOpen;
            optionsPanel.SetActive(isMenuOpen);
            settingsPanel.SetActive(false);
            uiSounds.clip = swipeSound;
            uiSounds.Play();
        }
    }
    public void EnterSettings()
    {
        optionsPanel.SetActive(false);
        isMenuOpen = !isMenuOpen;
        settingsPanel.SetActive(true);
        uiSounds.clip = swipeSound;
        uiSounds.Play();

    }

    public void EnterMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ExitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void PlayClickSound()
    {
        uiSounds.clip = clickSound;
        uiSounds.Play();
    }

}

=======
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] GameObject optionsPanel;
    [SerializeField] GameObject settingsPanel;

    [SerializeField] GameObject optionsButton;

    [SerializeField] AudioSource uiSounds;
    [SerializeField] AudioClip swipeSound;
    [SerializeField] AudioClip clickSound;

    private bool isMenuOpen = false;

    void Start()
    {
        if (optionsPanel != null)
            optionsPanel.SetActive(false);
        if (settingsPanel != null)
            settingsPanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMenu();
        }
    }

    public void ToggleMenu()
    {
        if (optionsPanel != null)
        {
            optionsButton.SetActive(isMenuOpen);
            isMenuOpen = !isMenuOpen;
            optionsPanel.SetActive(isMenuOpen);
            settingsPanel.SetActive(false);
            uiSounds.clip = swipeSound;
            uiSounds.Play();
        }
    }
    public void EnterSettings()
    {
        optionsPanel.SetActive(false);
        isMenuOpen = !isMenuOpen;
        settingsPanel.SetActive(true);
        uiSounds.clip = swipeSound;
        uiSounds.Play();

    }

    public void EnterMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ExitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void PlayClickSound()
    {
        uiSounds.clip = clickSound;
        uiSounds.Play();
    }

}

>>>>>>> 170d36684f19b92e12997d1a1e72fd5da00dcd84
