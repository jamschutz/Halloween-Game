using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventOnKeyPress : MonoBehaviour
{
    public KeyPressOptions key;
    public UnityEvent eventOnKey;


    private void Update()
    {
        if(Input.GetKeyDown(GetKeyCode(key))) {
            eventOnKey.Invoke();
        }
    }


    private KeyCode GetKeyCode(KeyPressOptions k)
    {
        switch(k) {
            case KeyPressOptions.Space:
                return KeyCode.Space;
            case KeyPressOptions.R:
                return KeyCode.R;
            case KeyPressOptions.Enter:
                return KeyCode.Return;
            default:
                return KeyCode.Space;
        }
    }






    public enum KeyPressOptions { Space, R, Enter }
}
