using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.Rendering;

public class GameManager : MonoBehaviour
{
    public ScoreManager scoreManager;
    public UIManager UiManager;
    public WaveManager waveManager; 
    public Vector4 gameAreaEdges;
    public bool gameHasStarted = false;
    public float timeBeforeGameEnds = 5f;
    private float m_cachedTimeBeforeGameEnds; 

    //-----Singleton-----
    private static GameManager _instance;
    public static GameManager Instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
    }

    private void Start()
    {
        UiManager.ShowTitleScreen(true);
    }

    public void PlaceInfected()
    {
        
        
        // StartGame(); 
    }

    public void StartGame()
    {
        m_cachedTimeBeforeGameEnds = timeBeforeGameEnds; 
        gameHasStarted = true; 
        waveManager.Init();
    }


    private void Update()
    {

        if (!gameHasStarted)
        {
            return; 
        }

        if (GetCurrentInfectedBalls())
        {
            m_cachedTimeBeforeGameEnds = timeBeforeGameEnds; 
        }
        else
        {
            m_cachedTimeBeforeGameEnds -= Time.deltaTime; 
        }

        if (m_cachedTimeBeforeGameEnds <= 0)
        {
            EndGame();
        }
    }


    public bool GetCurrentInfectedBalls()
    {
        float currentInfected = 0;
        float ballsAmount = waveManager.activeBallList.Count;
        if (ballsAmount == 0)
        {
            return false; 
        }
        for (int i = 0; i < ballsAmount; i++)
        {
            if (waveManager.activeBallList[i].infected)
            {
                currentInfected++; 
            }
        }

        float scaledInfected = (currentInfected / ballsAmount) * 100;
        UiManager.percentInfected.SetText("Infected: " + scaledInfected.ToString("F1") + "%");

        return currentInfected > 0;
    }

    public void EndGame()
    {
        UiManager.ShowUpgradeUI(false);
        UiManager.ShowEndScreen(true);
        gameHasStarted = false;
    }
}
