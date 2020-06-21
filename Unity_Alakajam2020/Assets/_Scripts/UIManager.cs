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
    public TextMeshProUGUI endScreenScore; 
    public TextMeshProUGUI endScreenHighScore;
    public TextMeshProUGUI percentInfected;
    public TextMeshProUGUI menuHighScore;
    public GameObject waveCounter;
    public float waveCounterTimer = 4f;
    private float cachedTimer; 
    private bool waveCounterVisible;

    private void Start()
    {
        cachedTimer = waveCounterTimer; 
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
    }

    public void ShowWaveTimer(bool visibility)
    {
        
        waveCounter.SetActive(visibility);
        waveCounterVisible = visibility;

    }

    public void ShowUpgradeUI(bool visibility)
    { 
        upgradeUI.SetActive(visibility);
       upgradeSystem.RefreshUpgrades();
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
    
    

}
