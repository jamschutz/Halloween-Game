using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateLevel : MonoBehaviour
{
    public string[] words;
    public GameObject wordBrickPrefab;

    private void Start()
    {
        foreach (string word in words)
        {
            GameObject wordBricks = Instantiate(wordBrickPrefab);
            wordBricks.GetComponent<CollideWithWords>().theWord = word;
        }
    }


}
