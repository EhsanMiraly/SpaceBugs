using System;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerCombatInfo_UI : MonoBehaviour
{
    UIDocument uIDocument;
    VisualElement root;

    VisualElement healthBarBackground_VisualElement;
    VisualElement healthBarForeground_VisualElement;

    Label score_Label;
    Label bullets_Label;




    public void Initialize()
    {
        EventsManager.OnLanguageChanged_Event += OnLanguageChanged;
        EventsManager.OnFontSizeChanged_Event += OnFontSizeChanged;

        ConnectUI();
        InitializeUI();
    }

    private void OnDisable()
    {
        EventsManager.OnLanguageChanged_Event -= OnLanguageChanged;
        EventsManager.OnFontSizeChanged_Event -= OnFontSizeChanged;
    }

    private void ConnectUI()
    {
        uIDocument = GetComponent<UIDocument>();
        root = uIDocument.rootVisualElement;

        healthBarBackground_VisualElement = root.Q<VisualElement>("Background_VisualElement");
        healthBarForeground_VisualElement = root.Q<VisualElement>("Foreground_VisualElement");
        score_Label = root.Q<Label>("Score_Label");
        bullets_Label = root.Q<Label>("Bullets_Label");
    }

    public void InitializeUI()
    {
        int x = (Screen.width / 100) * 20;
        int y = (Screen.height / 100) * 5;

        healthBarBackground_VisualElement.style.width = x;
        healthBarBackground_VisualElement.style.height = y;

        healthBarForeground_VisualElement.style.width = Length.Percent(100);
        healthBarForeground_VisualElement.style.height = Length.Percent(100);

        score_Label.style.width = x;
        score_Label.style.height = y;

        bullets_Label.style.width = x;
        bullets_Label.style.height = y;

        OnLanguageChanged();
        OnFontSizeChanged();
    }

    public void UpdateHealthInUI()
    {
        float x = (100 * PlayerData.CurrentHealth) / AchievementsData.health;

        healthBarForeground_VisualElement.style.width = Length.Percent(x);
    }

    public void UpdateScoreInUI()
    {
        score_Label.text =
            LanguageTextsData.score[SettingsData.currentLanguageIndex] + PlayerData.Score
            + " / " + GameData.currentLevelData.ScoreNeeded;
    }

    public void UpdateBulletsInUI()
    {
        bullets_Label.text =
            LanguageTextsData.bullets[SettingsData.currentLanguageIndex] + PlayerData.CurrentBullets;
    }



    private void OnLanguageChanged()
    {
        #region Score
        score_Label.text =
            LanguageTextsData.score[SettingsData.currentLanguageIndex] + PlayerData.Score;
        score_Label.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        score_Label.style.unityFont =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        #endregion

        #region Bullets
        bullets_Label.text =
            LanguageTextsData.bullets[SettingsData.currentLanguageIndex] + PlayerData.CurrentBullets;
        bullets_Label.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        bullets_Label.style.unityFont =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        #endregion
    }

    private void OnFontSizeChanged()
    {
        #region Score
        score_Label.style.fontSize =
            LanguageTextsData.fontSize_CategorySmall[SettingsData.currentFontSizeIndex];
        #endregion

        #region Bullets
        bullets_Label.style.fontSize =
            LanguageTextsData.fontSize_CategorySmall[SettingsData.currentFontSizeIndex];
        #endregion
    }

}
