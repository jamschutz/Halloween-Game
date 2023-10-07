using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateLevel : MonoBehaviour
{
    public string[] words;
    public GameObject wordBrickPrefab;
    public float levelRangeX, levelRangeZ;
    public float groundScalingMax, groundScalingMin;
    private GameObject player;

    private void Start()
    {
        player = GameObject.Find("Player");

        foreach (string word in words)
        {
            GameObject wordBricks = Instantiate(wordBrickPrefab);
            wordBricks.GetComponent<CollideWithWords>().theWord = word;
            wordBricks.transform.position = new Vector3(
                Random.Range(player.transform.position.x+levelRangeX / 2, player.transform.position.x - levelRangeX / 2), 
                0.5f, 
                Random.Range(player.transform.position.z + levelRangeZ / 2, player.transform.position.z - levelRangeZ / 2));

            GameObject cylinder = wordBricks.transform.Find("Cylinder").gameObject;
            cylinder.transform.localScale = new Vector3(
                Random.Range(groundScalingMax, groundScalingMin), 1, Random.Range(groundScalingMax, groundScalingMin));

        }
    }
}
