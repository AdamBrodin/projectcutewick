using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

/* - Bogdan Chernikov - SU17A -  2019-2020© -*/

public class PlayerMovement : MonoBehaviour
{
    #region Variables
    public Transform groundCheck;
    public LayerMask groundLayer;
    private Rigidbody2D rb2d;

    public bool isGrounded = false;

    private bool playerDir = true;
    public float facingRight = 1;

    private Vector3 m_Velocity = Vector3.zero;

    [Range(0, .3f)] [SerializeField] private float smoothing = .05f;

    public float playerAxis = 0f;
    public float jumpForce;

    #endregion

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        playerAxis = Input.GetAxisRaw("Horizontal");
    }

    void FixedUpdate()
    {
        Move(playerAxis);

        if (BaseWeapon.is_Firing == true)
        {
           // rb2d.velocity *= -1;
        }

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.15f, groundLayer);

        if (Input.GetButtonDown("Jump") && isGrounded == true)
        {
            rb2d.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
    }

    public void Move(float move)
    {
        //if (isGrounded == true)
        {
            Vector3 targetVelocity = new Vector2(move * 10f, rb2d.velocity.y);
            rb2d.velocity = Vector3.SmoothDamp(rb2d.velocity, targetVelocity, ref m_Velocity, smoothing);

            if (move > 0 && !playerDir)
            {
                Flip();
                facingRight = 1;
            }

            else if (move < 0 && playerDir)
            {
                Flip();

                facingRight = 0;
            }
        }
    }

    private void Flip()
    {
        playerDir = !playerDir;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
