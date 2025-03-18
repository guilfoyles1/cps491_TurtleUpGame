using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemPickUpUIController : MonoBehaviour
{
    public static ItemPickUpUIController Instance { get; private set; }

    public GameObject popupPrefab;
    public int maxPopups = 5;
    public float popupDuration = 3f;

    private readonly Queue<GameObject> activePopups = new();
    private readonly Queue<GameObject> popupPool = new(); // Pool for UI popups

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        // Pre-instantiate popups to prevent lag
        for (int i = 0; i < maxPopups; i++)
        {
            GameObject popup = Instantiate(popupPrefab, transform);
            popup.SetActive(false);
            popupPool.Enqueue(popup);
        }
    }

    public void ShowItemPickup(string itemName, Sprite itemIcon)
    {
        GameObject newPopup;

        // Reuse an inactive popup from the pool instead of instantiating a new one
        if (popupPool.Count > 0)
        {
            newPopup = popupPool.Dequeue();
        }
        else
        {
            // If no available popup in pool, create a new one (but this should be rare)
            newPopup = Instantiate(popupPrefab, transform);
        }

        // Set popup details
        newPopup.GetComponentInChildren<TMP_Text>().text = itemName;
        UnityEngine.UI.Image itemImage = newPopup.transform.Find("ItemIcon")?.GetComponent<UnityEngine.UI.Image>();
        if (itemImage)
        {
            itemImage.sprite = itemIcon;
        }

        newPopup.SetActive(true);
        activePopups.Enqueue(newPopup);

        // If there are too many popups, deactivate and reuse the oldest one
        if (activePopups.Count > maxPopups)
        {
            GameObject oldPopup = activePopups.Dequeue();
            oldPopup.SetActive(false);
            popupPool.Enqueue(oldPopup);
        }

        StartCoroutine(FadeOutAndDeactivate(newPopup));
    }

    private IEnumerator FadeOutAndDeactivate(GameObject popup)
    {
        CanvasGroup canvasGroup = popup.GetComponent<CanvasGroup>();
        if (canvasGroup == null) yield break;

        float duration = popupDuration; // Fade duration
        for (float timePassed = 0f; timePassed < duration; timePassed += Time.deltaTime)
        {
            canvasGroup.alpha = 1f - (timePassed / duration);
            yield return null;
        }

        // After fade out, disable popup instead of destroying it
        popup.SetActive(false);
        popupPool.Enqueue(popup);
    }
}
