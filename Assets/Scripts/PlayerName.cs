using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerName : MonoBehaviour
{
    public TextMeshProUGUI inputText;
    public TextMeshPro nameText;
    public Camera mainCamera;
    public float secondsYouCanPlay;
    public float timer;

    private GenerateLevel levelGenerator;
    private EpitaphManager epitaphManager;
    private TempPlayerController tempPlayerController;
    private Camera secondCamera;
    private bool gameStart;
    private void Start()
    {
        levelGenerator = GetComponent<GenerateLevel>();
        epitaphManager = GameObject.Find("epitaphText").GetComponent<EpitaphManager>();
        tempPlayerController = GameObject.FindWithTag("Player").GetComponent<TempPlayerController>();
        secondCamera = GameObject.Find("secondCamera").GetComponentInParent<Camera>();
        timer = 0;
    }

    private void Update()
    {
        if(gameStart)timer += Time.deltaTime;


        if(timer > secondsYouCanPlay)
        {
            epitaphManager.EndGame();
        }

    }

    public void ChangeTheNameOnStone()
    {
        nameText.text = inputText.text;
    }

    public void StartGame()
    {
        Invoke("StartTheGame", 3f);
    }

    private void StartTheGame()
    {
        levelGenerator.enabled = true;
        epitaphManager.ShowLine01();
        tempPlayerController.enabled = true;
        mainCamera.gameObject.SetActive(true);
        secondCamera.gameObject.SetActive(false);
        gameStart = true;
    }


}
