using System;
using System.Collections;
using System.Collections.Generic;
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
        public UpgradeType Type;
        public int PosChange;
        public int negChange;
        public GameObject spawn; 
        public bool isPlaceableObject;
    }

    public enum UpgradeType
    {
        NotUsed,
        MoreLevelLessLife,
        MoreSpeedLessRadus,
        MoreRadiusLessLevel,
        MoreLifeLessSpeed,
        ShrinkPlayableArea,
        SpherePlayableArea
    }
    
    public UpgradeInfo[] upgrades = Array.Empty<UpgradeInfo>();
}
