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

    [SerializeField] private GameObject _backgroundMusicPlayer;




    private void Start()
    {
        InstantiateGameObjects();
    }

    private async void InstantiateGameObjects()
    {
        Settings_SaveSystem.Load_Settings();
        Achievements_SaveSystem.Load_Achievements();

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
            _playerHealthScoreBullets_UI.GetComponent<PlayerCombatInfo_UI>().Initialize();
            loadingPage_UI.SetProgress(50);

            _playerInputUI = Instantiate(_playerInputUI);
            _playerInputUI.GetComponent<PlayerInputUI_Controller>().Initialize();
            loadingPage_UI.SetProgress(60);

            MakeWallsAroundScreen();
            loadingPage_UI.SetProgress(70);

            _backgroundMusicPlayer = Instantiate(_backgroundMusicPlayer);
            _backgroundMusicPlayer.GetComponent<BackgroundMusicPlayer>().Initialize();
            loadingPage_UI.SetProgress(80);



            loadingPage_UI.SetProgress(100);

            await Awaitable.WaitForSecondsAsync(1f);
        }

    }


    private void MakeWallsAroundScreen()
    {
        GameObject parentGameObject = new GameObject();
        parentGameObject.layer = LayerMask.NameToLayer("Wall");

        float x = Screen.width;
        float y = Screen.height;
        float z = Mathf.Abs(Camera.main.transform.position.z);

        Vector3 ZeroPoint = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, z));
        Vector3 XY = Camera.main.ScreenToWorldPoint(new Vector3(x, y, z));

        //LeftWall
        GameObject leftWall = new GameObject();
        leftWall.layer = LayerMask.NameToLayer("Wall");
        leftWall.transform.parent = parentGameObject.transform;
        leftWall.transform.localScale = new Vector3(1, (XY.y - ZeroPoint.y) * 2, 1);
        leftWall.AddComponent<BoxCollider2D>();
        leftWall.transform.localPosition = new Vector3(ZeroPoint.x - 0.5f, 0, 0);

        //RightWall
        GameObject rightWall = new GameObject();
        rightWall.layer = LayerMask.NameToLayer("Wall");
        rightWall.transform.parent = parentGameObject.transform;
        rightWall.transform.localScale = new Vector3(1, (XY.y - ZeroPoint.y) * 2, 1);
        rightWall.AddComponent<BoxCollider2D>();
        rightWall.transform.localPosition = new Vector3(XY.x + 0.5f, 0, 0);

        //TopWall
        GameObject topWall = new GameObject();
        topWall.layer = LayerMask.NameToLayer("Wall");
        topWall.transform.parent = parentGameObject.transform;
        topWall.transform.localScale = new Vector3((XY.x - ZeroPoint.x) * 2, 1, 1);
        topWall.AddComponent<BoxCollider2D>();
        topWall.transform.localPosition = new Vector3(0, XY.y + 0.5f, 0);

        //DownWall
        GameObject downWall = new GameObject();
        downWall.layer = LayerMask.NameToLayer("EndOfLine");
        downWall.tag = "EndOfLine";
        downWall.transform.parent = parentGameObject.transform;
        downWall.transform.localScale = new Vector3((XY.x - ZeroPoint.x) * 2, 1, 1);
        downWall.AddComponent<BoxCollider2D>();
        downWall.transform.localPosition = new Vector3(0, ZeroPoint.y - 0.5f, 0);
    }



}
