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

    #region SettingsPage

    public static List<Language> languages = new List<Language>
    {
        new Language("English", LanguageDirection.LTR, font_English),
        new Language("فارسی", LanguageDirection.RTL,font_Farsi)
    };

    public static List<FontSize> fontSizes = new List<FontSize>
    {
        new FontSize("Small","کوچک",50),
        new FontSize("Average","متوسط",100),
        new FontSize("Big","بزرگ",150)
    };
    //Delete 50,100,150 and add multiple numbers for every category of texts
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
    public List<string> FontSizeString { get; }
    public int FontSizeInt { get; }

    public FontSize(string fontSizeEnglish, string fontSizeFarsi, int fontSizeInt)
    {
        FontSizeString = new List<string> { fontSizeEnglish, fontSizeFarsi };
        FontSizeInt = fontSizeInt;
    }
}
