using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI score;
    public GameObject upgradeUI;
    public UpgradeSystem upgradeSystem; 
    public GameObject endScreen;
    public GameObject titleScreen; 
    public GameObject tutorialScreen;
    public TextMeshProUGUI endScreenScore; 
    public TextMeshProUGUI endScreenHighScore;
    public TextMeshProUGUI percentInfected;
    public TextMeshProUGUI menuHighScore;
    public TextMeshProUGUI timerText;
    float startTime = -1;
    public GameObject waveCounter;
    public TextMeshProUGUI waveCounterText;
    public float waveCounterTimer = 3f;
    private float cachedTimer; 
    private bool waveCounterVisible;

    int waveNumber;

    CanvasGroup uIUpgradeCanvas;


    private void Start()
    {
        cachedTimer = waveCounterTimer;
        uIUpgradeCanvas = upgradeUI.GetComponent<CanvasGroup>();
    }

    private void Update()
    {
        if (waveCounterVisible)
        {
            if (cachedTimer < 0)
            {
                ShowWaveTimer(false);
                cachedTimer = waveCounterTimer;
            }

            cachedTimer -= Time.deltaTime; 
        }

        /*
        if (GameManager.Instance.gameHasStarted)
        {
            if (startTime == -1)
            {
                startTime = Time.timeSinceLevelLoad;
            }

            float timeText;
            timeText = Time.timeSinceLevelLoad-startTime;
            timerText.SetText(timeText.ToString("F2"));
        }else
        {
            if (startTime != -1)
            {
                startTime = -1;
                timerText.SetText("");
            }

        }*/

        //timerText.enabled = !waveCounterVisible;
    }

    public void ShowWaveTimer(bool visibility)
    {   
        if (visibility)
        {
            waveNumber++;
            waveCounterText.SetText("Wave "+waveNumber.ToString());
        }

        waveCounter.SetActive(visibility);
        waveCounterVisible = visibility;
    }

    public void ShowUpgradeUI(bool visibility)
    {
        StartCoroutine(ShowUpgradeUI_Coroutine(visibility));
        //upgradeUI.SetActive(visibility);
        //upgradeSystem.RefreshUpgrades();
    }

    IEnumerator ShowUpgradeUI_Coroutine(bool visibility)
    {
        if (visibility)
            upgradeSystem.RefreshUpgrades();

        float endVal = visibility ? 1 : 0;
        uIUpgradeCanvas.alpha = 1-endVal;

        yield return pTween.To(.3f, 1-endVal,endVal, t =>
        {
            uIUpgradeCanvas.alpha = t;
        });

        upgradeUI.SetActive(visibility);
    }
    
    public void ShowEndScreen(bool visibility)
    {
        endScreen.SetActive(visibility);
        endScreenScore.SetText(GameManager.Instance.scoreManager.currentScore.ToString());
        GameManager.Instance.scoreManager.CheckScore();
        endScreenHighScore.SetText(GameManager.Instance.scoreManager.GetHighScore().ToString());
    }

    public void ShowTitleScreen(bool visibility)
    {
        menuHighScore.SetText(GameManager.Instance.scoreManager.GetHighScore().ToString());
        titleScreen.SetActive(visibility);
    }

    public void OnPlayPressed()
    {
        titleScreen.SetActive(false);
        GameManager.Instance.PlaceInfected();
        
    }
    public void ReloadGame()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name); 
    }

    public void OnTutorialPressed()
    {
        titleScreen.SetActive(false);
        tutorialScreen.SetActive(true);
        score.enabled = false;
        percentInfected.enabled = false;

    }

}
