using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CrateBehavior : Destroyable
{
    #region Variables
    [SerializeField] private float weaponSpawnChance;
    [SerializeField] private GameObject[] weapons;
    #endregion

    protected override void DestroySelf()
    {
        float random = UnityEngine.Random.Range(0, 100);
        if (weaponSpawnChance >= random)
        {
            SpawnWeapon();
        }

        Destroy(gameObject);
    }

    private void SpawnWeapon()
    {
        GameObject weaponToSpawn = null;
        while (weaponToSpawn == null)
        {
            foreach (GameObject g in weapons)
            {
                float spawnChance = g.GetComponent<BaseWeapon>().spawnChance;
                float random = UnityEngine.Random.Range(0, 100);

                if (spawnChance >= random)
                {
                    weaponToSpawn = g;
                }
            }
        }

        if (weaponToSpawn != null) Instantiate(weaponToSpawn, transform.position, Quaternion.identity);
    }
}
