#pragma warning disable CS0649

/* 
 * Developed by Adam Brodin
 * https://github.com/AdamBrodin
 */

public interface IPickupable
{
    void Pickup();
    string PickupSoundName { get; }
}
