using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemPickUpUIController : MonoBehaviour
{
    public static ItemPickUpUIController Instance { get; private set; }

    [SerializeField] GameObject popupPrefab;
    [SerializeField] int maxPopups = 5;
    [SerializeField] float popupDuration;

    [SerializeField] float maxSlotDimension = 70f;

    private readonly Queue<GameObject> activePopups = new();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("Multiple ItemPickUpUIManager instances detected! Destroying the extra one.");
            Destroy(gameObject);
        }
    }

    public void ShowItemPickup(string itemName, Sprite itemIcon)
    {
        GameObject newPopup = Instantiate(popupPrefab, transform);
        newPopup.GetComponentInChildren<TMP_Text>().text = itemName;

        Image itemImage = newPopup.transform.Find("ItemIcon")?.GetComponent<Image>();
        if (itemImage)
        {
            itemImage.sprite = itemIcon;
            SetItemSizeToSprite(itemImage.gameObject);
        }

        activePopups.Enqueue(newPopup);
        if (activePopups.Count > maxPopups)
        {
            Destroy(activePopups.Dequeue());
        }

        // Fade out and destroy
        StartCoroutine(FadeOutAndDestroy(newPopup));
    }

    private IEnumerator FadeOutAndDestroy(GameObject popup)
    {
        yield return new WaitForSeconds(popupDuration);
        if (popup == null) yield break;

        CanvasGroup canvasGroup = popup.GetComponent<CanvasGroup>();
        for (float timePassed = 0f; timePassed < 1f; timePassed += Time.deltaTime)
        {
            if (popup == null) yield break;
            canvasGroup.alpha = 1f - timePassed;
            yield return null;
        }

        Destroy(popup);
    }

    private void SetItemSizeToSprite(GameObject item)
    {
        Image image = item.GetComponent<Image>();
        RectTransform rectTransform = item.GetComponent<RectTransform>();

        if (image != null && image.sprite != null)
        {
            Vector2 spriteSize = image.sprite.bounds.size;
            float maxSlotSize = maxSlotDimension; // Max size for width/height

            // Calculate the scale factor based on the larger dimension
            float scaleFactor = maxSlotSize / Mathf.Max(spriteSize.x, spriteSize.y);

            // Apply the scale while maintaining the aspect ratio
            rectTransform.sizeDelta = new Vector2(spriteSize.x * scaleFactor, spriteSize.y * scaleFactor);
        }
    }

}
