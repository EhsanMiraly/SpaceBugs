using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerCombatInfo_UI : MonoBehaviour
{
    PanelRenderer panelRenderer;


    VisualElement health_VisualElement;
    VisualElement bullets_VisualElement;
    Label progress_Label;

    VisualTreeAsset health_VisualTreeAsset;
    VisualTreeAsset bullet_VisualTreeAsset;

    List<VisualElement> health_List;
    List<VisualElement> bullets_List;



    public void OnEnable()
    {
        panelRenderer = GetComponent<PanelRenderer>();
        panelRenderer.RegisterUIReloadCallback(OnUIReloadCallback);

        health_VisualTreeAsset =
        Resources.Load<VisualTreeAsset>("UI/Basic_Templates/PlayerCombatInfo/Health_Template");
        bullet_VisualTreeAsset =
                Resources.Load<VisualTreeAsset>("UI/Basic_Templates/PlayerCombatInfo/Bullet_Template");


        ConnctEvents();
    }

    private void OnDisable()
    {
        DisconnctEvents();

        panelRenderer.UnregisterUIReloadCallback(OnUIReloadCallback);
    }


    private void OnUIReloadCallback(PanelRenderer panelRenderer, VisualElement root)
    {
        ScreenSafeArea.RemoveUnSafeAreaFromUI(root);

        health_VisualElement = root.Q<VisualElement>("Health_VisualElement");
        bullets_VisualElement = root.Q<VisualElement>("Bullets_VisualElement");
        progress_Label = root.Q<Label>("Progress_Label");

        UI_Utilities.FixCombatInfoSize(health_VisualElement, bullets_VisualElement, progress_Label);

        health_List = new List<VisualElement>();
        FillHealth();

        bullets_List = new List<VisualElement>();
        FillBullets();

        OnLanguageChanged();
        OnFontSizeChanged();
    }



    #region Events Manager

    private void ConnctEvents()
    {
        EventsManager.OnLanguageChanged_Event += OnLanguageChanged;
        EventsManager.OnFontSizeChanged_Event += OnFontSizeChanged;

        EventsManager.OnHealthChanged_Event += UpdateHealthInUI;
        EventsManager.OnScoreChanged_Event += UpdateScoreInUI;
        EventsManager.OnBulletsChanged_Event += UpdateBulletsInUI;
    }

    private void DisconnctEvents()
    {
        EventsManager.OnLanguageChanged_Event -= OnLanguageChanged;
        EventsManager.OnFontSizeChanged_Event -= OnFontSizeChanged;

        EventsManager.OnHealthChanged_Event -= UpdateHealthInUI;
        EventsManager.OnScoreChanged_Event -= UpdateScoreInUI;
        EventsManager.OnBulletsChanged_Event -= UpdateBulletsInUI;
    }


    private void OnLanguageChanged()
    {
        #region Progress
        if (GameData.currentLevelData != null)
            progress_Label.text = PlayerData.Score + " / " + GameData.currentLevelData.ScoreNeeded;
        progress_Label.languageDirection =
            LanguageTextsData.languages[0].languageDirection;
        progress_Label.style.unityFont =
            LanguageTextsData.languages[0].font;
        #endregion
    }

    private void OnFontSizeChanged()
    {
        #region Progress
        progress_Label.style.fontSize =
            LanguageTextsData.fontSize_CategorySmall[SettingsData.currentFontSizeIndex];
        #endregion
    }


    public void UpdateHealthInUI()
    {
        for (int i = 0; i < health_List.Count; i++)
        {
            if (i < PlayerData.CurrentHealth)
            {
                health_List[i].style.display = DisplayStyle.Flex;
            }
            else
            {
                health_List[i].style.display = DisplayStyle.None;
            }
        }
    }

    public void UpdateBulletsInUI()
    {
        for (int i = 0; i < bullets_List.Count; i++)
        {
            if (i < PlayerData.CurrentBullets)
            {
                bullets_List[i].style.display = DisplayStyle.Flex;
            }
            else
            {
                bullets_List[i].style.display = DisplayStyle.None;
            }
        }
    }

    public void UpdateScoreInUI()
    {
        progress_Label.text = PlayerData.Score + " / " + GameData.currentLevelData.ScoreNeeded;
    }

    #endregion


    #region Utilities
    private void FillHealth()
    {
        for (int i = 0; i < AchievementsData.health; i++)
        {
            VisualElement health = health_VisualTreeAsset.Instantiate();
            UI_Utilities.FixCombatInfoItemSize(health);
            health_VisualElement.Add(health);
            health_List.Add(health);
        }
    }

    private void FillBullets()
    {
        for (int i = 0; i < AchievementsData.bullets; i++)
        {
            VisualElement bullet = bullet_VisualTreeAsset.Instantiate();
            UI_Utilities.FixCombatInfoItemSize(bullet);
            bullets_VisualElement.Add(bullet);
            bullets_List.Add(bullet);
        }
    }
    #endregion

}
