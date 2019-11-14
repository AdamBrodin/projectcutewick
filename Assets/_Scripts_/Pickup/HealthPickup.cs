#pragma warning disable CS0649
using UnityEngine;

/* 
 * Developed by Adam Brodin
 * https://github.com/AdamBrodin
 */

public class HealthPickup : PickupBase
{
    #region Variables
    private GameObject playerObject;
    [SerializeField] private int amountOfHealth;
    #endregion

    private void Start() => playerObject = GameObject.FindGameObjectWithTag("Player");
    public override void Pickup()
    {
        playerObject?.GetComponent<Health>().ChangeHealth(amountOfHealth);
        base.Pickup();
    }
}
