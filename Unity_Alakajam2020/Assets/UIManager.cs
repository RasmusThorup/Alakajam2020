using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using TMPro; 

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI score;
    public GameObject upgradeUI;
    public Image endScreen;
    public TextMeshProUGUI endScreenScore; 
    public TextMeshProUGUI endScreenHighScore;
    public TextMeshProUGUI percentInfected; 

    public void ShowShopUI(bool visibility)
    {
        upgradeUI.SetActive(visibility);
    }


    public void ShowEndScreen(bool visibility)
    {
        endScreen.enabled = visibility;
        endScreenScore.SetText(GameManager.Instance.scoreManager.currentScore.ToString());
        GameManager.Instance.scoreManager.CheckScore();
        endScreenHighScore.SetText(GameManager.Instance.scoreManager.GetHighScore().ToString());
    }

}
