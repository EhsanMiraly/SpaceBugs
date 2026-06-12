using UnityEngine;
using System.IO;

public class Settings_SaveSystem
{
    private static string saveDirectory;
    private static Settings_SaveData settings_SaveData = new Settings_SaveData();


    private static void CreateSaveDirectory()
    {
        saveDirectory = Path.Combine(Application.persistentDataPath, "SaveData");

        if (!Directory.Exists(saveDirectory))
        {
            Directory.CreateDirectory(saveDirectory);
        }
    }

    public static string Settings_SaveFileName()
    {
        CreateSaveDirectory();
        return saveDirectory + "/Settings_SaveData" + ".txt";
    }

    public static void Save_Settings()
    {
        settings_SaveData.currentLanguageIndex = SettingsData.currentLanguageIndex;
        settings_SaveData.currentFontSizeIndex = SettingsData.currentFontSizeIndex;

        settings_SaveData.isBackgroundMusicOn = SettingsData.isBackgroundMusicOn;
        settings_SaveData.backgroundMusicVolume = SettingsData.backgroundMusicVolume;

        settings_SaveData.isSoundEffectsOn = SettingsData.isSoundEffectsOn;
        settings_SaveData.soundEffectsVolume = SettingsData.soundEffectsVolume;

        File.WriteAllText(Settings_SaveFileName(), JsonUtility.ToJson(settings_SaveData, true));
    }

    public static void Load_Settings()
    {
        if (!File.Exists(Settings_SaveFileName()))
        {
            return;
        }

        string saveContent = File.ReadAllText(Settings_SaveFileName());

        settings_SaveData = JsonUtility.FromJson<Settings_SaveData>(saveContent);

        SettingsData.currentLanguageIndex = settings_SaveData.currentLanguageIndex;
        SettingsData.currentFontSizeIndex = settings_SaveData.currentFontSizeIndex;

        SettingsData.isBackgroundMusicOn = settings_SaveData.isBackgroundMusicOn;
        SettingsData.backgroundMusicVolume = settings_SaveData.backgroundMusicVolume;

        SettingsData.isSoundEffectsOn = settings_SaveData.isSoundEffectsOn;
        SettingsData.soundEffectsVolume = settings_SaveData.soundEffectsVolume;
    }

}



[System.Serializable]
public struct Settings_SaveData
{
    public int currentLanguageIndex;
    public int currentFontSizeIndex;

    public bool isBackgroundMusicOn;
    public float backgroundMusicVolume;

    public bool isSoundEffectsOn;
    public float soundEffectsVolume;
}
