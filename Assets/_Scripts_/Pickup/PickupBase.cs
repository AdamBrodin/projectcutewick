#pragma warning disable CS0649
using UnityEngine;

/* 
 * Developed by Adam Brodin
 * https://github.com/AdamBrodin
 */

public class PickupBase : MonoBehaviour, IPickupable
{
    #region Variables
    #endregion
    public string PickupSoundName => soundName;
    [SerializeField] private string soundName;

    public virtual void Pickup()
    {
        // Pickup specific stuff first

        // Destroy and play audio
        AudioManager.Instance?.SetState(PickupSoundName, true);
        Destroy(gameObject);
    }
}
