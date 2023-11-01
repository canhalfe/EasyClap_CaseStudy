using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
//using Sirenix.OdinInspector;

public class ObjectPoolManager : Singleton<ObjectPoolManager>
{
    public List<ObjectPoolData> objectList;
    void Start()
    {
        for (int i = 0; i < objectList.Count; i++)
        {
            ObjectPoolData pool = objectList[i];
            SpawnObjects(objectList[i].id, pool.spawnCount);
        }
    }

    void SpawnObjects(int id, int spawnCount)
    {
        ObjectPoolData pool = GetObjectPoolDataByID(id);
        for (int i = 0; i < spawnCount; i++)
        {
            GameObject newItem = Instantiate(pool.prefab);

            pool.poolList.Add(newItem);
            newItem.gameObject.SetActive(false);
        }
    }

    public GameObject GetPool(int id)
    {
        ObjectPoolData pool = GetObjectPoolDataByID(id);

        if (pool.poolList.Count <= 5)
            SpawnObjects(id, 30);

        GameObject item = pool.poolList[0];
        item.gameObject.SetActive(true);
        pool.poolList.RemoveAt(0);

        return item;
    }

    public List<GameObject> GetPoolList(int id, int count)
    {
        ObjectPoolData pool = GetObjectPoolDataByID(id);

        if (pool.poolList.Count <= count)
            SpawnObjects(id, count);


        List<GameObject> list = new List<GameObject>();

        for (int i = 0; i < count; i++)
        {
            list.Add(pool.poolList[0]);
            pool.poolList.RemoveAt(0);
        }
        return list;
    }

    public void AddPool(GameObject item, int id)
    {
        ObjectPoolData pool = GetObjectPoolDataByID(id);
        pool.poolList.Add(item);
        item.gameObject.SetActive(false);
    }
    public void AddPoolList(List<GameObject> items, int id)
    {
        ObjectPoolData pool = GetObjectPoolDataByID(id);

        for (int i = 0; i < items.Count; i++)
        {
            pool.poolList.Add(items[i]);
        }
    }

    ObjectPoolData GetObjectPoolDataByID(int ID)
    {
        for (int i = 0; i < objectList.Count; i++)
        {
            if (ID == objectList[i].id)
                return objectList[i];
        }

        return null;
    }
}

[System.Serializable]
public class ObjectPoolData
{
    public int id;
    public string Name;
    public int spawnCount;
    public GameObject prefab;

    [Space]
    //[ReadOnly] public int index;
    [ReadOnly] public List<GameObject> poolList;
}