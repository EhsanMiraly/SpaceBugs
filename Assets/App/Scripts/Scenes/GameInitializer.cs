using UnityEngine;
using UnityEngine.Rendering.Universal;

public class GameInitializer : MonoBehaviour
{
    [SerializeField] private GameObject _scenesManager;
    [SerializeField] private Camera _camera;
    [SerializeField] private Light2D _light;
    [SerializeField] private GameObject _menu;
    [SerializeField] private GameObject _screen_UI;
    [SerializeField] private GameObject _baseWalls;


    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private GameObject _enemy;
    [SerializeField] private GameObject _enemyGenerator;




    private void Start()
    {
        InstantiateGameObjects();
    }

    private async void InstantiateGameObjects()
    {
        using (LoadingPage_UI loadingPage_UI = new LoadingPage_UI(new GameObject()))
        {
            _scenesManager = Instantiate(_scenesManager);
            _scenesManager.GetComponent<ScenesManager>().Initialize();
            loadingPage_UI.SetProgress(10);

            _camera = Instantiate(_camera);
            loadingPage_UI.SetProgress(20);

            _light = Instantiate(_light);
            loadingPage_UI.SetProgress(30);

            _menu = Instantiate(_menu);
            _menu.GetComponent<MenuController>().Initialize();
            loadingPage_UI.SetProgress(40);

            _screen_UI = Instantiate(_screen_UI);
            _screen_UI.GetComponent<Screen_UI>().Initialize();
            loadingPage_UI.SetProgress(50);

            _baseWalls = Instantiate(_baseWalls);
            loadingPage_UI.SetProgress(60);

            _player = Instantiate(_player);
            _player.GetComponent<Player_Controller>().Initialize();
            loadingPage_UI.SetProgress(70);

            loadingPage_UI.SetProgress(100);

            await Awaitable.WaitForSecondsAsync(10f);
        }

        /*
        _enemyGenerator = Instantiate(_enemyGenerator);
        _enemyGenerator.GetComponent<EnemyGenerator>().Initialize();
        loading_UI.SetProgress(80);
        */


        //_bullet = Instantiate(_bullet);
        //_enemy = Instantiate(_enemy);


    }

}
