using UnityEngine;

public abstract class ConsumableBase : MonoBehaviour
{
    [SerializeField] protected int maxUses;
    protected int usesRemaining;
    /// <summary>The item that the current item becomes after being used up. Null is valid as none.</summary>
    protected GameObject[] itemAfterUse;
    public abstract void Use();
}
