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
    public string currentEpitaph;
    public Coroutine displayWordsCorountine;
    public string underLine;
    public string theDate;
    public GameObject howToPlay;
    public GameObject timerText;
    public GameObject replayButton;

    private int[] epitaphTemplateIndex;
    private int howManywordsYouGot;
    private int howManyEpitaphsYouGot;
    private TextMeshProUGUI epitaphText;
    private Coroutine displayWordCorountine;
    private bool end;
    private int rand;
    public int numWordsOnLine;
    private GameObject player;
    private Camera secondCamera;
    private TextMeshPro epitaphOnStone;
    private TextMeshPro date;
    private DateTime earliestDate = new DateTime(1887, 1, 1);
    private AudioSource typingSound;

    private void Start()
    {
        epitaphText = GetComponent<TextMeshProUGUI>();
        player = GameObject.FindWithTag("Player");
        secondCamera = GameObject.Find("secondCamera").GetComponentInParent<Camera>();
        date = GameObject.Find("date").GetComponent<TextMeshPro>();
        epitaphOnStone = GameObject.Find("epitaphOnStone").GetComponent<TextMeshPro>();
        typingSound = GameObject.Find("typingSound").GetComponent<AudioSource>();

        //randomize
        epitaphTemplateIndex = new int[epitaphTemplate.Length];
        numWordsOnLine = 0;

        for (int i = 0; i < epitaphTemplate.Length; i++)
        {
            epitaphTemplateIndex[i] = epitaphTemplate.Length + 1;
        }

        for (int j = 0; j < epitaphTemplate.Length; j++)
        {
            rand = UnityEngine.Random.Range(0, epitaphTemplate.Length);
            while (epitaphTemplateIndex.Contains(rand))
            {
                rand = UnityEngine.Random.Range(0, epitaphTemplate.Length);
            }

            epitaphTemplateIndex[j]= rand;

        }

        currentEpitaph = epitaphTemplate[epitaphTemplateIndex[0]];
        
        howManywordsYouGot = 0;
        howManyEpitaphsYouGot= 0;
        end = false;
    }

    public void AddNewWords(string newWords,bool isNoun)
    {
        if (end) return;

        howManywordsYouGot++;
        numWordsOnLine++;

        epitaphText.text = currentEpitaph;

        if (isNoun)
        {
            howManyEpitaphsYouGot++;

            if (displayWordCorountine != null)
            {
                StopCoroutine(displayWordCorountine);
                epitaphText.text = currentEpitaph;
            }

            string newLine;
            newLine = newWords + ".\n" + epitaphTemplate[howManyEpitaphsYouGot];
            currentEpitaph += newLine;
            displayWordCorountine = StartCoroutine(TypeOutNewWords(newLine+ underLine));
            numWordsOnLine = 0;
        }
        else
        {
            if (displayWordCorountine != null)
            {
                StopCoroutine(displayWordCorountine);
                epitaphText.text = currentEpitaph;
            }

            currentEpitaph += newWords;
            displayWordCorountine = StartCoroutine(TypeOutNewWords(newWords+ underLine));

            
        }
        
    }

    public IEnumerator TypeOutNewWords(string line)
    {
        foreach (char c in line.ToCharArray())
        {
            epitaphText.text += c;
            typingSound.Play();
            yield return new WaitForSeconds(typingSpeed);
        }

        
    }

    public void ShowLineWithUnderline()
    {
        epitaphText.text = currentEpitaph + underLine;
    }


    public void EndGame()
    {
        if(end) return;
        if(numWordsOnLine == 0) {
            currentEpitaph += " NOTHING";
        }
        epitaphOnStone.text = currentEpitaph;
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
