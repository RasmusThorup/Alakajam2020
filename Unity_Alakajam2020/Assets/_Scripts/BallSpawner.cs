using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class BallSpawner : MonoBehaviour
{
    [BoxGroup("Settings")]
    public Vector2 randomXPositionMinMax = new Vector2(-5,5);
    [BoxGroup("Settings")]
    public Vector2 randomYPositionMinMax = new Vector2(-5,5);
    BallPooler ballPooler;

    void Start()
    {
        ballPooler = BallPooler.Instance;
    }

    void FixedUpdate()
    {
        ballPooler.SpawnFromPool("Base Ball", new Vector2(Random.Range(randomXPositionMinMax.x,randomXPositionMinMax.y),Random.Range(randomYPositionMinMax.x,randomYPositionMinMax.y)));
    }
}
