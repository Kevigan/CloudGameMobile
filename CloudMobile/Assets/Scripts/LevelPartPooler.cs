using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPartPooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    public static LevelPartPooler Instance;

    private void Awake()
    {
        Instance = this;

        InstantiatePools();
    }

    public List<Pool> poolsLevelParts;
    public Dictionary<string, Queue<GameObject>> poolDictionaryLevelPart;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Pool item in poolsLevelParts)
        {
        }
    }

    public void InstantiatePools()
    {
        poolDictionaryLevelPart = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in poolsLevelParts)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionaryLevelPart.Add(pool.tag, objectPool);
        }
    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionaryLevelPart.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesnt exist.");
            return null;
        }

        GameObject objectToSpawn = poolDictionaryLevelPart[tag].Dequeue();

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        poolDictionaryLevelPart[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }


}
