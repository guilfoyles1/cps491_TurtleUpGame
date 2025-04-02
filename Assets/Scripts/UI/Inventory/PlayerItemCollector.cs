using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemCollector : MonoBehaviour
{
    private InventoryController inventoryController;
    [SerializeField] AudioSource pickupSFX;
    private HashSet<string> validTags = new HashSet<string> { "Glass", "Paper", "Plastic", "Metal", "Trash" };

    void Start()
    {
        inventoryController = FindObjectOfType<InventoryController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (validTags.Contains(collision.gameObject.tag))
        {
            Item item = collision.GetComponent<Item>();
            if (item != null)
            {
                //add item to inventory
                bool itemAdded = inventoryController.AddItem(collision.gameObject);
                if (itemAdded)
                {
                    item.PickUp();
                    pickupSFX.pitch = UnityEngine.Random.Range(1f, 2f);
                    pickupSFX.Play();
                    Destroy(collision.gameObject);
                }
            }
        }
    }
}
