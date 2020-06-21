using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{

    public int currentScore = 0;
    private int displayScore; 

    public void CheckScore()
    {
        if (currentScore > GetHighScore())
        {
            PlayerPrefs.SetInt("HighScore", currentScore);
            
            //TODO: Set HighScore In UI Somewhere. 
        }
    }

    public void IncreaseScore(int scoreValue)
    {
        currentScore += scoreValue;
       
    }

    private void Update()
    {
        if (displayScore < currentScore)
        {
            displayScore++; 
            GameManager.Instance.UiManager.score.SetText("Score: " + displayScore.ToString());
        }
    }


    public void DecreaseScore(int scoreValue)
    { 
        currentScore -= scoreValue;
        if (currentScore <= 0)
        {
            currentScore = 0; 
        }
        GameManager.Instance.UiManager.score.SetText(currentScore.ToString());
    }

    public int GetHighScore()
    {
        int score = PlayerPrefs.GetInt("HighScore", 0);
        return score; 
    }
    
    
}
