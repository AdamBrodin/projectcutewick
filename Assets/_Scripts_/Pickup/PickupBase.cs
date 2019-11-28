#pragma warning disable CS0649
using System;
using UnityEngine;

/* 
 * Developed by Adam Brodin
 * https://github.com/AdamBrodin
 */

public class PickupBase : MonoBehaviour, IPickupable
{
    #region Singleton
    private static PickupBase instance;
    public static PickupBase Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<PickupBase>();
            }
            return instance;
        }
    }
    #endregion
    #region Variables
    public string PickupSoundName => soundName;
    [SerializeField] private string soundName, pickupName;
    public Action<string> itemPickedUp;
    #endregion

    public virtual void Pickup()
    {
        // Pickup specific stuff first
        itemPickedUp?.Invoke(pickupName);

        // Destroy and play audio
        AudioManager.Instance?.SetState(PickupSoundName, true);
        Destroy(gameObject);
    }
}
