using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public LegInput[] legs;
    public float raisedLegHeight;
    public float jumpForce;
    public float resetRotationTime = 0.5f;

    private Vector3 startingLegPosition;
    private Rigidbody rb;
    private Quaternion targetResetRotation;
    private float maxRotationDelta;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        targetResetRotation = new Quaternion();
        targetResetRotation.eulerAngles = Vector3.up;
        maxRotationDelta = 180 / resetRotationTime;

        foreach(var leg in legs) {
            leg.startPos = leg.legObj.transform.localPosition;
            leg.forcePoint = leg.legObj.transform.GetChild(0);
        }
    }


    private void Update()
    {
        if(Input.GetKey(KeyCode.Keypad0)) {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetResetRotation, maxRotationDelta * Time.deltaTime);

            if(Quaternion.Angle(transform.rotation, targetResetRotation) < 0.1f) {
                rb.angularVelocity = Vector3.zero;
            }
        }
        else {
            foreach(var leg in legs) {
                if(Input.GetKeyDown(GetLegKeycode(leg))) {
                    leg.legObj.transform.localPosition = leg.startPos + (Vector3.up * raisedLegHeight);
                }
                if(Input.GetKeyUp(GetLegKeycode(leg))) {
                    leg.legObj.transform.localPosition = leg.startPos;
                    rb.AddForceAtPosition(transform.up * jumpForce, leg.forcePoint.position, ForceMode.Impulse);
                }
            }
        }

        
    }



    private KeyCode GetLegKeycode(LegInput leg)
    {
        switch(leg.numKey) {
            case 0: return KeyCode.Keypad0;
            case 1: return KeyCode.Keypad1;
            case 2: return KeyCode.Keypad2;
            case 3: return KeyCode.Keypad3;
            case 4: return KeyCode.Keypad4;
            case 5: return KeyCode.Keypad5;
            case 6: return KeyCode.Keypad6;
            case 7: return KeyCode.Keypad7;
            case 8: return KeyCode.Keypad8;
            case 9: return KeyCode.Keypad9;
        }

        Debug.LogError($"bad number for leg input: {leg.numKey}");
        return KeyCode.Keypad0;
    }




    [System.Serializable]
    public class LegInput
    {
        public int numKey;
        public GameObject legObj;
        public Vector3 startPos;
        public Transform forcePoint;
    }
}
