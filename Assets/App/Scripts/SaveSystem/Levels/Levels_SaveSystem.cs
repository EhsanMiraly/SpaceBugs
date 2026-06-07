using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Levels_SaveSystem
{
    private static string saveDirectory;
    private static Levels_SaveData levels_SaveData = new Levels_SaveData();


    private static void CreateSaveDirectory()
    {
        saveDirectory = Path.Combine(Application.persistentDataPath, "SaveData");

        if (!Directory.Exists(saveDirectory))
        {
            Directory.CreateDirectory(saveDirectory);
        }
    }

    public static string Levels_SaveFileName()
    {
        CreateSaveDirectory();
        return saveDirectory + "/Levels_SaveData" + ".txt";
    }

    public static void Save_Levels()
    {
        levels_SaveData.Levels = LevelsData.Levels;

        File.WriteAllText(Levels_SaveFileName(), JsonUtility.ToJson(levels_SaveData, true));
    }

    public static void Load_Levels()
    {
        if (!File.Exists(Levels_SaveFileName()))
        {
            return;
        }

        string saveContent = File.ReadAllText(Levels_SaveFileName());

        levels_SaveData = JsonUtility.FromJson<Levels_SaveData>(saveContent);

        LevelsData.FillLevelsData(levels_SaveData.Levels);
    }

}


[System.Serializable]
public struct Levels_SaveData
{
    public Level[] Levels;
}

[System.Serializable]
public struct Level
{
    public bool IsOpen;
    public int Progress;
    public int Coins;

    public Level(bool isOpen, int progress, int coins)
    {
        IsOpen = isOpen;
        Progress = progress;
        Coins = coins;
    }
}