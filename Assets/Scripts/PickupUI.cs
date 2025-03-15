using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PickupUI : MonoBehaviour
{
    public GameObject pickupPanel; // UI Panel for pickup notification
    public Image pickupImage;      // Image for the picked item
    public TextMeshProUGUI pickupText;        // Text for item name
    public float displayTime = 1f; // How long to show the UI

    private Coroutine hideCoroutine;

    public void ShowPickup(Sprite itemSprite, string itemName)
    {
        pickupImage.sprite = itemSprite;
        pickupText.text = itemName;
        SetImageSize(pickupImage, itemSprite);
        pickupImage.enabled = true;
        pickupText.enabled = false;

        if (hideCoroutine != null) StopCoroutine(hideCoroutine);
        hideCoroutine = StartCoroutine(HideAfterDelay());
    }

    IEnumerator HideAfterDelay()
    {
        yield return new WaitForSeconds(displayTime);
        pickupImage.enabled = false;
        pickupText.enabled = false;
    }

    private void SetImageSize(Image image, Sprite sprite)
    {
        if (sprite != null)
        {
            image.SetNativeSize(); // Ensures the image matches the sprite's original size

            // Optional: Scale down if the image is too big for the UI
            float maxSize = 50f; // Adjust this value to fit your UI slot size
            float scale = Mathf.Min(maxSize / sprite.texture.width, maxSize / sprite.texture.height);

            image.rectTransform.sizeDelta = new Vector2(
                sprite.texture.width * scale,
                sprite.texture.height * scale
            );
        }
    }
}
