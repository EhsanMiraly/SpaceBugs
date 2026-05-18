using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] List<LevelData_SO> levelsData;
    LevelData_SO currentLevelData;

    List<Pool<Enemy>> enemyPoolsList;


    public void Initialize()
    {
        enemyPoolsList = new List<Pool<Enemy>>();
        currentLevelData = levelsData[GameData.CurrentLevelNumber - 1];

        foreach (EnemyData_SO enemyData in currentLevelData.Enemies)
        {
            enemyPoolsList.Add(new Pool<Enemy>(enemyPrefab, enemyData.MaxInPool));
        }
    }

    public async void GenerateEnemys()
    {
        while (true)
        {
            int randomEnemyDataIndex = RandomEnemyDataIndex();

            if (enemyPoolsList[randomEnemyDataIndex].CanGetGameObject())
            {
                float x = Random.Range(-11f, 11f);
                GameObject enemy = enemyPoolsList[randomEnemyDataIndex].GetGameObject();
                enemy.GetComponent<Enemy>().EnemyData = currentLevelData.Enemies[randomEnemyDataIndex];
                enemy.transform.position = transform.position + new Vector3(x, 0f, 0f);
                enemy.transform.rotation = Quaternion.identity;
                enemy.transform.parent = this.transform;
                enemy.GetComponent<Enemy>().StartMoving();

                await Awaitable.WaitForSecondsAsync(currentLevelData.EnemyGenerationRate);
            }
            else
            {
                await Awaitable.WaitForSecondsAsync(1f);
            }

        }
    }

    public int RandomEnemyDataIndex()
    {
        int totalWeight = 0;
        foreach (EnemyData_SO enemyData_SO in currentLevelData.Enemies)
        {
            totalWeight += enemyData_SO.RespawnPossibility;
        }

        int randomNumber = Random.Range(0, totalWeight);

        int cumulative = 0;
        for (int i = 0; i < currentLevelData.Enemies.Count; i++)
        {
            cumulative += currentLevelData.Enemies[i].RespawnPossibility;
            if (randomNumber < cumulative)
            {
                return i;
            }
        }

        return currentLevelData.Enemies.Count - 1;
    }

}
