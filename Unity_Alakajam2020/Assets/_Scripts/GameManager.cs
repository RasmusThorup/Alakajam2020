using System;
using System.Collections;
using System.Collections.Generic;
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
        gameHasStarted = true; 
    }


    private void Update()
    {

        if (!gameHasStarted)
        {
            return; 
        }

        GetCurrentBalls();
        
    }


    public void GetCurrentBalls()
    {
        int currentInfected = 0;
        int ballsAmount = waveManager.activeBallList.Count; 
        //int currentCitizens; 
        for (int i = 0; i < ballsAmount; i++)
        {
            if (waveManager.activeBallList[i].infected)
            {
                currentInfected++; 
            }
        }
        
        UiManager.percentInfected.SetText("% Infected: " + (currentInfected/ballsAmount)*100);
    }


    public void UpdateUpgrades()
    {
        
        //Show buy screen. 
    }

    public void EndGame()
    {
        // Show UI. Register HighScore 
        
        
        
    }
}
