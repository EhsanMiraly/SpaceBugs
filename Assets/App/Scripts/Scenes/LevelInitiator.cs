using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelInitiator : MonoBehaviour
{
    [SerializeField] private GameObject _enemyGenerator;
    [SerializeField] private GameObject _walls;


    private async void Start()
    {
        using (LoadingPage_UI loadingPage_UI = new LoadingPage_UI(new GameObject()))
        {
            while (GameData.CurrentLevelNumber == 0 || GameData.currentLevelName == "")
            {
                await Awaitable.WaitForSecondsAsync(0.1f);
            }

            _walls = Instantiate(_walls);
            SceneManager.MoveGameObjectToScene(_walls, SceneManager.GetSceneByName(GameData.currentLevelName));
            loadingPage_UI.SetProgress(10);

            _enemyGenerator = Instantiate(_enemyGenerator);
            SceneManager.MoveGameObjectToScene(_enemyGenerator, SceneManager.GetSceneByName(GameData.currentLevelName));
            _enemyGenerator.GetComponent<EnemyGenerator>().Initialize();
            loadingPage_UI.SetProgress(20);

            loadingPage_UI.SetProgress(100);

            await Awaitable.WaitForSecondsAsync(10f);
        }

        await Awaitable.WaitForSecondsAsync(3f);
        if (_enemyGenerator != null)
            _enemyGenerator.GetComponent<EnemyGenerator>().GenerateEnemys();

    }
}
