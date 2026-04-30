using System.Collections.Generic;
using UnityEngine;

public class Pool<T> where T : IObjectInPool
{
    private GameObject Prefab;

    private List<GameObject> PoolList;
    private int MaxObjectsInPool;
    private int CurrentObjectsInPool;

    public Pool(GameObject prefab, int maxObjectsInPool)
    {
        Prefab = prefab;
        PoolList = new List<GameObject>();
        MaxObjectsInPool = maxObjectsInPool;
        CurrentObjectsInPool = 0;
    }

    public bool CanGetGameObject()
    {
        for (int i = 0; i < PoolList.Count; i++)
        {
            if (!PoolList[i].GetComponent<T>().IsEnable)
            {
                return true;
            }
        }

        if (CurrentObjectsInPool < MaxObjectsInPool)
        {
            return true;
        }

        return false;
    }

    public GameObject GetGameObject()
    {
        for (int i = 0; i < PoolList.Count; i++)
        {
            if (!PoolList[i].GetComponent<T>().IsEnable)
            {
                return PoolList[i];
            }
        }
        return AddGameObjectToPool();
    }

    private GameObject AddGameObjectToPool()
    {
        GameObject gameObject = MonoBehaviour.Instantiate(Prefab);
        PoolList.Add(gameObject);
        CurrentObjectsInPool++;
        return gameObject;
    }

}
