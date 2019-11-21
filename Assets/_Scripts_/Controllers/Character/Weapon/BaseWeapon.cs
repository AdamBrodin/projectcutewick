#pragma warning disable CS0649
using UnityEngine;
using TMPro;



public abstract class BaseWeapon : MonoBehaviour
{
    #region BaseVariables

    public Rigidbody2D bullet;
    public float fireForce = 20f;
    public float range_Spread;

    public TextMeshProUGUI ammo_Text;

    [SerializeField] public int ammo;
    [SerializeField] protected int internal_Ammo;

    [SerializeField] private bool accuracy;
    [SerializeField] private bool can_Reload; //under utveckling
    [SerializeField] public static bool is_Firing;

    #endregion

    #region Properties
    public bool semi_Auto = false;
    public bool full_Auto = false;
    public bool shotgun = false;
    public bool range = false;
    #endregion

    private PlayerMovement playerCtrl;

    void Awake()
    {
        playerCtrl = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    void FixedUpdate()
    {
        ammo_Text.text = internal_Ammo.ToString();

        if (internal_Ammo <= 1)
        {
            internal_Ammo = 0;
        }

        if (internal_Ammo >= 50)
        {
            internal_Ammo = 50;
        }
    }

    public void Fire(string sound)
    {
        is_Firing = true;

        internal_Ammo -= 1;

        if (accuracy == true)
        {
            range_Spread = Random.Range(1, -1);
        }

        if (internal_Ammo > 0)
        {
            AudioManager.Instance.SetState(sound, true);

            if (playerCtrl.facingRight == 1)
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
            }
        }

        else
        {
            AudioManager.Instance.SetState("Weapon_Fail", true);
        }
    }
}