using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateLevel : MonoBehaviour
{
    public string[] words;
    public TextAsset nounsFile;
    public TextAsset verbsFile;
    public TextAsset adjectivesFile;
    public TextAsset prepositionsFile;
    public int numNouns;
    public int numVerbs;
    public int numAdjectives;
    public int numPrepositions;
    public GameObject wordBrickPrefab;
    public float levelRangeX, levelRangeY, levelRangeZ;
    public float groundScalingMax, groundScalingMin;
    public float cylinderScalingMax, cylinderScalingMin;
    public Material[] mat;
    [Header("clouds")]
    public int cloudAmounts;
    public GameObject[] cloudsPrefabs;

    private int matIndex;
    private GameObject player;
    private float wholePlatformScaling;
    private float cylinderScaling;
    private int wordCount;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        wordCount = 0;
        LoadWords();
        foreach (string word in words)
        {
            wordCount++;

            GameObject wordBricks = Instantiate(wordBrickPrefab);
            wordBricks.GetComponent<CollideWithWords>().theWord = word;
            wordBricks.transform.position = new Vector3(
                Random.Range(player.transform.position.x+levelRangeX / 2, player.transform.position.x - levelRangeX / 2),
                Random.Range(player.transform.position.y, player.transform.position.y - levelRangeY),
                Random.Range(player.transform.position.z + levelRangeZ / 2, player.transform.position.z - levelRangeZ / 2));

            wholePlatformScaling = Random.Range(groundScalingMax, groundScalingMin);
            wordBricks.transform.localScale = new Vector3(wholePlatformScaling, wholePlatformScaling, wholePlatformScaling);

            if(wordCount< numNouns)
            {
                wordBricks.GetComponent<CollideWithWords>().isNoun = true;
            }

            GameObject cylinder = wordBricks.transform.Find("Cylinder").gameObject;
            matIndex = Random.Range(0, mat.Length);
            cylinder.GetComponent<MeshRenderer>().material = mat[matIndex];
            cylinderScaling =  Random.Range(cylinderScalingMax, cylinderScalingMin);
            cylinder.transform.localScale = new Vector3(
                cylinderScaling, 1, cylinderScaling);

        }

        for (int i = 0; i < cloudAmounts; i++)
        {
            if(i%2 == 0)
            {
                GameObject clouds = Instantiate(cloudsPrefabs[0]);
                clouds.transform.position = new Vector3(
                Random.Range(player.transform.position.x + levelRangeX*3 , player.transform.position.x - levelRangeX * 3),
                Random.Range(player.transform.position.y, player.transform.position.y - levelRangeY),
                Random.Range(player.transform.position.z + levelRangeZ * 3, player.transform.position.z - levelRangeZ * 3));
            }
            else
            {
                GameObject clouds = Instantiate(cloudsPrefabs[1]);
                clouds.transform.position = new Vector3(
                Random.Range(player.transform.position.x + levelRangeX * 3, player.transform.position.x - levelRangeX * 3),
                Random.Range(player.transform.position.y, player.transform.position.y - levelRangeY),
                Random.Range(player.transform.position.z + levelRangeZ * 3, player.transform.position.z - levelRangeZ * 3));
            }
            


        }


    }


    // private void LoadWords()
    // {
    //     const int BufferSize = 128;
    //     using (var fileStream = File.OpenRead(fileName)) {
    //         using (var streamReader = new StreamReader(fileStream, System.Text.Encoding.UTF8, true, BufferSize)) {
    //             string line;
    //             while ((line = streamReader.ReadLine()) != null)
    //             {
    //                 // Process line
    //             }
    //         }
    //     }
    // }


    private void LoadWords()
    {
        var nouns = GetWordsInFile(nounsFile);
        var verbs = GetWordsInFile(verbsFile);
        var adjectives = GetWordsInFile(adjectivesFile);
        // var prepositions = GetWordsInFile(prepositionsFile);

        var wordsToUse = GetRandomWords(nouns, numNouns);
        wordsToUse.AddRange(GetRandomWords(verbs, numVerbs));
        wordsToUse.AddRange(GetRandomWords(adjectives, numAdjectives));
        // wordsToUse.AddRange(GetRandomWords(prepositions, numPrepositions));

        words = wordsToUse.ToArray();
        foreach(var word in words) {
            Debug.Log($"got word: {word}");
        }
    }


    private List<string> GetWordsInFile(TextAsset file)
    {
        var allWords = new List<string>();
        foreach(var line in file.text.Split("\n")) {
            if(string.IsNullOrEmpty(line))
                continue;

            allWords.Add(line.Trim().ToUpper());
        }
        return allWords;
    }


    private List<string> GetRandomWords(List<string> allWords, int numWords)
    {
        var randomWords = new List<string>();
        for(int i = 0; i < numWords; i++) {
            int randomIndex = Random.Range(0, allWords.Count);
            randomWords.Add(allWords[randomIndex]);
        }
        return randomWords;
    }
}
