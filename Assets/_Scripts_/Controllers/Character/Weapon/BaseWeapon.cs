#pragma warning disable CS0649
using TMPro;
using UnityEngine;

public abstract class BaseWeapon : MonoBehaviour
{
    #region BaseVariables
    [SerializeField] private Rigidbody2D bullet;
    [SerializeField] private float range_Spread, fireForce = 20f, minAimAngle, maxAimAngle;
    [SerializeField] private TextMeshProUGUI ammo_Text;
    [SerializeField] private int ammo, internal_Ammo;
    [SerializeField] private bool accuracy, can_Reload;
    [SerializeField] public static bool is_Firing;
    [SerializeField] private GameObject weaponHolder;
    #endregion

    #region Properties
    [SerializeField] private bool semi_Auto, full_Auto, shotgun, range;
    #endregion

    private PlayerMovement playerCtrl;
    private void Awake() => playerCtrl = transform.root.GetComponent<PlayerMovement>();
    private void FixedUpdate()
    {
        // ammo_Text.text = internal_Ammo.ToString();
        if (internal_Ammo <= 1)
        {
            internal_Ammo = 0;
        }

        if (internal_Ammo >= 50)
        {
            internal_Ammo = 50;
        }
    }

    protected virtual void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);

        mousePos.x -= objectPos.x;
        mousePos.y -= objectPos.y;

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        weaponHolder.transform.rotation = Quaternion.Euler(0, 0, Mathf.Clamp(angle, minAimAngle, maxAimAngle));
    }

    public void Fire(string sound)
    {
        is_Firing = true;
        internal_Ammo -= 1;
        if (accuracy) { range_Spread = Random.Range(1, -1); }

        if (internal_Ammo > 0)
        {
            AudioManager.Instance.SetState(sound, true);
            Rigidbody2D bulletInstance = Instantiate(bullet, transform.position, transform.rotation);
            bulletInstance.velocity = new Vector2(fireForce, range_Spread);

            /* if (playerCtrl.facingRight == 1)
             {
                 // instantiate facing right and set it's velocity to the right.
                 Rigidbody2D bulletInstance = Instantiate(bullet, transform.position, Quaternion.Euler(new Vector3(0, 0, 0))) as Rigidbody2D;
                 bulletInstance.velocity = new Vector2(fireForce, range_Spread);
             }
             else
             {
                 // instantiate facing left and set it's velocity to the left.
                 Rigidbody2D bulletInstance = Instantiate(bullet, transform.position, Quaternion.Euler(new Vector3(0, 0, 180f))) as Rigidbody2D;
                 bulletInstance.velocity = new Vector2(-fireForce, range_Spread);
             }*/
        }
        else
        {
            AudioManager.Instance.SetState("Weapon_Fail", true);
        }
    }
}
