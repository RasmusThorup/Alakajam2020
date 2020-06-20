using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using TMPro; 

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI score;
    public Image shopUI;
    public Image endScreen;
    public TextMeshProUGUI endScreenScore; 
    public TextMeshProUGUI endScreenHighScore; 

    public void ShowShopUI(bool visibility)
    {
        shopUI.enabled = visibility;
    }


    public void ShowEndScreen(bool visibility)
    {
        endScreen.enabled = visibility;
        endScreenScore.SetText(GameManager.Instance.scoreManager.currentScore.ToString());
        GameManager.Instance.scoreManager.CheckScore();
        endScreenHighScore.SetText(GameManager.Instance.scoreManager.GetHighScore().ToString());
    }

}
