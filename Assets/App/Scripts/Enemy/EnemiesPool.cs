using System.Collections.Generic;
using UnityEngine;


public class EnemiesPool : MonoBehaviour
{
    [SerializeField] List<GameObject> enemyPrefabs = new List<GameObject>();

    private List<List<GameObject>> enemiesPool;
    private int[] maxEnemiesInPool = new int[] { 10 };
    private int[] currentEnemiesInPool = new int[] { 0 };


    private void Awake()
    {
        enemiesPool = new List<List<GameObject>>();

        foreach (GameObject enemy in enemyPrefabs)
        {
            enemiesPool.Add(new List<GameObject>());
        }

    }

    public bool CanGetEnemy(int index = 0) // 0 for now
    {
        for (int i = 0; i < enemiesPool[index].Count; i++)
        {
            if (!enemiesPool[index][i].GetComponent<Enemy>().IsEnable)
            {
                return true;
            }
        }

        if (currentEnemiesInPool[index] < maxEnemiesInPool[index])
        {
            return true;
        }

        return false;
    }

    public GameObject GetEnemy(int index = 0)
    {
        for (int i = 0; i < enemiesPool[index].Count; i++)
        {
            if (!enemiesPool[index][i].GetComponent<Enemy>().IsEnable)
            {
                return enemiesPool[index][i];
            }
        }
        return AddEnemyToPool(index);
    }

    private GameObject AddEnemyToPool(int index)
    {
        GameObject enemy = Instantiate(enemyPrefabs[index]);
        enemiesPool[index].Add(enemy);
        currentEnemiesInPool[index]++;
        return enemy;
    }
}
