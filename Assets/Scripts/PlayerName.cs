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
    public TextMeshProUGUI timerText;

    private GenerateLevel levelGenerator;
    private EpitaphManager epitaphManager;
    private PlayerController_MOUSE tempPlayerController;
    private Camera secondCamera;
    private bool gameStart;
    private void Start()
    {
        levelGenerator = GetComponent<GenerateLevel>();
        epitaphManager = GameObject.Find("epitaphText").GetComponent<EpitaphManager>();
        tempPlayerController = GameObject.FindWithTag("Player").GetComponent<PlayerController_MOUSE>();
        secondCamera = GameObject.Find("secondCamera").GetComponentInParent<Camera>();
        
    }

    private void Update()
    {
        if(gameStart) secondsYouCanPlay -= Time.deltaTime;

        timerText.text = "TIME BEFORE GO TO HEAVEN: " + (int)secondsYouCanPlay;

        if (secondsYouCanPlay < 0)
        {
            epitaphManager.EndGame();
            timerText.gameObject.SetActive(false);
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
        timerText.gameObject.SetActive(true);
        gameStart = true;
    }


}
