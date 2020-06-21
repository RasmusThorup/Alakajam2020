using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;


[CreateAssetMenu]
public class Upgrade : ScriptableObject
{
    [System.Serializable]
    public struct UpgradeInfo
    {
        public string Text;
        public string ToolTipText;
        public UpgradeType Type;
        public int PosChange;
        public int negChange;
        public GameObject spawn; 
        //public Sprite Icon;
        public bool isPlaceableObject;
    }

    public enum UpgradeType
    {
        NotUsed,
        MoreLevelLessLife,
        MoreSpeedLessRadus,
        MoreRadiusLessLevel,
        MoreLifeLessSpeed
    }
    
    public UpgradeInfo[] upgrades = Array.Empty<UpgradeInfo>();
}
