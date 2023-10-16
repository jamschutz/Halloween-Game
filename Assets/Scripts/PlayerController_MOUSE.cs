using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_MOUSE : MonoBehaviour
{
    public float turnForce;
    public float hoverForce;

    private Rigidbody rb;
    private float angleX;
    private float angleY;

    private string allKeys = "`1234567890-=qwertyuiop[]\\asdfghjkl;'zxcvbnm,./";

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
        Debug.Log($"got mouse x: {Input.GetAxis("Mouse X")}");
        Debug.Log($"got mouse y: {Input.GetAxis("Mouse Y")}");
        angleX += turnForce * Input.GetAxis("Mouse Y") * Time.deltaTime;
        angleY += turnForce * Input.GetAxis("Mouse X") * Time.deltaTime;
        transform.eulerAngles = new Vector3(angleX, angleY, transform.eulerAngles.z);

        foreach(var key in allKeys) {
            // Debug.Log("getting key: " + key);
            if(Input.GetKeyDown(key.ToString())) {
                rb.AddForce(transform.up * hoverForce, ForceMode.Impulse);
            }
        }
    }
}
