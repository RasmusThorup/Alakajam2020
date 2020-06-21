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
    public GameObject firstInfectedToPlace;
    [ReadOnly]
    public GameObject placeablePlaceholder;
    [ReadOnly]
    public Vector4 gameAreaEdges;
    [ReadOnly]
    public GameObject upgradeToPlace; 
    [ReadOnly]
    public bool gameHasStarted = false;
    public float timeBeforeGameEnds = 5f;
    private float m_cachedTimeBeforeGameEnds;

    private float scaledInfected;
    private float displayScaledInfected;

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

        if (!UiManager)
            UiManager = FindObjectOfType<UIManager>();
    }

    private void Start()
    {
        UiManager.ShowTitleScreen(true);
    }

    public void PlaceInfected()
    {
        //Debug.Log("Place infected");
        placeablePlaceholder.SetActive(true);
        waveManager.Init();
    }

    public void StartGame()
    {
        m_cachedTimeBeforeGameEnds = timeBeforeGameEnds; 
        gameHasStarted = true; 
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


        if (displayScaledInfected < scaledInfected)
        {
            displayScaledInfected++; 
            UiManager.percentInfected.SetText("Infected: " + displayScaledInfected.ToString("F0") + "%");
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

        scaledInfected = (currentInfected / ballsAmount) * 100;
       

        return currentInfected > 0;
    }

    public void EndGame()
    {
        UiManager.ShowUpgradeUI(false);
        UiManager.ShowEndScreen(true);
        gameHasStarted = false;
    }

    
    public void EnablePlaceableUpgrade(GameObject upgrade)
    {
        upgradeToPlace = upgrade;
        placeablePlaceholder.SetActive(true);
    }
    public void PlacePlaceableUpgrade(Vector3 placement)
    {
        if (!upgradeToPlace)
        {
            Debug.LogWarning("Tried to place an upgrade but no upgrade was given");
            return;
        }

        GameObject obj = Instantiate(upgradeToPlace);
        obj.transform.localPosition = placement;

        upgradeToPlace = null;
    }

    public void PlaceFirstInfected(Vector3 placement)
    {
        GameObject obj = Instantiate(firstInfectedToPlace);
        obj.GetComponent<BaseBall>().SetInfected();
        obj.transform.localPosition = placement;

        StartGame();
    }
}
