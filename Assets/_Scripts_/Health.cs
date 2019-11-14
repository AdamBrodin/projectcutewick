#pragma warning disable CS0649
using UnityEngine;

/* 
 * Developed by Adam Brodin
 * https://github.com/AdamBrodin
 */

public abstract class Health : MonoBehaviour, IKillable
{
    #region Variables
    public int CurrentHealth { get => currentHealth; set => currentHealth = value; }
    public int StartHealth { get => startHealth; set => startHealth = value; }
    private int currentHealth, startHealth;
    #endregion

    public void ChangeHealth(int value)
    {
        if (CurrentHealth + value <= StartHealth)
        {
            CurrentHealth += value;
        }
        else
        {
            CurrentHealth = StartHealth;
        }

        OnHitEffect();
        if (CurrentHealth <= 0) { Die(); }
    }

    protected virtual void OnHitEffect() { }
    protected virtual void OnDeathEffect() { }

    public void Die()
    {
        OnDeathEffect();
        Destroy(gameObject);
    }
}
