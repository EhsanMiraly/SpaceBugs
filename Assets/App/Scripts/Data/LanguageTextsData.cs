using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LanguageTextsData
{
    public static Font font_Farsi = Resources.Load<Font>("Fonts/Farsi/Parastoo-Bold");
    public static Font font_English = Resources.Load<Font>("Fonts/English/Roboto-Medium");

    #region LoadingPage
    public static List<string> loading = new List<string> { "Loading...", "در حال لود شدن..." };
    #endregion


    #region MainPage
    public static List<string> resume = new List<string> { "Resume", "ادامه" };
    public static List<string> levels = new List<string> { "Levels", "لول‌ها" };
    public static List<string> settings = new List<string> { "Settings", "تنظیمات" };
    public static List<string> exit = new List<string> { "Exit", "خروج" };
    #endregion

    #region LevelsPage
    public static List<string> level = new List<string> { "Level", "لول" };
    #endregion

    #region SettingsPage

    public static List<Language> languages = new List<Language>
    {
        new Language("English", LanguageDirection.LTR, font_English),
        new Language("فارسی", LanguageDirection.RTL,font_Farsi)
    };

    public static List<FontSize> fontSize_Text = new List<FontSize>
    {
        new FontSize("Small","کوچک"),
        new FontSize("Average","متوسط"),
        new FontSize("Big","بزرگ")
    };

    public static List<int> fontSize_CategorySmall = new List<int> { 10, 20, 30 };
    public static List<int> fontSize_CategoryAverage = new List<int> { 20, 40, 60 };
    public static List<int> fontSize_CategoryBig = new List<int> { 40, 80, 120 };

    public static List<string> backgroundMusic = new List<string> { "Background music", "موزیک پس‌زمینه" };
    public static List<string> soundEffects = new List<string> { "Sound effects", "جلوه‌های صوتی" };

    #endregion
}

public class Language
{
    public string language { get; }
    public LanguageDirection languageDirection { get; }
    public Font font { get; }

    public Language(string language, LanguageDirection languageDirection, Font font)
    {
        this.language = language;
        this.languageDirection = languageDirection;
        this.font = font;
    }
}

public class FontSize
{
    public List<string> FontSizeLanguage { get; }

    public FontSize(string fontSizeEnglish, string fontSizeFarsi)
    {
        FontSizeLanguage = new List<string> { fontSizeEnglish, fontSizeFarsi };
    }
}
