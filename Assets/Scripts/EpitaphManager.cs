using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class EpitaphManager : MonoBehaviour
{
    public float typingSpeed;
    public string currentEpitaph;
    public Coroutine displayWordsCorountine;

    private TextMeshProUGUI epitaphText;
    private Coroutine displayWordCorountine;

    private void Start()
    {
        epitaphText = GetComponent<TextMeshProUGUI>();
        epitaphText.text = currentEpitaph;
    }

    public void AddNewWords(string newWords)
    {
        if (displayWordCorountine != null)
        {
            StopCoroutine(displayWordCorountine);
            epitaphText.text = currentEpitaph;
        }

        currentEpitaph += newWords;
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
