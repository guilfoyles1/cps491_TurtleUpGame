using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using TMPro;


public class CharacterSelectionManager : MonoBehaviour
{
    [Header("Character Selection")]
    public List<GameObject> characters;
    public CinemachineVirtualCamera virtualCam;
    public GameObject characterSelectPanel;

    public Button leftButton;
    public Button rightButton;
    public Button startGameButton;

    private int currentIndex = 0;

    [Header("UI")]
    public TextMeshProUGUI descriptionText; // Or just Text if you're not using TMP

    [TextArea]
    public List<string> characterDescriptions = new List<string>
    {
    "++inventory    -speed",
    "++speed    -inventory",
    "+inventory     +speed"
    };


    [Header("Optional Player Input Scripts to Disable")]
    public List<MonoBehaviour> playerInputScripts; // Add movement scripts here

    [Header("Inventory Panels - order based on characters")]
    public List<GameObject> inventoryPanels; // Must be same size as characters


    [Header("Audio")]
    public AudioSource clickSound;


    void Awake()
    {
        // Disable all InventoryControllers at the beginning
        foreach (var character in characters)
        {
            InventoryController inv = character.GetComponent<InventoryController>();
            if (inv != null) inv.enabled = false;
        }
    }

    void Start()
    {
        // Zoom in for character selection
        virtualCam.m_Lens.OrthographicSize = 3;

        // Hook up buttons
        leftButton.onClick.AddListener(PreviousCharacter);
        rightButton.onClick.AddListener(NextCharacter);
        startGameButton.onClick.AddListener(StartGame);

        UpdateSelection();
        DisablePlayerControls(true); // Disable movement at the start
    }

    void PreviousCharacter()
    {
        currentIndex = (currentIndex - 1 + characters.Count) % characters.Count;
        UpdateSelection();
        PlayClickSound();
    }

    void NextCharacter()
    {
        currentIndex = (currentIndex + 1) % characters.Count;
        UpdateSelection();
        PlayClickSound();
    }

    void PlayClickSound()
    {
        if (clickSound != null)
            clickSound.Play();
    }


    void UpdateSelection()
    {
        for (int i = 0; i < characters.Count; i++)
        {
            bool isSelected = (i == currentIndex);
            characters[i].SetActive(isSelected);

            if (isSelected)
            {
                Animator anim = characters[i].GetComponent<Animator>();
                if (anim != null && anim.runtimeAnimatorController != null)
                {
                    anim.SetFloat("moveY", -1f);
                }
            }
        }

        virtualCam.Follow = characters[currentIndex].transform;

        // Update the character description text
        if (descriptionText != null && currentIndex < characterDescriptions.Count)
        {
            descriptionText.text = characterDescriptions[currentIndex];
        }
    }


    void StartGame()
    {
        characterSelectPanel.SetActive(false);
        virtualCam.m_Lens.OrthographicSize = 7;
        DisablePlayerControls(false);

        // Enable InventoryController if needed
        GameObject selectedPlayer = characters[currentIndex];
        InventoryController inventory = selectedPlayer.GetComponent<InventoryController>();
        if (inventory != null)
        {
            inventory.enabled = true;
        }

        // Enable matching inventory panel (if using)
        for (int i = 0; i < inventoryPanels.Count; i++)
        {
            if (inventoryPanels[i] != null)
                inventoryPanels[i].SetActive(i == currentIndex);
        }

        // Disable the description text
        if (descriptionText != null)
        {
            descriptionText.gameObject.SetActive(false);
        }
    }


    void DisablePlayerControls(bool disable)
    {
        foreach (var script in playerInputScripts)
        {
            if (script != null)
                script.enabled = !disable;
        }
    }
}
