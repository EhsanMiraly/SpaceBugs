using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public void Initialize()
    {
        GameState_EventManager.OnStartLevel_Event += OnLevelChanged;
        GameState_EventManager.OnStopLevel_Event += OnExitLevel;

        GameState_EventManager.OnStartLevel_Event +=
            (object g, GameState_EventArgs gameState_EventArgs) =>
            {
                GameData.CurrentLevelNumber = gameState_EventArgs.LevelNumber;
                Time.timeScale = 1;
            };

        GameState_EventManager.OnPauseLevel_Event +=
            (object g, GameState_EventArgs gameState_EventArgs) =>
            {
                GameData.CurrentLevelNumber = gameState_EventArgs.LevelNumber;
                Time.timeScale = 0;
            };

        GameState_EventManager.OnResumeLevel_Event +=
            (object g, GameState_EventArgs gameState_EventArgs) =>
            {
                GameData.CurrentLevelNumber = gameState_EventArgs.LevelNumber;
                Time.timeScale = 1;
            };

        GameState_EventManager.OnStopLevel_Event +=
            (object g, GameState_EventArgs gameState_EventArgs) =>
            {
                GameData.CurrentLevelNumber = gameState_EventArgs.LevelNumber;
                Time.timeScale = 0;
            };

    }

    public async void OnLevelChanged(object o, GameState_EventArgs gameState_EventArgs)
    {
        if (GameData.currentLevel.isLoaded)
        {
            //Debug.Log("I Am Active");
            await SceneManager.UnloadSceneAsync(GameData.currentLevel);
        }

        string sceneName = "";
        if (gameState_EventArgs.LevelNumber < 10)
        {
            sceneName = "Level0" + gameState_EventArgs.LevelNumber;
        }
        else
        {
            sceneName = "Level" + gameState_EventArgs.LevelNumber;
        }

        GameData.currentLevel = SceneManager.GetSceneByName(sceneName);
        GameData.currentLevelName = sceneName;
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }


    public async void OnExitLevel(object o, GameState_EventArgs gameState_EventArgs)
    {

        if (SceneManager.GetSceneByName(GameData.currentLevelName).isLoaded)
        {
            Debug.Log("I Am Active");
            await SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName(GameData.currentLevelName));
        }
        /*
                if (GameData.currentLevel.isLoaded)
                {
                    //Debug.Log("I Am Active");
                    await SceneManager.UnloadSceneAsync(GameData.currentLevel);
                }
                */
    }
}
