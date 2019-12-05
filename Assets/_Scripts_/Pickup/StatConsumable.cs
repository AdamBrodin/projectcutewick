#pragma warning disable CS0649
using UnityEngine;

/* 
 * Developed by Adam Brodin
 * https://github.com/AdamBrodin
 */

public class StatConsumable : ConsumableBase
{
    #region Variables
    [SerializeField] private int healthEffect;
    [SerializeField] private float radiationEffect;

    private InventoryManager inventoryManager;
    #endregion

    private void Start()
    {
        inventoryManager = GameObject.Find("Player").GetComponent<InventoryManager>();

        usesRemaining = maxUses;
    }

    public override void Use()
    {
        usesRemaining--;
        Player.Instance.ChangeHealth(healthEffect);
    }

    public override void OnEmpty()
    {
    }
}
