using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class UpgradeSystem : MonoBehaviour
{
    
   [System.Serializable]
    public struct Choice
    {
        public TextMeshProUGUI Text;
        public TextMeshProUGUI ToolTipText; 
        public Image Outer;
        public Image Middle;
        public Image Inner;
        public Image Icon;
        public Button Button;
    }
    
    public Upgrade Settings;
    public InfectedSettings infectedSetting;
    public Choice[] Choices = System.Array.Empty<Choice>();
    
    private int m_Choice1;
    private int m_Choice2;
    private int m_Choice3;
    
    public void PopulateChoice(Choice choice, Upgrade.UpgradeInfo info)
    {
        choice.Text.text = info.Text;
        choice.ToolTipText.text = info.ToolTipText; 
        choice.Button.onClick.RemoveAllListeners();
        choice.Button.onClick.AddListener(() =>
        {
            if(info.isPlaceableObject)
            {

                //Debug.Log("Ball has been selected");
                GameManager.Instance.EnablePlaceableUpgrade(info.spawn);
                //Spawn obj. 
            }
            else if(info.spawn)
            {
                //Is not a placeable object, but will still spawn sometihng.
                //Debug.Log("Shrink level");
            }
            else
            {
                //Debug.Log("Upgrade values");
                switch (info.Type)
                {
                    case Upgrade.UpgradeType.MoreLevelLessLife:
                        infectedSetting.SetVirusLevel(info.PosChange);
                        infectedSetting.SetLifeTime(info.negChange);

                        break;
                    case Upgrade.UpgradeType.MoreLifeLessSpeed:
                        infectedSetting.SetLifeTime(info.PosChange);
                        infectedSetting.SetSpeed(info.negChange); 

                        break;
                    case Upgrade.UpgradeType.MoreRadiusLessLevel:
                        infectedSetting.SetTriggerRadius(info.PosChange);
                        infectedSetting.SetVirusLevel(info.negChange); 

                        break;
                    case Upgrade.UpgradeType.MoreSpeedLessRadus:
                        infectedSetting.SetSpeed(info.PosChange);
                        infectedSetting.SetTriggerRadius(info.negChange); 

                        break;
                    
                    case Upgrade.UpgradeType.ShrinkPlayableArea:
                        PlayableArea.Instance.ShrinkArea();
                        break;

                    case Upgrade.UpgradeType.SpherePlayableArea:
                        PlayableArea.Instance.SphericalArea();
                        break;
                }
            }          

            //gameObject.SetActive(false);
            GameManager.Instance.UiManager.ShowUpgradeUI(false);
        });
    }
    
    
    public void RefreshUpgrades()
    {

        if (Settings.upgrades.Length == 0)
        {
            return; 
        }
        m_Choice1 = Random.Range(0, Settings.upgrades.Length);
        m_Choice2 = m_Choice1;
        while (m_Choice1 == m_Choice2)
        {
            m_Choice2 = Random.Range(0, Settings.upgrades.Length);
        }

        m_Choice3 = m_Choice1;
        while (m_Choice1 == m_Choice3 || m_Choice2 == m_Choice3)
        {
            m_Choice3 = Random.Range(0, Settings.upgrades.Length);
        }

        PopulateChoice(Choices[0], Settings.upgrades[m_Choice1]);
        PopulateChoice(Choices[1], Settings.upgrades[m_Choice2]);
        PopulateChoice(Choices[2], Settings.upgrades[m_Choice3]);

        //Choices[1].Button.Select();
    }

    
    
    
    
    
}
