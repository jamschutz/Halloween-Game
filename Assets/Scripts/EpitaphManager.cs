using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using TMPro;
using UnityEngine;

public class EpitaphManager : MonoBehaviour
{
    public string[] epitaphTemplate;
    public float typingSpeed;
    public string currentEpitaph01;
    public string currentEpitaph02;
    public string currentEpitaph03;
    public Coroutine displayWordsCorountine;
    public int howManyTemplate;

    private int[] epitaphTemplateIndex;
    private int wordsForThisEpitaph;
    private TextMeshProUGUI epitaphText;
    private Coroutine displayWordCorountine;

    private void Start()
    {
        epitaphText = GetComponent<TextMeshProUGUI>();

        //pick three
        epitaphTemplateIndex= new int[howManyTemplate];

        for (int i = 0; i < howManyTemplate; i++)
        {
            epitaphTemplateIndex[i] = howManyTemplate+1;
        }

        for (int j = 0; j < howManyTemplate; j++)
        {
            epitaphTemplateIndex[j] = Random.Range(0, howManyTemplate);
            while (epitaphTemplateIndex.Contains(epitaphTemplateIndex[j]))
            {
                epitaphTemplateIndex[j] = Random.Range(0, howManyTemplate);
            }
        }

        currentEpitaph01 = epitaphTemplate[epitaphTemplateIndex[0]];
        currentEpitaph02 = epitaphTemplate[epitaphTemplateIndex[1]];
        currentEpitaph03 = epitaphTemplate[epitaphTemplateIndex[2]];


        epitaphText.text = currentEpitaph01;
        wordsForThisEpitaph = 0;
    }

    public void AddNewWords(string newWords)
    {
        wordsForThisEpitaph++;
        if (wordsForThisEpitaph >= 3)
        {
            wordsForThisEpitaph = 0;

        }

        if (displayWordCorountine != null)
        {
            StopCoroutine(displayWordCorountine);
            epitaphText.text = currentEpitaph01;
        }

        currentEpitaph01 += newWords;
        displayWordCorountine = StartCoroutine(TypeOutNewWords(newWords));
        
    }

    public IEnumerator TypeOutNewWords(string line)
    {
        foreach (char c in line.ToCharArray())
        {
            epitaphText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

}
