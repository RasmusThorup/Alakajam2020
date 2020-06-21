using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BallSpawnValue
{
    public string ballName;
    public int ballAmount;
    public float spawnChance;

    public BallSpawnValue(string name, int amount, float spawnChance)
    {
        this.ballName = name;
        this.ballAmount = amount;
        this.spawnChance = spawnChance;

    }
}

public class WaveManager : MonoBehaviour
{
    private static WaveManager _instance;

    public static WaveManager instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;



        currentTime = waveTime;
    }

    public List<BaseBall> activeBallList = new List<BaseBall>();
    public List<BallSpawnValue> ballTypes = new List<BallSpawnValue>();

    public float timeToSpawn;

    public float waveTime;
    private float currentTime;

    public int waveCounter;

    public void Update()
    {
        if (currentTime <= 0)
        {
            waveCounter++;
            NewWave();
            currentTime = waveTime;
        }
        currentTime -= Time.deltaTime;
    }

    public void NewWave()
    {

        for (int i = 0; i < ballTypes.Count; i++)
        {
            BallPooler.Instance.SpawnBalls(ballTypes[i].ballName, ballTypes[i].ballAmount, timeToSpawn);
        }
    }
}
