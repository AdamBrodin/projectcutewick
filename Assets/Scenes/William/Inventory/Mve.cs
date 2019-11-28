using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mve : MonoBehaviour
{

    Vector2 playermove;
    Rigidbody2D  rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        playermove = new Vector2( Input.GetAxisRaw("Horzintal"), Input.GetButtonDown("Jump") ? 1 : 0);

        rb.AddForce(playermove);

    }
}
