using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController_NEW : MonoBehaviour
{
    public float turnForce;
    public float hoverForce;

    private Rigidbody rb;
    private float angleX;
    private float angleY;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        angleX = transform.eulerAngles.x;
        angleY = transform.eulerAngles.y;
    }


    private void Update()
    {
        // rotate left
        if(Input.GetKey(KeyCode.LeftControl)) {
            angleY -= turnForce * Time.deltaTime;
        }        
        // rotate right
        if(Input.GetKey(KeyCode.RightControl)) {
            angleY += turnForce * Time.deltaTime;
        }
        // rotate up
        if(Input.GetKey(KeyCode.CapsLock)) {
            angleX -= turnForce * Time.deltaTime;
        }
        // rotate down
        if(Input.GetKey(KeyCode.Return)) {
            angleX += turnForce * Time.deltaTime;
        }
        transform.eulerAngles = new Vector3(angleX, angleY, transform.eulerAngles.z);

        // D & L to add force
        if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.L)) {
            rb.AddForce(transform.up * hoverForce, ForceMode.Impulse);
        }
    }
}
