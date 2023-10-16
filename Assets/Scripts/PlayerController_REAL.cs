using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_REAL : MonoBehaviour
{
    public float movingSpeed;
    public float jumpForce;
    public int numJumpsAllowed;

    private Rigidbody rb;
    private int jumpsUsed;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        jumpsUsed = 0;
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(0, 0, movingSpeed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(0, 0, -movingSpeed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(movingSpeed, 0, 0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(-movingSpeed, 0, 0);
        }

        if (Input.GetKeyDown(KeyCode.Space) && jumpsUsed < numJumpsAllowed)
        {
            rb.AddForce(0, jumpForce, 0);
            jumpsUsed++;
        }


    }
}
