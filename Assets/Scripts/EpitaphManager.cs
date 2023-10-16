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
    private int howManywordsYouGot;
    private TextMeshProUGUI epitaphText01;
    private TextMeshProUGUI epitaphText02;
    private TextMeshProUGUI epitaphText03;
    private Coroutine displayWordCorountine;
    private bool end;
    private int rand;
    private void Start()
    {
        epitaphText01 = GetComponent<TextMeshProUGUI>();
        epitaphText02 = transform.Find("epitaphText02").GetComponent<TextMeshProUGUI>();
        epitaphText03 = transform.Find("epitaphText03").GetComponent<TextMeshProUGUI>();

        //pick three
        epitaphTemplateIndex= new int[howManyTemplate];

        for (int i = 0; i < howManyTemplate; i++)
        {
            epitaphTemplateIndex[i] = howManyTemplate+1;
        }

        for (int j = 0; j < howManyTemplate; j++)
        {
            rand = Random.Range(0, howManyTemplate);
            while (epitaphTemplateIndex.Contains(rand))
            {
                rand = Random.Range(0, howManyTemplate);
            }

            epitaphTemplateIndex[j]= rand;

        }

        currentEpitaph01 = epitaphTemplate[epitaphTemplateIndex[0]];
        currentEpitaph02 = epitaphTemplate[epitaphTemplateIndex[1]];
        currentEpitaph03 = epitaphTemplate[epitaphTemplateIndex[2]];


        epitaphText01.text = currentEpitaph01;
        howManywordsYouGot = 0;
        end = false;
    }

    public void AddNewWords(string newWords)
    {
        if (end) return;

        howManywordsYouGot++;
        if (howManywordsYouGot >= 9)
        {
            currentEpitaph03 += newWords;
            epitaphText03.text = currentEpitaph03;
            EndGame();
            return;
        }

        if (howManywordsYouGot == 3)
        {
            Invoke("ShowLine02",1f);
        }
        if(howManywordsYouGot == 6)
        {
            Invoke("ShowLine03", 1f);
        }


        if (howManywordsYouGot>=7)
        {
            if (displayWordCorountine != null)
            {
                StopCoroutine(displayWordCorountine);
                epitaphText03.text = currentEpitaph03;
            }

            currentEpitaph03 += newWords;
            displayWordCorountine = StartCoroutine(TypeOutNewWords(newWords));
        }
        else if (howManywordsYouGot<7 && howManywordsYouGot>=4)
        {
            if (displayWordCorountine != null)
            {
                StopCoroutine(displayWordCorountine);
                epitaphText02.text = currentEpitaph02;
            }

            currentEpitaph02 += newWords;
            displayWordCorountine = StartCoroutine(TypeOutNewWords(newWords));
        }
        else
        {
            if (displayWordCorountine != null)
            {
                StopCoroutine(displayWordCorountine);
                epitaphText01.text = currentEpitaph01;
            }

            currentEpitaph01 += newWords;
            displayWordCorountine = StartCoroutine(TypeOutNewWords(newWords));
        }



    }

    public IEnumerator TypeOutNewWords(string line)
    {
        foreach (char c in line.ToCharArray())
        {
            if (howManywordsYouGot >= 7)
            {
                epitaphText03.text += c;
            }
            else if(howManywordsYouGot<7 && howManywordsYouGot >= 4)
            {
                epitaphText02.text += c;
            }
            else
            {
                epitaphText01.text += c;
            }


            
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    private void ShowLine02()
    {
        epitaphText02.text = currentEpitaph02;
    }

    private void ShowLine03()
    {
        epitaphText03.text = currentEpitaph03;
    }

    private void EndGame()
    {
        Debug.Log("END");
        end = true;
    }

}
