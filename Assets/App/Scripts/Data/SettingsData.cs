using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SettingsData
{
    //Move Where?
    public static int NumberOfLevels { get; } = 2;
    public static int Level_Template_Size { get; } = 10;
    public static int Level_Template_Padding { get; } = 100;


    //Move Where?

    public static List<Language> languages = new List<Language>
    {
        new Language("English", LanguageDirection.LTR),
        new Language("فارسی", LanguageDirection.RTL)
    };
    public static int currentLanguageIndex = 0;


    public static bool isBackgroundMusicOn = true;
    public static float backgroundMusicVolume = 0.1f;

    public static bool isSoundEffectsOn = true;
    public static float soundEffectsVolume = 0.1f;

}

public class Language
{
    public string language { get; }
    public LanguageDirection languageDirection { get; }

    public Language(string language, LanguageDirection languageDirection)
    {
        this.language = language;
        this.languageDirection = languageDirection;
    }
}
