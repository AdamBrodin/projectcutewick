using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSelector : MonoBehaviour
{
    [SerializeField]
    private InventoryManager inventoryManager;

    [HideInInspector]
    public Image[] itemSlotBorders;

    private KeyCode[] keyCodes = {
         KeyCode.Alpha1,
         KeyCode.Alpha2,
         KeyCode.Alpha3,
         KeyCode.Alpha4,
         KeyCode.Alpha5,
         KeyCode.Alpha6,
         KeyCode.Alpha7,
         KeyCode.Alpha8,
         KeyCode.Alpha9,
    };

    [HideInInspector]
    public GameObject selectedItem;
    [HideInInspector]
    public int selectedItemIndex = 0;
    [SerializeField]
    private float timeBetweenRapidScroll;
    private bool isScrolling = false;
    private float scrollInput = 0;
    private bool firstLoop = true;



    private void Awake()
    {
        itemSlotBorders = new Image[inventoryManager.itemSlots.Count];
        for (int i = 0; i < inventoryManager.itemSlots.Count; i++)
            itemSlotBorders[i] = inventoryManager.itemSlots[i].transform.GetChild(0).GetComponent<Image>();
    }

    // To control when you start rapid scrolling change the sensitivity in the input settings for the Keyboard Scroll axis.
    void Update()
    {
        if (Input.GetAxisRaw("Keyboard Scroll") > 0 || Input.GetAxisRaw("Mouse ScrollWheel") < 0) // Get axis raw for instant input detection (single tap, double tap, triple tap...)
            Scroll(1);
        else if (Input.GetAxisRaw("Keyboard Scroll") < 0 || Input.GetAxisRaw("Mouse ScrollWheel") > 0)
            Scroll(-1);
        if (Input.GetAxisRaw("Keyboard Scroll") == 0)
        { firstLoop = true; isScrolling = false; }

        selectedItem = inventoryManager.Items[selectedItemIndex];
        for (int i = 0; i < inventoryManager.itemSlots.Count; i++) // Highlights the selected item with the item border
        {
            if (i == selectedItemIndex)
                itemSlotBorders[i].enabled = true;
            else { itemSlotBorders[i].enabled = false; }
        }

        for (int i = 0; i < keyCodes.Length; i++)
        {
            if (Input.GetKeyDown(keyCodes[i]))
                selectedItemIndex = i;
        }
    }

    /// <summary>
    /// The main method to control the scrolling.
    /// </summary>
    /// <param name="direction">Controls in which direction the scrolling happens. (Clamped between -1 and 1.)</param>
    private void Scroll(int direction)
    {
        if (IsBetween(selectedItemIndex + direction, -1, inventoryManager.itemSlots.Count)) // Get axis raw for that instant input detection (single tap, double tap, triple tap...)
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

        while (isScrolling && scrollInput == direction && IsBetween(selectedItemIndex + direction, -1, inventoryManager.itemSlots.Count))
        {
            selectedItemIndex += direction;
            yield return new WaitForSecondsRealtime(timeBetweenRapidScroll);
        }
        isScrolling = false;
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
