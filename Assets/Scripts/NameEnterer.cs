using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameEnterer : MonoBehaviour
{
    private void Start()
    {
        var inputField = GetComponent<InputField>();
        inputField.Select();
        inputField.ActivateInputField();
    }
}
