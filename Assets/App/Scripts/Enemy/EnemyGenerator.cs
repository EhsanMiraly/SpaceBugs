using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    private bool isPlaying = true;

    [SerializeField] GameObject enemyPrefab;
    [SerializeField] List<EnemyData_SO> enemysData;

    List<Pool<Enemy>> enemyPoolsList;

    private int enemyGenerationRate = 5; // Later Based On LevelData_SO


    private void Awake()
    {
        enemyPoolsList = new List<Pool<Enemy>>();
        foreach (EnemyData_SO enemyData in enemysData)
        {
            enemyPoolsList.Add(new Pool<Enemy>(enemyPrefab, enemyData.MaxInPool));
        }
    }

    async void Start()
    {
        while (isPlaying)
        {
            await Awaitable.WaitForSecondsAsync(enemyGenerationRate);
            //Add While for Full randomEnemyDataIndex
            int randomEnemyDataIndex = RandomEnemyDataIndex();

            if (enemyPoolsList[randomEnemyDataIndex].CanGetGameObject())
            {
                float x = Random.Range(-11f, 11f);
                GameObject enemy = enemyPoolsList[randomEnemyDataIndex].GetGameObject();
                enemy.GetComponent<Enemy>().EnemyData = enemysData[randomEnemyDataIndex];
                enemy.transform.position = transform.position + new Vector3(x, 0f, 0f);
                enemy.transform.rotation = Quaternion.identity;
                enemy.transform.parent = this.transform;
                enemy.GetComponent<Enemy>().StartMoving();
            }
        }
    }

    public int RandomEnemyDataIndex()
    {
        int totalWeight = 0;
        foreach (EnemyData_SO enemyData in enemysData)
        {
            totalWeight += enemyData.RespawnPossibility;
        }

        int randomNumber = Random.Range(0, totalWeight);

        int cumulative = 0;
        for (int i = 0; i < enemysData.Count; i++)
        {
            cumulative += enemysData[i].RespawnPossibility;
            if (randomNumber < cumulative)
            {
                return i;
            }
        }

        return enemysData.Count - 1;
    }

}
