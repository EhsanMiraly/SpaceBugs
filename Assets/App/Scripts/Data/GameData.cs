using UnityEngine;
using UnityEngine.SceneManagement;

public class GameData
{
    public static bool IsPlaying { get; set; } = false;
    public static string currentLevelName { get; set; } = "";
    public static int CurrentLevelNumber { get; set; } = 0;
    public static LevelData_SO currentLevelData { get; set; } = null;


    public static bool IsGameDataSet()
    {
        if (IsPlaying == false || currentLevelName == "" || CurrentLevelNumber == 0)// || currentLevelData == null
        {
            return false;
        }
        return true;
    }

    public static void ResetGameData()
    {
        IsPlaying = false;
        currentLevelName = "";
        CurrentLevelNumber = 0;
        currentLevelData = null;
    }
}
