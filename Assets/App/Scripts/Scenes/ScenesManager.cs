using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{

    public void Initialize()
    {
        GameState_EventManager.OnStartLevel_Event += OnLevelStarted;
        GameState_EventManager.OnStopLevel_Event += OnStopLevel;

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

    public void OnLevelStarted(object o, GameState_EventArgs gameState_EventArgs)
    {
        string sceneName = "";
        if (gameState_EventArgs.LevelNumber < 10)
        {
            sceneName = "Level0" + gameState_EventArgs.LevelNumber;
        }
        else
        {
            sceneName = "Level" + gameState_EventArgs.LevelNumber;
        }

        GameData.currentLevelName = sceneName;
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }


    public async void OnStopLevel(object o, GameState_EventArgs gameState_EventArgs)
    {
        if (SceneManager.GetSceneByName(GameData.currentLevelName).isLoaded)
        {
            await SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName(GameData.currentLevelName));
            GameData.currentLevelName = "";
        }
    }
}
