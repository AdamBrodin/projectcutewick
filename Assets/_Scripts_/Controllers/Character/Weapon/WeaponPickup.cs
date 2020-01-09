using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    private Image secondaryWeaponImage;

    private void Awake()
    {
        secondaryWeaponImage = GameObject.Find("ImgHolder").GetComponentInChildren<Image>();
    }

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
                AudioManager.Instance.SetState("Weapon_Pickup", true);
                weapons.Add(hit.collider.gameObject);

                hit.collider.gameObject.SetActive(false);
                hit.transform.SetParent(holder, false);
                hit.transform.localPosition = Vector3.forward;
                hit.transform.rotation = Quaternion.LookRotation(hit.transform.forward, transform.up);
                hit.transform.parent = this.transform;
                hit.collider.gameObject.GetComponent<BaseWeapon>().enabled = true;

                if (weapons.Count > 1)
                {

                }
            }
        }

        if (Input.GetKeyDown(dropKey) && currentWeapon != null)
        {
            currentWeapon.transform.parent = null;
            currentWeapon.transform.position = dropPoint.position;
            currentWeapon.gameObject.GetComponent<BaseWeapon>().enabled = false;

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

            if (weapons.Count > 1)
            {
                if (index == 0)
                {
                    secondaryWeaponImage.sprite = weapons[1].GetComponent<SpriteRenderer>().sprite;
                }
                else
                {
                    secondaryWeaponImage.sprite = weapons[0].GetComponent<SpriteRenderer>().sprite;
                }
            }

        }
    }
}
