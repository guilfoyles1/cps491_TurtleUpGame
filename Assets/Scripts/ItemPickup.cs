using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
using System.Collections;

public class PlayerInventory : MonoBehaviour
{
    public Tilemap trashTilemap; // Assign your trash Tilemap in the Inspector
    public List<GameObject> inventorySlots = new List<GameObject>(); // 7 inventory slots (GameObject, not Image)

    private int currentSlot = 0; // Track the next available slot

    public PickupUI pickup;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            RemoveLastItem();
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Trash"))
        {
            Vector3 hitPosition = other.ClosestPoint(transform.position);
            Vector3Int tilePosition = trashTilemap.WorldToCell(hitPosition);

            // Get the sprite of the removed tile
            TileBase tile = trashTilemap.GetTile(tilePosition);
            if (tile != null)
            {
                Sprite tileSprite = (tile as Tile)?.sprite;
                if (tileSprite != null)
                {
                    AddToInventory(tileSprite); // Add to inventory
                    pickup.ShowPickup(tileSprite, "+1 trash");
                }
            }

            // Remove the tile from the Tilemap
            trashTilemap.SetTile(tilePosition, null);
        }
    }

    IEnumerator FlashSlot(Image slot)
    {
        Color originalColor = slot.color;
        slot.color = Color.yellow; // Highlight effect
        yield return new WaitForSeconds(0.2f);
        slot.color = originalColor;
    }

    private void AddToInventory(Sprite itemSprite)
    {
        if (currentSlot < inventorySlots.Count)
        {
            GameObject slot = inventorySlots[currentSlot];
            Image itemImage = slot.transform.Find("ItemImage").GetComponent<Image>();

            itemImage.sprite = itemSprite;
            itemImage.enabled = true;

            // Adjust the image size to match the sprite's original size
            SetImageSize(itemImage, itemSprite);

            StartCoroutine(FlashSlot(slot.GetComponent<Image>())); // Flash effect

            currentSlot++;
        }
        else
        {
            Debug.Log("Inventory is full!");
        }
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

    private void RemoveLastItem()
    {
        if (currentSlot > 0) // Ensure there are items to remove
        {
            currentSlot--; // Move back a slot

            GameObject slot = inventorySlots[currentSlot]; // Get the slot GameObject
            Image itemImage = slot.transform.Find("ItemImage").GetComponent<Image>(); // Find the child image

            itemImage.sprite = null; // Clear the sprite
            itemImage.enabled = false; // Hide the image

            Debug.Log("Removed item from inventory.");
        }
        else
        {
            Debug.Log("Inventory is empty!");
        }
    }
}
