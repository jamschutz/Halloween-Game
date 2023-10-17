using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_REAL : MonoBehaviour
{
    public float movingSpeed;
    public float jumpForce;
    public int numJumpsAllowed;
    public float turnForce;
    public Animator animator;

    private Rigidbody rb;
    private int jumpsUsed;
    private float angleX;
    private float angleY;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        jumpsUsed = 0;
        angleX = transform.eulerAngles.x;
        angleY = transform.eulerAngles.y;
    }
    void Update()
    {
        //animation
        if (Input.GetKey(KeyCode.W)|| Input.GetKey(KeyCode.A)|| Input.GetKey(KeyCode.S)|| Input.GetKey(KeyCode.D))
        {
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }



        if (Input.GetKey(KeyCode.W))
        {
            rb.AddRelativeForce(0, 0, movingSpeed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.AddRelativeForce(0, 0, -movingSpeed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddRelativeForce(movingSpeed, 0, 0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddRelativeForce(-movingSpeed, 0, 0);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(0, jumpForce, 0);
            animator.SetTrigger("jump");            
            jumpsUsed++;
        }


        // rotate left
        if(Input.GetKey(KeyCode.LeftArrow)) {
            angleY -= turnForce * Time.deltaTime;
        }        
        // rotate right
        if(Input.GetKey(KeyCode.RightArrow)) {
            angleY += turnForce * Time.deltaTime;
        }
        // // rotate up
        // if(Input.GetKey(KeyCode.UpArrow)) {
        //     angleX -= turnForce * Time.deltaTime;
        // }
        // // rotate down
        // if(Input.GetKey(KeyCode.DownArrow)) {
        //     angleX += turnForce * Time.deltaTime;
        // }
        transform.eulerAngles = new Vector3(angleX, angleY, transform.eulerAngles.z);

    }
}
