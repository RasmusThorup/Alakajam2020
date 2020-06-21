using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BallSpawnValue
{
    public string ballName;
    public int ballAmount;
    public float additionalBallSpawn;
    public float ballSpawnChance;

    public BallSpawnValue(string name, int amount, float additionalSpawn, float spawnChance)
    {
        this.ballName = name;
        this.ballAmount = amount;
        this.additionalBallSpawn = additionalSpawn;
        this.ballSpawnChance = spawnChance;

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
    }

    [HideInInspector]
    public List<BaseBall> activeBallList = new List<BaseBall>();
    public List<BallSpawnValue> ballTypes = new List<BallSpawnValue>();

    public float timeToSpawn;

    public float waveTime;
    private float currentTime;

    private int waveCounter;

    public void Start()
    {
        //TODO this should be happening in the start game function
        currentTime = 0;
    }

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
        Debug.Log(waveCounter % 2);

        for (int i = 0; i < ballTypes.Count; i++)
        {
            BallSpawnValue ballSpawn = ballTypes[i];
            if (waveCounter % ballSpawn.additionalBallSpawn == 0)
            {
                ballSpawn.ballAmount++;

            }
            for (int j = 0; j < ballSpawn.ballAmount; j++)
            {
                if (Random.value < ballSpawn.ballSpawnChance)
                {
                    BallPooler.Instance.SpawnBalls(ballSpawn.ballName, 1, timeToSpawn);
                }
            }
        }
    }
}
