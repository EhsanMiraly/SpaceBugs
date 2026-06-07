using UnityEngine;
using System.IO;


public class Achievements_SaveSystem
{
    private static string saveDirectory;
    private static Achievements_SaveData achievements_SaveData = new Achievements_SaveData();


    private static void CreateSaveDirectory()
    {
        saveDirectory = Path.Combine(Application.persistentDataPath, "SaveData");

        if (!Directory.Exists(saveDirectory))
        {
            Directory.CreateDirectory(saveDirectory);
        }
    }

    public static string Achievements_SaveFileName()
    {
        CreateSaveDirectory();
        return saveDirectory + "/Achievements_SaveData" + ".txt";
    }

    public static void Save_Achievements()
    {
        achievements_SaveData.coins = AchievementsData.coins;
        achievements_SaveData.health = AchievementsData.health;
        achievements_SaveData.bullets = AchievementsData.bullets;

        File.WriteAllText(Achievements_SaveFileName(), JsonUtility.ToJson(achievements_SaveData, true));
    }

    public static void Load_Achievements()
    {
        if (!File.Exists(Achievements_SaveFileName()))
        {
            return;
        }

        string saveContent = File.ReadAllText(Achievements_SaveFileName());

        achievements_SaveData = JsonUtility.FromJson<Achievements_SaveData>(saveContent);

        AchievementsData.coins = achievements_SaveData.coins;
        AchievementsData.health = achievements_SaveData.health;
        AchievementsData.bullets = achievements_SaveData.bullets;
    }
}


[System.Serializable]
public struct Achievements_SaveData
{
    public int coins;
    public int health;
    public int bullets;
}
