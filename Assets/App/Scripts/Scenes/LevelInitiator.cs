using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelInitiator : MonoBehaviour
{
    [SerializeField] private GameObject _enemyGenerator;
    [SerializeField] private GameObject _walls;
    [SerializeField] private GameObject _player;


    private async void Start()
    {
        using (LoadingPage_UI loadingPage_UI = new LoadingPage_UI(new GameObject()))
        {
            while (!GameData.IsGameDataSet())
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

            await Awaitable.WaitForSecondsAsync(1f);

            _player = Instantiate(_player);
            SceneManager.MoveGameObjectToScene(_player, SceneManager.GetSceneByName(GameData.currentLevelName));
            _player.GetComponent<Player_Controller>().Initialize();
            PlayerData.ResetPlayerData();
            loadingPage_UI.SetProgress(30);

            loadingPage_UI.SetProgress(100);

            await Awaitable.WaitForSecondsAsync(1f);
        }

        await Awaitable.WaitForSecondsAsync(3f);
        if (_enemyGenerator != null)
            _enemyGenerator.GetComponent<EnemyGenerator>().GenerateEnemys();

    }
}
