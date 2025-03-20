using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    public GameObject inventoryPanel;
    public GameObject slotPrefab;
    public int slotCount;
    public GameObject[] itemPrefabs;

    void Start()
    {
        for (int i = 0; i < slotCount; i++)
        {
            Slot slot = Instantiate(slotPrefab, inventoryPanel.transform).GetComponent<Slot>();
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
        foreach (Transform slotTransform in inventoryPanel.transform)
        {
            Slot slot = slotTransform.GetComponent<Slot>();
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

}