using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_MOUSE2 : MonoBehaviour
{
    public float turnForce;
    public float hoverForce;

    private Rigidbody rb;
    private float angleX;
    private float angleY;
    private Vector2 lastMousePos;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        angleX = transform.eulerAngles.x;
        angleY = transform.eulerAngles.y;

        Cursor.visible = false;
    }


    private void Update()
    {
        angleX += turnForce * Input.GetAxis("Mouse Y") * Time.deltaTime;
        angleY += turnForce * Input.GetAxis("Mouse X") * Time.deltaTime;

        if(Input.GetMouseButton(0) || Input.GetMouseButton(1)) {
            float mouseDelta = Vector2.Distance(lastMousePos, Input.mousePosition);
            rb.AddForce(transform.up * hoverForce * mouseDelta * Time.deltaTime, ForceMode.Impulse);
        }
        else {
            transform.eulerAngles = new Vector3(angleX, angleY, transform.eulerAngles.z);
        }
        lastMousePos = Input.mousePosition;
    }
}
