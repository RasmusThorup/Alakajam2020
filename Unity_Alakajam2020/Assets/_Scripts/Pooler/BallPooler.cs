using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class BallPooler : MonoBehaviour
{

    [System.Serializable]
    public class Pool{
        public string tag;
        public GameObject prefab;

        public int size;
    }

    //-- Ugly singleton
    public static BallPooler Instance;

    private void Awake()
    {
        Instance = this;
    }
    //--

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    public GameObject SpawnFromPool (string tag, Vector2 position)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exists.");
            return null;
        }

        GameObject objectToSpawn = poolDictionary[tag].Dequeue();

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = new Vector3(position.x, position.y, 0);

        IPooledObject pooledObj = objectToSpawn.GetComponent<IPooledObject>();

        if (pooledObj != null)
            pooledObj.OnObjectSpawn();

        poolDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }

    public void SpawnBalls(string tag, int amount, Vector2 ranPosXMinMax, Vector2 ranPosYMinMax, float timeToSpawnTheAmount)
    {
        if (amount == 0)
        {
            Debug.LogWarning("Tried to spawn 0 amount of balls, no can do mister");
            return;
        }
        StartCoroutine(SpawnTheBallsCoroutine(tag, amount, ranPosXMinMax, ranPosYMinMax, timeToSpawnTheAmount));

    }

    IEnumerator SpawnTheBallsCoroutine(string tag, int amount, Vector2 ranPosXMinMax, Vector2 ranPosYMinMax, float timeToSpawnTheAmount)
    {
        float timeBetweenBalls = timeToSpawnTheAmount/amount;
        
        for (int i = 0; i < amount; i++)
        {   
            float x = Random.Range(ranPosXMinMax.x,ranPosXMinMax.y);
            float y = Random.Range(ranPosYMinMax.x,ranPosYMinMax.y);
            SpawnFromPool(tag,new Vector2(x, y));
            yield return new WaitForSeconds(timeBetweenBalls);
        }

        yield return null;
    }

    [Button("Test Spawn 69 Balls over 4.20 seconds")]
    void TestTriggerSpawner()
    {
        SpawnBalls("Base Ball", 69, new Vector2(-5,5), new Vector2(-5,5), 4.2f);
    }
}
