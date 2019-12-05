using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryDisplayer : MonoBehaviour
{
    [SerializeField]
    InventoryManager inventoryManager;

    private Image[] itemSlotImages;

    private void Start()
    {
        itemSlotImages = new Image[inventoryManager.itemSlots.Count];

        for (int i = 0; i < inventoryManager.itemSlots.Count; i++)
            itemSlotImages[i] = inventoryManager.itemSlots[i].transform.GetChild(0).GetChild(0).GetComponent<Image>();
    }

    private void OnEnable()
    {
        inventoryManager.ItemAddEvent += AddItemSprite;
        inventoryManager.ItemRemoveEvent += DisableItemSprite;
    }

    public void DisableItemSprite(int index)
    {
        itemSlotImages[index].enabled = false;
        itemSlotImages[index].sprite = null;
    }

    private void AddItemSprite(GameObject item, int index)
    {
        itemSlotImages[index].sprite = item.GetComponent<SpriteRenderer>().sprite; // Sets the itemslot's item sprite to the itemSprite
        itemSlotImages[index].enabled = true;
    }
}
