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

        var canvasGroup = draggedItem.GetComponent<CanvasGroup>();
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;

        Slot originalSlot = originalParent.GetComponent<Slot>();
        Slot dropSlot = eventData.pointerEnter?.GetComponent<Slot>();

        if (dropSlot == null && eventData.pointerEnter != null)
        {
            dropSlot = eventData.pointerEnter.GetComponentInParent<Slot>();
        }

        if (dropSlot != null && dropSlot != originalSlot)
        {
            // Clear original slot
            originalSlot.currentItem = null;

            if (dropSlot.currentItem != null)
            {
                // Swap: move the existing item to original slot
                dropSlot.currentItem.transform.SetParent(originalSlot.transform);
                dropSlot.currentItem.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                originalSlot.currentItem = dropSlot.currentItem;
            }

            // Move dragged item to new slot
            draggedItem.transform.SetParent(dropSlot.transform);
            draggedItem.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            dropSlot.currentItem = draggedItem;
        }
        else
        {
            // Dropped outside or back to original slot
            draggedItem.transform.SetParent(originalParent);
            draggedItem.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            originalSlot.currentItem = draggedItem;
        }

        draggedItem = null;
    }



}