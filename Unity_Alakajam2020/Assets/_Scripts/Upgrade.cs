using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu]
public class Upgrade : ScriptableObject
{
    [System.Serializable]
    public struct UpgradeInfo
    {
        public string Text;
        public string ToolTipText;
        public Color Outer;
        public Color Middle;
        public Color Inner;
        public UpgradeType Type;
        public int PosChange;
        public int negChange;
        public GameObject spawn; 
        public Sprite Icon;
    }

    public enum UpgradeType
    {
        MoreLevelLessLife,
        MoreSpeedLessRadus,
        MoreRadiusLessLevel,
        MoreLifeLessSpeed,
        
    }
    
    public UpgradeInfo[] upgrades = Array.Empty<UpgradeInfo>();



}
