using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempPlayerController : MonoBehaviour
{
    public float movingSpeed;
    public float jumpForce;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(0, jumpForce, 0);
        }


    }
}
