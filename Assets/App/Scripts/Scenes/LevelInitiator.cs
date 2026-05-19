using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelInitiator : MonoBehaviour
{
    [SerializeField] private GameObject _loading_UI;
    [SerializeField] private GameObject _enemyGenerator;
    [SerializeField] private GameObject _walls;


    private async void Start()
    {
        _loading_UI = Instantiate(_loading_UI);
        Loading_UI loading_UI = _loading_UI.GetComponent<Loading_UI>();
        loading_UI.Initialize();

        while (GameData.CurrentLevelNumber == 0 || GameData.currentLevelName == "")
        {
            await Awaitable.WaitForSecondsAsync(0.1f);
        }

        _walls = Instantiate(_walls);
        SceneManager.MoveGameObjectToScene(_walls, SceneManager.GetSceneByName(GameData.currentLevelName));
        loading_UI.SetProgress(10);

        _enemyGenerator = Instantiate(_enemyGenerator);
        SceneManager.MoveGameObjectToScene(_enemyGenerator, SceneManager.GetSceneByName(GameData.currentLevelName));
        _enemyGenerator.GetComponent<EnemyGenerator>().Initialize();
        loading_UI.SetProgress(20);

        loading_UI.SetProgress(100);

        Destroy(loading_UI);
        Destroy(_loading_UI.gameObject);

        await Awaitable.WaitForSecondsAsync(3f);
        if (_enemyGenerator != null)
            _enemyGenerator.GetComponent<EnemyGenerator>().GenerateEnemys();
    }
}
