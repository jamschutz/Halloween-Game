using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CollideWithWords : MonoBehaviour
{
    public static bool addedAtLeastOneWord;
    public string theWord;

    private EpitaphManager epitaphManager;
    private GameObject words;
    private string newWords;
    private float typingSpeed =0.2f ;
    private bool added = false;

    private void Start()
    {
        epitaphManager = GameObject.Find("epitaphText").GetComponent<EpitaphManager>();
        words = transform.Find("Words").gameObject;
        words.GetComponent<TextMeshPro>().text = theWord;
        addedAtLeastOneWord = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && !added)
        {
            if (!addedAtLeastOneWord)
            {
                newWords = " " + theWord;
                addedAtLeastOneWord = true;
            }
            else
            {
                newWords = " " + theWord;
            }
            epitaphManager.AddNewWords(newWords);
            added = true;
            words.SetActive(false);
        }
    }


}
