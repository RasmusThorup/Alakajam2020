using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveBarController : MonoBehaviour
{
    public float endScale = 8.25f;
    public float startPosition = -41.2f;
    float t;

    void Update() 
    {
        if (!GameManager.Instance.gameHasStarted)
        {
            //transform.localScale = Vector3.zero;
            return;
        }

        t = GameManager.Instance.waveManager.currentTime/GameManager.Instance.waveManager.waveTime;

        transform.localPosition = new Vector3(t*startPosition, 22.35f, 10);
        transform.localScale = new Vector3((1-t)*endScale, 1, 0.125f);
    } 
}
