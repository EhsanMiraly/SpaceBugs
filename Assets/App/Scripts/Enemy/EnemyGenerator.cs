using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    bool timeToMakeEnemy = false;

    [SerializeField] GameObject enemyPrefab;
    [SerializeField] List<EnemyData_SO> enemysData; //Delete This???
    [SerializeField] List<LevelData_SO> levelsData;

    List<Pool<Enemy>> enemyPoolsList;

    private int enemyGenerationRate = 5; // Later Based On LevelData_SO


    private void Awake()
    {
        enemyPoolsList = new List<Pool<Enemy>>();
        foreach (EnemyData_SO enemyData in enemysData)
        {
            enemyPoolsList.Add(new Pool<Enemy>(enemyPrefab, enemyData.MaxInPool));
        }

        GameState_EventManager.OnStartLevel_Event += GenerateEnemys;
    }

    private async void GenerateEnemys(object o, GameState_EventArgs gameState_EventArgs)
    {
        PlayerData.IsPlaying = gameState_EventArgs.IsPlaying;
        PlayerData.IsPaused = gameState_EventArgs.IsPaused;
        PlayerData.CurrentLevelNumber = gameState_EventArgs.LevelNumber;
        PlayerData.CurrentLevelID = gameState_EventArgs.LevelID;

        while (PlayerData.IsPlaying && PlayerData.CurrentLevelID == gameState_EventArgs.LevelID)
        {
            await Awaitable.WaitForSecondsAsync(1f);
            if (!PlayerData.IsPaused)
            {
                //Generate Based On Level Number
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

                await Awaitable.WaitForSecondsAsync(enemyGenerationRate);
            }
        }
    }




    private void GenerateOneEnemy()
    {
        //Generate Based On Level Number
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

    public int RandomEnemyDataIndex()
    {
        int totalWeight = 0;
        foreach (int possibility in levelsData[PlayerData.CurrentLevelNumber - 1].EnemiesRespawnPossibility)
        {
            totalWeight += possibility;
        }

        int randomNumber = Random.Range(0, totalWeight);

        int cumulative = 0;
        for (int i = 0; i < levelsData[PlayerData.CurrentLevelNumber - 1].EnemiesRespawnPossibility.Count; i++)
        {
            cumulative += levelsData[PlayerData.CurrentLevelNumber - 1].EnemiesRespawnPossibility[i];
            if (randomNumber < cumulative)
            {
                return i;
            }
        }

        return levelsData[PlayerData.CurrentLevelNumber - 1].EnemiesRespawnPossibility.Count - 1;
    }


    public int RandomEnemyDataIndex2()
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
