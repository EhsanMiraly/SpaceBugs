using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{

    public void Initialize()
    {
        EventsManager.OnStartLevel_Event += OnStartLevel;
        EventsManager.OnPauseLevel_Event += OnPauseLevel;
        EventsManager.OnResumeLevel_Event += OnResumeLevel;
        EventsManager.OnStopLevel_Event += OnStopLevel;

    }

    private void OnDisable()
    {
        EventsManager.OnStartLevel_Event -= OnStartLevel;
        EventsManager.OnPauseLevel_Event -= OnPauseLevel;
        EventsManager.OnResumeLevel_Event -= OnResumeLevel;
        EventsManager.OnStopLevel_Event -= OnStopLevel;
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

    public void OnPauseLevel()
    {
        Time.timeScale = 0;
    }

    public void OnResumeLevel()
    {
        Time.timeScale = 1;
    }

    public async void OnStopLevel()
    {
        if (SceneManager.GetSceneByName(GameData.currentLevelName).isLoaded)
        {
            await SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName(GameData.currentLevelName));
        }

        PlayerData.ResetPlayerData();
        GameData.ResetGameData();
    }

}
