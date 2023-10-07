using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CollideWithWords : MonoBehaviour
{
    private TextMeshProUGUI epitaphText;
    private string newWord;

    private void Start()
    {
        epitaphText = GameObject.Find("epitaphText").GetComponent<TextMeshProUGUI>();
        newWord = transform.Find("Words").GetComponent<TextMeshPro>().text;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            epitaphText.text = epitaphText.text + " " + newWord;
            Destroy(gameObject);
        }
    }


}
