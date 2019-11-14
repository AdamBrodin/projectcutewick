using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [SerializeField]
    private int nrOfItemSlots;
    [SerializeField]
    [Range(0,1)]
    private float itemSlotSize = 0.54f;
    [SerializeField]
    private GameObject itemSlotPrefab;

    [HideInInspector]
    public List<GameObject> itemSlots;
    private Image[] itemSlotImages;
    private Image[] itemSlotBorders;

    public GameObject[] Items { get; private set; }
    private int itemCount = 0;

    [HideInInspector]
    public GameObject selectedItem;
    [HideInInspector]
    public int selectedItemIndex = 0;
    [SerializeField]
    private float timeBetweenRapidScroll;
    private bool isScrolling = false;
    private float scrollInput = 0;
    private bool firstLoop = true;

    /*private void OnValidate() //Automate the set up proccess
    {
        Rect inventoryRect = GetComponent<RectTransform>().rect;

        if (nrOfItemSlots > itemSlots.Count)
        {
            for (int i = 0; i < nrOfItemSlots - itemSlots.Count; i++) // Adds itemslots until the count matches nrOfItemSlots
            {
                GameObject newItemSlot = Instantiate(itemSlotPrefab, transform);
                itemSlots.Add(newItemSlot);
            }

            for (int i = 0; i < itemSlots.Count; i++)
            {
                Rect itemSlotRect = itemSlots[i].GetComponent<Rect>();
                itemSlotRect.size = new Vector2(itemSlotSize * inventoryRect.height, itemSlotSize * inventoryRect.height);

                float itemSlotGap = inventoryRect.width / nrOfItemSlots;
                itemSlotRect.position = new Vector2(itemSlotGap * i - inventoryRect.x / 2f, inventoryRect.y);
            }
        }
        else if (nrOfItemSlots < itemSlots.Count)
        {
            for (int i = 0; i < itemSlots.Count - nrOfItemSlots; i++) // remove all of the overflowing item slots
                itemSlots.RemoveAt(itemSlots.Count - i);

            for (int i = 0; i < itemSlots.Count; i++)
            {
                Rect itemSlotRect = itemSlots[i].GetComponent<Rect>();
                itemSlotRect.size = new Vector2(itemSlotSize * inventoryRect.height, itemSlotSize * inventoryRect.height);

                float itemSlotGap = inventoryRect.width / nrOfItemSlots;
                itemSlotRect.position = new Vector2(itemSlotGap * i - inventoryRect.x / 2f, inventoryRect.y);
            }
        }
    }*/

    private void Awake()
    {
        itemSlotImages = new Image[itemSlots.Count];
        itemSlotBorders = new Image[itemSlots.Count];
        Items = new GameObject[itemSlots.Count];

        for (int i = 0; i < itemSlots.Count; i++)
        {
            itemSlots[i] = transform.GetChild(i).gameObject; // Sets the item slot to the corresponding item slot in unity
            itemSlotImages[i] = itemSlots[i].transform.GetChild(0).GetChild(0).GetComponent<Image>(); // Sets the item image to the item image used by unity
            itemSlotBorders[i] = itemSlots[i].transform.GetChild(0).GetComponent<Image>(); // Sets the item border to the item border in unity
        }
    }

    // To control when you start rapid scrolling change the sensitivity in the input settings for the Keyboard Scroll axis.
    private void Update()
    {
        if (Input.GetAxisRaw("Keyboard Scroll") > 0 || Input.GetAxisRaw("Mouse ScrollWheel") < 0) // Get axis raw for instant input detection (single tap, double tap, triple tap...)
            Scroll(1);
        else if (Input.GetAxisRaw("Keyboard Scroll") < 0 || Input.GetAxisRaw("Mouse ScrollWheel") > 0)
            Scroll(-1);
        if (Input.GetAxisRaw("Keyboard Scroll") == 0)
        { firstLoop = true; isScrolling = false; }

        selectedItem = Items[selectedItemIndex];
        for (int i = 0; i < itemSlots.Count; i++) // Highlights the selected item with the item border
        {
            if (i == selectedItemIndex)
                itemSlotBorders[i].enabled = true;
            else { itemSlotBorders[i].enabled = false; }
        }
    }

    /// <summary>
    /// The main method to control the scrolling.
    /// </summary>
    /// <param name="direction">Controls in which direction the scrolling happens. (Clamped between -1 and 1.)</param>
    private void Scroll(int direction)
    {
        if (IsBetween(selectedItemIndex + direction, -1, itemSlots.Count)) // Get axis raw for that instant input detection (single tap, double tap, triple tap...)
        {
            if (firstLoop) // Only runs the first frame the button is help down
            {
                firstLoop = false;
                selectedItemIndex += direction;
            }

            scrollInput = Input.GetAxis("Keyboard Scroll");
            if ((scrollInput == 1 || scrollInput == -1) && !isScrolling) // Get axis and makes sure RapidScrolling isn't already running for the rapid scrolling action (Holding down input to scroll)
                StartCoroutine(RapidScroll(direction));
        }

    }

    /// <summary>
    /// Coroutine to rapidly scroll through the items.
    /// </summary>
    /// <param name="direction">Number of items to scroll in the direction passed per scroll.</param>
    /// <returns></returns>
    private IEnumerator RapidScroll(int direction)
    {
        isScrolling = true;

        while (isScrolling && scrollInput == direction && IsBetween(selectedItemIndex + direction, -1, itemSlots.Count))
        {
            selectedItemIndex += direction;
            yield return new WaitForSecondsRealtime(timeBetweenRapidScroll);
        }
        isScrolling = false;
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
                Sprite itemSprite = item.GetComponent<SpriteRenderer>().sprite;
                itemSlotImages[i].sprite = itemSprite; // Sets the itemslot's item sprite to the itemSprite
                itemSlotImages[i].enabled = true;
                itemSlotBorders[i].enabled = true;

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
        itemSlotImages[index].enabled = false;
        itemSlotImages[index].sprite = null;
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

    /// <summary>
    /// Checks if c is between a and b.
    /// </summary>
    /// <param name="value">The value that is being checked if it's between a and b.</param>
    /// <param name="min">Min value for the operation.</param>
    /// <param name="max">Max value for the operation.</param>
    /// <returns></returns>
    private bool IsBetween(float value, float min, float max)
    {
        if (max < min) // If the user does a fucky this solves the problem by doing a fixy. (It changes the a and b parameters around.)
        {
            float cache = max;
            max = min;
            min = cache;
        }

        if (value > min && value < max) // Checks if c is between a and b
            return true;
        else return false;
    }
}
