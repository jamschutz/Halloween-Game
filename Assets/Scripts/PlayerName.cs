using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerName : MonoBehaviour
{
    private TextMeshProUGUI inputText;
    private TextMeshPro nameText;

    private void Start()
    {
        inputText = GameObject.Find("inputText").GetComponent<TextMeshProUGUI>();
        nameText = GameObject.Find("nameText").GetComponent<TextMeshPro>();
    }
    public void ChangeTheNameOnStone()
    {
        nameText.text = inputText.text;
    }


}
