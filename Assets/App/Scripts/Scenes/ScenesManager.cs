using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{

    public void Initialize()
    {
        GameState_EventManager.OnStartLevel_Event += OnStartLevel;
        GameState_EventManager.OnPauseLevel_Event += OnPauseLevel;
        GameState_EventManager.OnResumeLevel_Event += OnResumeLevel;
        GameState_EventManager.OnStopLevel_Event += OnStopLevel;

    }

    private void OnDisable()
    {
        GameState_EventManager.OnStartLevel_Event -= OnStartLevel;
        GameState_EventManager.OnPauseLevel_Event -= OnPauseLevel;
        GameState_EventManager.OnResumeLevel_Event -= OnResumeLevel;
        GameState_EventManager.OnStopLevel_Event -= OnStopLevel;
    }


    public async void OnStartLevel(object o, GameState_EventArgs gameState_EventArgs)
    {
        Time.timeScale = 1;
        GameData.CurrentLevelNumber = gameState_EventArgs.LevelNumber;
        GameData.IsPlaying = true;

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
        await SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
    }

    public void OnPauseLevel(object o, GameState_EventArgs gameState_EventArgs)
    {
        Time.timeScale = 0;
    }

    public void OnResumeLevel(object o, GameState_EventArgs gameState_EventArgs)
    {
        Time.timeScale = 1;
    }

    public async void OnStopLevel(object o, GameState_EventArgs gameState_EventArgs)
    {
        if (SceneManager.GetSceneByName(GameData.currentLevelName).isLoaded)
        {
            await SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName(GameData.currentLevelName));
        }

        PlayerData.ResetPlayerData();
        GameData.ResetGameData();
    }

}
