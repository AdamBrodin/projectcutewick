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
    #endregion

    public override void Use()
    {
        usesRemaining--;
        Player.Instance.ChangeHealth(healthEffect);

        if (usesRemaining <= 0)
        {

        }
    }
}
