using UnityEngine;
using UnityEngine.Rendering.Universal;

public class GameInitializer : MonoBehaviour
{
    [SerializeField] private GameObject _scenesManager;
    [SerializeField] private Camera _camera;
    [SerializeField] private Light2D _light;
    [SerializeField] private GameObject _menu;
    [SerializeField] private GameObject _playerHealthScoreBullets_UI;
    [SerializeField] private GameObject _playerInputUI;
    [SerializeField] private GameObject _baseWalls;

    [SerializeField] private GameObject _backgroundMusicPlayer;




    private void Start()
    {
        InstantiateGameObjects();
    }

    private async void InstantiateGameObjects()
    {
        using (LoadingWindow_UI loadingPage_UI = new LoadingWindow_UI(new GameObject()))
        {
            _scenesManager = Instantiate(_scenesManager);
            _scenesManager.GetComponent<ScenesManager>().Initialize();
            loadingPage_UI.SetProgress(10);

            _camera = Instantiate(_camera);
            loadingPage_UI.SetProgress(20);

            _light = Instantiate(_light);
            loadingPage_UI.SetProgress(30);

            _menu = Instantiate(_menu);
            _menu.GetComponent<Menu_UIConnector>().Initialize();
            loadingPage_UI.SetProgress(40);

            _playerHealthScoreBullets_UI = Instantiate(_playerHealthScoreBullets_UI);
            _playerHealthScoreBullets_UI.GetComponent<PlayerHealthScoreBullets_UI>().Initialize();
            loadingPage_UI.SetProgress(50);

            _playerInputUI = Instantiate(_playerInputUI);
            _playerInputUI.GetComponent<PlayerInputUI_Controller>().Initialize();
            loadingPage_UI.SetProgress(60);

            _baseWalls = Instantiate(_baseWalls);
            loadingPage_UI.SetProgress(70);

            _backgroundMusicPlayer = Instantiate(_backgroundMusicPlayer);
            _backgroundMusicPlayer.GetComponent<BackgroundMusicPlayer>().Initialize();
            loadingPage_UI.SetProgress(80);



            loadingPage_UI.SetProgress(100);

            await Awaitable.WaitForSecondsAsync(1f);
        }

    }

}
