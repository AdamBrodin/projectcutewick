using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [SerializeField]
    private InventoryDisplayer inventoryDisplayer;
    public Transform inventory;

    /// <summary>
    /// Broadcasts when an item is added to the inventory. GameObject is item added, Int is the items index.
    /// </summary>
    public event Action<GameObject, int> ItemAddEvent;
    public event Action<int> ItemRemoveEvent;

    public List<GameObject> itemSlots;
    [HideInInspector]
    public GameObject[] Items { get; private set; }
    [HideInInspector]
    public int itemCount = 0;

    private void Awake()
    {
        Items = new GameObject[itemSlots.Count];

        for (int i = 0; i < itemSlots.Count; i++)
            itemSlots[i] = inventory.GetChild(i).gameObject; // Sets the item slot to the corresponding item slot
    }

    /// <summary>
    /// Adds an item to the inventory.
    /// </summary>
    /// <param name="item">The item GameObject that will get added to the inventory.</param>
    public void AddItem(GameObject item)
    {
        if (itemCount < itemSlots.Count - 1) // Checks to see that there is a space left in the inventory
            itemCount++; // If there is space in the inventory increases the item count
        else return; // Returns if there isn't any space left in the inventory

        for (int i = 0; i <= itemCount; i++)
        {
            if (Items[i] == null) // If the item slot with the index i is free add the item to it
            {
                ItemAddEvent(item, i);
                //itemSelector.itemSlotBorders[i].enabled = true;                                   -----MAYBE REMOVE THIS LINEd-----

                Items[i] = item;
                break;
            }
        }

    }

    /// <summary>
    /// Removes an item to the inventory by the index passed.
    /// </summary>
    /// <param name="index">Index of the item that will get removed.</param>
    public void RemoveItem(int index)
    {
        Items[index] = null;
        if (inventoryDisplayer)
            inventoryDisplayer.DisableItemSprite(index);
        itemCount--;
    }

    /// <summary>
    /// This function is to switch two items' itemslots in the inventory.
    /// </summary>
    /// <param name="currentIndex">Index of the currently selected itemslot.</param>
    /// <param name="newIndex">Index of the itemslot to move the currently selected item to.</param>
    public void MoveItem(int currentIndex, int newIndex)
    {
        GameObject cachedItem = Items[currentIndex]; // Caches the currently selected item

        Items[currentIndex] = Items[newIndex]; // Switches the items around
        Items[newIndex] = cachedItem;
    }
}
