using UnityEngine;

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

        _walls = Instantiate(_walls);
        loading_UI.SetProgress(10);

        _enemyGenerator = Instantiate(_enemyGenerator);
        while (GameData.CurrentLevelNumber == 0)
        {
            await Awaitable.WaitForSecondsAsync(0.1f);
        }
        _enemyGenerator.GetComponent<EnemyGenerator>().Initialize();
        loading_UI.SetProgress(20);

        loading_UI.SetProgress(100);

        Destroy(loading_UI);
        Destroy(_loading_UI.gameObject);

        await Awaitable.WaitForSecondsAsync(3f);
        _enemyGenerator.GetComponent<EnemyGenerator>().GenerateEnemys();
    }
}
