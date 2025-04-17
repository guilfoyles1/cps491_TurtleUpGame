using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

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

    [Header("Optional Player Input Scripts to Disable")]
    public List<MonoBehaviour> playerInputScripts; // Add movement scripts here

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
    }

    void NextCharacter()
    {
        currentIndex = (currentIndex + 1) % characters.Count;
        UpdateSelection();
    }

    void UpdateSelection()
    {
        for (int i = 0; i < characters.Count; i++)
        {
            bool isSelected = (i == currentIndex);
            characters[i].SetActive(isSelected);

            if (isSelected)
            {
                // Set animation direction to "face down"
                Animator anim = characters[i].GetComponent<Animator>();
                if (anim != null && anim.runtimeAnimatorController != null)
                {
                    anim.SetFloat("moveY", -1f);
                }
            }
        }

        virtualCam.Follow = characters[currentIndex].transform;
    }

    void StartGame()
    {
        // Hide selection panel
        characterSelectPanel.SetActive(false);

        // Zoom out for gameplay
        virtualCam.m_Lens.OrthographicSize = 7;

        // Enable movement
        DisablePlayerControls(false);
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
