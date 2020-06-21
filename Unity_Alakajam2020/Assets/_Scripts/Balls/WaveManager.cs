using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    private static WaveManager _instance;

    public static WaveManager instance
    {
        get { return _instance; }
    }



    public List<BaseBall> activeBallList;


}
