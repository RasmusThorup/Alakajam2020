using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

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

    [BoxGroup("Wave Setup")]
    public float timeToSpawn;
    [BoxGroup("Wave Setup")]
    public float waveTime;
    [BoxGroup("First Wave Setup")]
    public int firstWaveCitizentsAmount;
    [BoxGroup("First Wave Setup")]
    public float firstWaveSpawnTime = 3;
    private float currentTime;

    private int waveCounter;

    public void Start()
    {

    }

    public void Init()
    {
        currentTime = waveTime;
        BallPooler.Instance.SpawnBalls("Citizen", firstWaveCitizentsAmount, firstWaveSpawnTime);
    }

    public void Update()
    {
        if (!GameManager.Instance.gameHasStarted) return;
        
        if (currentTime <= 0)
        {
            waveCounter++;
            NewWave();
            GameManager.Instance.UiManager.ShowUpgradeUI(true);
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
            int ballSpawnAmount = 0;

            if (waveCounter % ballSpawn.additionalBallSpawn == 0)
            {
                ballSpawn.ballAmount++;

            }
            for (int j = 0; j < ballSpawn.ballAmount; j++)
            {
                if (Random.value < ballSpawn.ballSpawnChance)
                {
                    ballSpawnAmount++;
                }
            }
            BallPooler.Instance.SpawnBalls(ballSpawn.ballName, ballSpawnAmount, timeToSpawn);
        }
    }
}
