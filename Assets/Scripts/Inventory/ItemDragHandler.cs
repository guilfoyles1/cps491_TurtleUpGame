using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Transform originalParent;
    private CanvasGroup canvasGroup;
    private GameObject draggedItem;

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();

        // Ensure this slot is added to the global slot list
        Slot slot = GetComponent<Slot>();
        if (slot != null && !InventoryController.allInventorySlots.Contains(slot))
        {
            InventoryController.allInventorySlots.Add(slot);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Slot slot = GetComponent<Slot>();
        if (slot == null || slot.currentItem == null)
            return; // No item in slot, so don't drag

        draggedItem = slot.currentItem; // Store reference to the item
        originalParent = draggedItem.transform.parent; // Save original parent slot

        draggedItem.transform.SetParent(transform.root); // Move item above other UI elements
        draggedItem.GetComponent<CanvasGroup>().blocksRaycasts = false;
        draggedItem.GetComponent<CanvasGroup>().alpha = 0.6f; // Semi-transparent during drag
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (draggedItem == null) return;
        draggedItem.transform.position = eventData.position; // Move the item with the cursor
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (draggedItem == null) return;

        draggedItem.GetComponent<CanvasGroup>().blocksRaycasts = true;
        draggedItem.GetComponent<CanvasGroup>().alpha = 1f;

        Slot dropSlot = eventData.pointerEnter?.GetComponent<Slot>();

        if (dropSlot == null)
        {
            GameObject item = eventData.pointerEnter;
            if (item != null)
            {
                dropSlot = item.GetComponentInParent<Slot>();
            }
        }

        // If no valid drop slot is found, check across all inventories
        if (dropSlot == null)
        {
            foreach (Slot slot in InventoryController.allInventorySlots)
            {
                if (RectTransformUtility.RectangleContainsScreenPoint(
                    slot.GetComponent<RectTransform>(), Input.mousePosition))
                {
                    dropSlot = slot;
                    break;
                }
            }
        }

        Slot originalSlot = originalParent.GetComponent<Slot>();

        if (dropSlot != null)
        {
            if (dropSlot.currentItem != null)
            {
                // Swap items between slots
                dropSlot.currentItem.transform.SetParent(originalSlot.transform);
                originalSlot.currentItem = dropSlot.currentItem;
                dropSlot.currentItem.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            }
            else
            {
                originalSlot.currentItem = null;
            }
            // Move dragged item into the drop slot
            draggedItem.transform.SetParent(dropSlot.transform);
            dropSlot.currentItem = draggedItem;
        }
        else
        {
            // No valid drop slot, return item to its original slot
            draggedItem.transform.SetParent(originalParent);
        }

        draggedItem.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
    }
}
