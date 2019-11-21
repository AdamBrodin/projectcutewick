#pragma warning disable CS0649
using System;
using UnityEngine;

/* 
 * Developed by Adam Brodin
 * https://github.com/AdamBrodin
 */

public class AchievementSystem : MonoBehaviour
{
    #region Variables
    public Action<string> OnEntityKilled, OnPlayerDeath, OnWeaponFire;
    private int totalEnemyKills, totalDeaths, totalBulletsFired;
    #endregion


}
