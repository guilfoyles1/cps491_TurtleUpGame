<<<<<<< HEAD
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

    private GameScore gameScore;

    void Start()
    {
        inventorySFX = GetComponent<AudioSource>();
        canvasGroup = GetComponent<CanvasGroup>();
        gameScore = GameObject.Find("GameController").GetComponent<GameScore>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (SettingsMenu.Instance != null)
        {
            inventorySFX.volume = SettingsMenu.Instance.mainVolumeSlider.value - 0.3f;
        }
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

            //Check bin tag vs. item tag
            Transform bin = dropSlot.transform.parent; // assuming slot is a child of the bin

            if (bin != null && bin.tag.EndsWith("Bin"))
            {
                string binTag = bin.tag;
                string itemTag = draggedItem.tag;

                // "Glass" + "Bin" == GlassBin
                if (itemTag + "Bin" == binTag)
                {
                    Debug.Log("Correct");
                    gameScore.AddStreakedScore(true);


                    GameObject actualBin = GameObject.Find(binTag.ToLower()); // e.g., "glassbin"

                    if (actualBin != null)
                    {
                        Transform icon = actualBin.transform.Find("CorrectIcon");
                        if (icon != null)
                        {
                            FloatUpAndFadeOnce anim = icon.GetComponent<FloatUpAndFadeOnce>();

                            if (!icon.gameObject.activeSelf)
                            {
                                icon.gameObject.SetActive(true);
                            }
                            else
                            {
                                anim.Restart();
                            }
                        }
                    }
                    Destroy(draggedItem, 0.5f);

                }
                else
                {
                    Debug.Log("Incorrect");
                    gameScore.SubtractScore(500);
                    gameScore.ResetStreak();


                    GameObject actualBin = GameObject.Find(binTag.ToLower()); // e.g., "glassbin"

                    if (actualBin != null)
                    {
                        Transform icon = actualBin.transform.Find("IncorrectIcon");
                        if (icon != null)
                        {
                            FloatUpAndFadeOnce anim = icon.GetComponent<FloatUpAndFadeOnce>();

                            if (!icon.gameObject.activeSelf)
                            {
                                icon.gameObject.SetActive(true);
                            }
                            else
                            {
                                anim.Restart();
                            }
                        }
                    }

                }

            }
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


<<<<<<< HEAD

=======
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
        if (SettingsMenu.Instance != null)
        {
            inventorySFX.volume = SettingsMenu.Instance.mainVolumeSlider.value - 0.3f;
        }
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

            //Check bin tag vs. item tag
            Transform bin = dropSlot.transform.parent; // assuming slot is a child of the bin
            if (bin != null)
            {
                string binTag = bin.tag;
                string itemTag = draggedItem.tag;

                // "Glass" + "Bin" == GlassBin
                if (itemTag + "Bin" == binTag)
                {
                    Debug.Log("Correct");
                }
                else
                {
                    Debug.Log("Incorrect");
                }
            }
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



>>>>>>> 170d36684f19b92e12997d1a1e72fd5da00dcd84
=======
>>>>>>> 6af946e50caac2f9a9125b0a87e2d9965404b100
}