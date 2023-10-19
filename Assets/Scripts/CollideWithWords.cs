using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CollideWithWords : MonoBehaviour
{
    public string theWord;
    public bool isNoun;

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
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && !added)
        {
            if (isNoun)
            {
                newWords = " " + theWord;
            }
            else
            {
                newWords = " " + theWord;
            }
            epitaphManager.AddNewWords(newWords,isNoun);
            added = true;
            words.SetActive(false);
        }
    }


}
