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
    private GameObject player;

    private void Start()
    {
        player = GameObject.Find("Player");

        LoadWords();
        foreach (string word in words)
        {
            GameObject wordBricks = Instantiate(wordBrickPrefab);
            wordBricks.GetComponent<CollideWithWords>().theWord = word;
            wordBricks.transform.position = new Vector3(
                Random.Range(player.transform.position.x+levelRangeX / 2, player.transform.position.x - levelRangeX / 2),
                Random.Range(player.transform.position.y + levelRangeY / 2, player.transform.position.y - levelRangeY / 2),
                Random.Range(player.transform.position.z + levelRangeZ / 2, player.transform.position.z - levelRangeZ / 2));

            GameObject cylinder = wordBricks.transform.Find("Cylinder").gameObject;
            cylinder.transform.localScale = new Vector3(
                Random.Range(groundScalingMax, groundScalingMin), 1, Random.Range(groundScalingMax, groundScalingMin));

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
        var prepositions = GetWordsInFile(prepositionsFile);

        var wordsToUse = GetRandomWords(nouns, numNouns);
        wordsToUse.AddRange(GetRandomWords(verbs, numVerbs));
        wordsToUse.AddRange(GetRandomWords(adjectives, numAdjectives));
        wordsToUse.AddRange(GetRandomWords(prepositions, numPrepositions));

        words = wordsToUse.ToArray();
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