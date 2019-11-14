#pragma warning disable CS0649
/* 
 * Developed by Adam Brodin
 * https://github.com/AdamBrodin
 */

using UnityEngine;

public class Entity : Health
{
    #region Variables
    public EntityStats stats;
    private ParticleSystem ps;
    protected float moveSpeed;
    [SerializeField] private bool showBloodEffects;
    #endregion

    private void Awake()
    {
        if (showBloodEffects)
        {
            if (GetComponent<ParticleSystem>() != null) { ps = GetComponent<ParticleSystem>(); }
        }
    }

    protected override void OnHitEffect() { if (showBloodEffects) { ps?.Play(); } }
    protected virtual void Start() => StatsSetup();
    private void StatsSetup()
    {
        StartHealth = stats.startHealth;
        CurrentHealth = StartHealth;
        moveSpeed = stats.moveSpeed;
    }
}
