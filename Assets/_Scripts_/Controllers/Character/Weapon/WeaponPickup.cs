using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public KeyCode pickupKey = KeyCode.F;
    public KeyCode dropKey = KeyCode.G;
    string weaponTag = "Weapon";

    public List<GameObject> weapons;
    public int maxWeapons = 2;

    public GameObject currentWeapon;

    public Transform holder;
    public Transform rayPoint;
    public Transform dropPoint;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SelectWeapon(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SelectWeapon(1);
        }


        RaycastHit2D hit = Physics2D.Raycast(rayPoint.position, Vector2.right);
        Debug.DrawRay(rayPoint.position, Vector2.right, Color.magenta);

        if (hit.collider != null)
        {
            if (hit.transform.CompareTag(weaponTag) && Input.GetKeyDown(pickupKey) && weapons.Count < maxWeapons)
            {
                weapons.Add(hit.collider.gameObject);

                hit.collider.gameObject.SetActive(false);
                hit.transform.SetParent(holder);
                hit.transform.transform.localRotation = Quaternion.identity;
                hit.transform.localPosition = new Vector3(0, 0, 0);
                hit.collider.gameObject.GetComponent<BaseWeapon>().enabled = true;
            }
        }

        if (Input.GetKeyDown(dropKey) && currentWeapon != null)
        {
            currentWeapon.transform.parent = null;
            currentWeapon.transform.position = dropPoint.position;

            var weaponInstanceId = currentWeapon.GetInstanceID();
            for (int i = 0; i < weapons.Count; i++)
            {
                if (weapons[i].GetInstanceID() == weaponInstanceId)
                {
                    weapons.RemoveAt(i);
                    break;
                }
            }

            currentWeapon = null;
        }
    }

    void SelectWeapon(int index)
    {
        if (weapons.Count > index && weapons[index] != null)
        {
            if (currentWeapon != null)
            {
                currentWeapon.gameObject.SetActive(false);
            }
            currentWeapon = weapons[index];
            currentWeapon.SetActive(true);
        }
    }
}
