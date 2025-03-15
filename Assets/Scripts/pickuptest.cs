using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Pickuptest : MonoBehaviour
{
    public GameObject pickupPrefab; // Prefab for each pickup notification
    public Transform pickupContainer; // Parent object where pickups stack
    public float displayTime = 1f; // Time before each pickup disappears

    private List<GameObject> activePickups = new List<GameObject>(); // Store active notifications

    public void ShowPickup(Sprite itemSprite, string itemName)
    {
        // Create a new pickup UI instance
        GameObject newPickup = Instantiate(pickupPrefab, pickupContainer);
        Image pickupImage = newPickup.transform.Find("PickupImage").GetComponent<Image>();
        TextMeshProUGUI pickupText = newPickup.transform.Find("PickupText").GetComponent<TextMeshProUGUI>();

        pickupImage.sprite = itemSprite;
        pickupText.text = itemName;

        SetImageSize(pickupImage, itemSprite);

        activePickups.Add(newPickup);

        StartCoroutine(HideAfterDelay(newPickup));
    }

    IEnumerator HideAfterDelay(GameObject pickup)
    {
        yield return new WaitForSeconds(displayTime);

        activePickups.Remove(pickup);
        Destroy(pickup);
    }

    private void SetImageSize(Image image, Sprite sprite)
    {
        if (sprite != null)
        {
            image.SetNativeSize();

            float maxSize = 50f; // Adjust this value for consistency
            float scale = Mathf.Min(maxSize / sprite.texture.width, maxSize / sprite.texture.height);

            image.rectTransform.sizeDelta = new Vector2(
                sprite.texture.width * scale,
                sprite.texture.height * scale
            );
        }
    }
}
