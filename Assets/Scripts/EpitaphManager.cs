using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EpitaphManager : MonoBehaviour
{
    public string[] epitaphTemplate;
    public float typingSpeed;
    public string currentEpitaph01;
    public string currentEpitaph02;
    public string currentEpitaph03;
    public Coroutine displayWordsCorountine;
    public int howManyTemplate;
    public string underLine;
    public string theDate;
    public GameObject howToPlay;
    public GameObject timerText;
    public GameObject replayButton;

    private int[] epitaphTemplateIndex;
    private int howManywordsYouGot;
    private TextMeshProUGUI epitaphText01;
    private TextMeshProUGUI epitaphText02;
    private TextMeshProUGUI epitaphText03;
    private Coroutine displayWordCorountine;
    private bool end;
    private int rand;
    private GameObject player;
    private Camera secondCamera;
    private TextMeshPro epitaphOnStone;
    private TextMeshPro date;
    private DateTime earliestDate = new DateTime(1887, 1, 1);
    
    private void Start()
    {
        epitaphText01 = GetComponent<TextMeshProUGUI>();
        epitaphText02 = transform.Find("epitaphText02").GetComponent<TextMeshProUGUI>();
        epitaphText03 = transform.Find("epitaphText03").GetComponent<TextMeshProUGUI>();
        player = GameObject.FindWithTag("Player");
        secondCamera = GameObject.Find("secondCamera").GetComponentInParent<Camera>();
        date = GameObject.Find("date").GetComponent<TextMeshPro>();
        epitaphOnStone = GameObject.Find("epitaphOnStone").GetComponent<TextMeshPro>();

        //pick three
        epitaphTemplateIndex = new int[howManyTemplate];

        for (int i = 0; i < howManyTemplate; i++)
        {
            epitaphTemplateIndex[i] = howManyTemplate+1;
        }

        for (int j = 0; j < howManyTemplate; j++)
        {
            rand = UnityEngine.Random.Range(0, howManyTemplate);
            while (epitaphTemplateIndex.Contains(rand))
            {
                rand = UnityEngine.Random.Range(0, howManyTemplate);
            }

            epitaphTemplateIndex[j]= rand;

        }

        currentEpitaph01 = epitaphTemplate[epitaphTemplateIndex[0]];
        currentEpitaph02 = epitaphTemplate[epitaphTemplateIndex[1]];
        currentEpitaph03 = epitaphTemplate[epitaphTemplateIndex[2]];


        
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
            epitaphText03.text = currentEpitaph03;

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
            epitaphText02.text = currentEpitaph02;

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
            epitaphText01.text = currentEpitaph01;

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

        if(howManywordsYouGot==1) epitaphText01.text = epitaphText01.text + underLine + underLine;
        if (howManywordsYouGot == 2) epitaphText01.text = epitaphText01.text + underLine;
        if(howManywordsYouGot==4) epitaphText02.text = epitaphText02.text + underLine + underLine;
        if (howManywordsYouGot == 5) epitaphText02.text = epitaphText02.text + underLine;
        if(howManywordsYouGot==7) epitaphText03.text = epitaphText03.text + underLine + underLine;
        if (howManywordsYouGot == 8) epitaphText03.text = epitaphText03.text + underLine;



    }

    public void ShowLine01()
    {
        epitaphText01.text = currentEpitaph01 + underLine + underLine + underLine;
    }

    private void ShowLine02()
    {
        epitaphText02.text = currentEpitaph02 + underLine + underLine + underLine;
    }

    private void ShowLine03()
    {
        epitaphText03.text = currentEpitaph03 + underLine + underLine + underLine;
    }

    public void EndGame()
    {
        if(end) return;
        
        timerText.SetActive(false);
        howToPlay.SetActive(false);
        player.SetActive(false);
        secondCamera.gameObject.SetActive(true);
        replayButton.SetActive(true);
        // date.text = theDate;
        int month = UnityEngine.Random.Range(1, 12);
        int day = UnityEngine.Random.Range(1, 28);
        int year = UnityEngine.Random.Range(1887, 2003);
        date.text = $"{GetMonth(month)} {day}, {year} - October 21, 2023";

        if (howManywordsYouGot == 0)
        {
            epitaphOnStone.text = currentEpitaph01 +  " NOTHING\n" + currentEpitaph02 + " NOTHING\n" + currentEpitaph03 + " NOTHING";
        }
        else if (howManywordsYouGot <= 3)
        {
            epitaphOnStone.text = currentEpitaph01 + "\n" + currentEpitaph02 + " NOTHING\n" + currentEpitaph03 + " NOTHING";
        }
        else if(howManywordsYouGot <= 6)
        {
            epitaphOnStone.text = currentEpitaph01 + "\n" + currentEpitaph02 + "\n" + currentEpitaph03 + " NOTHING";
        }
        else
        {
            epitaphOnStone.text = currentEpitaph01 + "\n" + currentEpitaph02 + "\n" + currentEpitaph03;
        }
        end = true;
        gameObject.SetActive(false);
    }


    public void ReplayGame()
    {
        SceneManager.LoadScene(0);
    }


    private string GetMonth(int m)
    {
        switch(m) {
            case 1: return "January";
            case 2: return "February";
            case 3: return "March";
            case 4: return "April";
            case 5: return "May";
            case 6: return "June";
            case 7: return "July";
            case 8: return "August";
            case 9: return "September";
            case 10: return "October";
            case 11: return "November";
            case 12: return "December";
        }

        return "January";
    }

}
