using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Transform originalParent;
    private CanvasGroup canvasGroup;
    private GameObject draggedItem;
    private AudioSource inventorySFX;

    void Start()
    {
        inventorySFX = GetComponent<AudioSource>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        inventorySFX.pitch = Random.Range(.5f, 1f);
        inventorySFX.Play();
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
            draggedItem.transform.SetParent(originalParent);
        }

        draggedItem.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
    }


}