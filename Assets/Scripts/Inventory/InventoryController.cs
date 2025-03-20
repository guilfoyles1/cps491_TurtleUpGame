using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    public GameObject inventoryPanel; // Player's inventory
    public GameObject binInventoryPanel; // Chest inventory
    public GameObject slotPrefab;
    public int slotCount;
    public GameObject[] itemPrefabs;

    // Global list to track all slots across multiple inventories
    public static List<Slot> allInventorySlots = new List<Slot>();

    void Start()
    {
        // Initialize player inventory
        InitializeInventory(inventoryPanel);

        // Initialize chest inventory
        if (binInventoryPanel != null)
        {
            InitializeInventory(binInventoryPanel);
        }
    }

    void Update()
    {
        // Check if the player presses 'F' and delete items in bin inventory
        if (Input.GetKeyDown(KeyCode.F))
        {
            ClearBinInventory();
        }
    }

    private void InitializeInventory(GameObject inventory)
    {
        for (int i = 0; i < slotCount; i++)
        {
            Slot slot = Instantiate(slotPrefab, inventory.transform).GetComponent<Slot>();

            // Add to global list of slots
            allInventorySlots.Add(slot);

            if (i < itemPrefabs.Length)
            {
                GameObject item = Instantiate(itemPrefabs[i], slot.transform);
                item.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

                // Set the item size to match the sprite size
                SetItemSizeToSprite(item);

                slot.currentItem = item;
            }
        }
    }

    public bool AddItem(GameObject itemPrefab)
    {
        foreach (Slot slot in allInventorySlots)
        {
            if (slot != null && slot.currentItem == null)
            {
                GameObject newItem = Instantiate(itemPrefab, slot.transform);
                newItem.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

                // Set the item size to match the sprite size
                SetItemSizeToSprite(newItem);

                slot.currentItem = newItem;
                return true;
            }
        }
        Debug.Log("Inventory is full!");
        return false;
    }

    private void SetItemSizeToSprite(GameObject item)
    {
        Image image = item.GetComponent<Image>();
        RectTransform rectTransform = item.GetComponent<RectTransform>();

        if (image != null && image.sprite != null)
        {
            Vector2 spriteSize = image.sprite.bounds.size;
            float maxSlotSize = 87f; // Max size for width/height

            // Calculate the scale factor based on the larger dimension
            float scaleFactor = maxSlotSize / Mathf.Max(spriteSize.x, spriteSize.y);

            // Apply the scale while maintaining the aspect ratio
            rectTransform.sizeDelta = new Vector2(spriteSize.x * scaleFactor, spriteSize.y * scaleFactor);
        }
    }

    private void ClearBinInventory()
    {
        if (binInventoryPanel == null)
        {
            Debug.LogWarning("Bin inventory panel not set!");
            return;
        }

        // Iterate through all slots in the bin inventory and delete items
        foreach (Transform slotTransform in binInventoryPanel.transform)
        {
            Slot slot = slotTransform.GetComponent<Slot>();
            if (slot != null && slot.currentItem != null)
            {
                Destroy(slot.currentItem); // Delete the item
                slot.currentItem = null; // Clear the reference
            }
        }

        Debug.Log("Bin inventory cleared!");
    }
}
