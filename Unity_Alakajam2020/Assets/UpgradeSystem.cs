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
        choice.Outer.color = info.Outer;
        choice.Middle.color = info.Middle;
        choice.Inner.color = info.Inner;
        choice.Icon.sprite = info.Icon;
        choice.Button.onClick.RemoveAllListeners();
        choice.Button.onClick.AddListener(() =>
        {

            if (info.spawn == null)
            {
                switch (info.Type)
                {
                    case Upgrade.UpgradeType.MoreLevelLessLife:
                        // Change Settings
                        break;
                    case Upgrade.UpgradeType.MoreLifeLessSpeed:
                        // Change Settings
                        break;
                    case Upgrade.UpgradeType.MoreRadiusLessLevel:
                        // Change Settings
                        break;
                    case Upgrade.UpgradeType.MoreSpeedLessRadus:
                        // Change Settings
                        break;
                }
            }
            else
            {
                //Spawn obj. 
            }
          

            gameObject.SetActive(false);
        });
    }
    
    
    private void OnEnable()
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

        Choices[1].Button.Select();
    }

    
    
    
    
    
}
