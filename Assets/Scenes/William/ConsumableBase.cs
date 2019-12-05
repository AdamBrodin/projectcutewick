using UnityEngine;

public abstract class ConsumableBase : MonoBehaviour, IConsumable
{
    [SerializeField] protected int maxUses;
    [HideInInspector] public int usesRemaining;
    /// <summary>The item that the current item becomes after being used up. Null is valid as none.</summary>
    protected GameObject[] itemAfterUse;

    public abstract void Use();
    public abstract void OnEmpty();
}
