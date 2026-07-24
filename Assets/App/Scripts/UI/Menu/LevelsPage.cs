using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LevelsPage : MonoBehaviour
{
    PanelRenderer panelRenderer;
    Menu menu;


    private VisualElement levelsPage_VisualElement;
    private VisualElement back_TemplateContainer;
    private VisualElement levelsHolder_VisualElement;
    private ScrollView levels_ScrollView;

    private VisualTreeAsset levelsHolderHorizontal_VisulaElement_Template;
    private VisualTreeAsset level_Template;

    List<VisualElement> levels_VisualElement_List;


    private void OnEnable()
    {
        Levels_SaveSystem.Load_Levels();

        panelRenderer = GetComponent<PanelRenderer>();
        panelRenderer.RegisterUIReloadCallback(OnUIReloadCallback);
        menu = GetComponent<Menu>();
    }

    private void OnDisable()
    {
        RemoveFunctionality();

        DisconnctEvents();

        panelRenderer.UnregisterUIReloadCallback(OnUIReloadCallback);
    }


    private void OnUIReloadCallback(PanelRenderer panelRenderer, VisualElement root)
    {
        levelsPage_VisualElement = root.Q<VisualElement>("LevelsPage_VisualElement");
        back_TemplateContainer = levelsPage_VisualElement.Q<VisualElement>("Back_TemplateContainer");
        UI_Utilities.FixBackButtonSize(back_TemplateContainer);
        levelsHolder_VisualElement = levelsPage_VisualElement.Q<VisualElement>("LevelsHolder_VisualElement");
        levels_ScrollView = levelsPage_VisualElement.Q<ScrollView>("Levels_ScrollView");
        ScrollViewController.InitializeScrollView(levels_ScrollView);

        levelsHolderHorizontal_VisulaElement_Template =
            Resources.Load<VisualTreeAsset>(
            "UI/BasicElements/LevelsHolderHorizontal_VisulaElement/LevelsHolderHorizontal_VisulaElement_Template"
            );
        level_Template = Resources.Load<VisualTreeAsset>("UI/Basic_Templates/Level/Level_Template");

        levels_VisualElement_List = new List<VisualElement>();


        FillLevelsScrollView();

        OnLanguageChanged();
        OnFontSizeChanged();

        AddFunctionality();
        ConnctEvents();
    }



    #region Functionality
    private void AddFunctionality()
    {
        back_TemplateContainer.RegisterCallback<ClickEvent>(OnBackSelected);
    }

    private void RemoveFunctionality()
    {
        back_TemplateContainer.UnregisterCallback<ClickEvent>(OnBackSelected);

        for (int i = 0; i < LevelsData.Levels.Length; i++)
        {
            levels_VisualElement_List[i].UnregisterCallback<ClickEvent>(OnLevelSelected);
        }
    }


    private void OnBackSelected(ClickEvent clickEvent)
    {
        menu.SwitchPage(menu.mainPage_VisualElement);
    }

    private void OnLevelSelected(ClickEvent clickEvent)
    {
        VisualElement visualElement = clickEvent.currentTarget as VisualElement;
        string name = visualElement.Q<Label>("LevelNumber_Label").text;
        int levelNumber = int.Parse(name.Split(" ")[1]);

        EventsManager.InvokeOnStartLevel(this, new GameState_EventArgs(levelNumber));

        menu.SwitchPage(menu.menu_VisualElement_TemplateContainer);
    }


    #endregion


    #region Events Manager

    private void ConnctEvents()
    {
        EventsManager.OnLanguageChanged_Event += OnLanguageChanged;
        EventsManager.OnFontSizeChanged_Event += OnFontSizeChanged;

        EventsManager.OnWinLevel_Event += OnWinLevel;
        EventsManager.OnLoseLevel_Event += OnLoseLevel;
    }

    private void DisconnctEvents()
    {
        EventsManager.OnLanguageChanged_Event -= OnLanguageChanged;
        EventsManager.OnFontSizeChanged_Event -= OnFontSizeChanged;

        EventsManager.OnWinLevel_Event -= OnWinLevel;
        EventsManager.OnLoseLevel_Event -= OnLoseLevel;
    }


    private void OnLanguageChanged()
    {
        Label levelNumber_Label;
        Label scoresNeeded_Label;
        Label reward_Label;
        Label currencyAmount_Label;

        for (int i = 0; i < levels_VisualElement_List.Count; i++)
        {
            levelNumber_Label = levels_VisualElement_List[i].Q<Label>("LevelNumber_Label");
            scoresNeeded_Label = levels_VisualElement_List[i].Q<Label>("ScoresNeeded_Label");
            reward_Label = levels_VisualElement_List[i].Q<Label>("Reward_Label");
            currencyAmount_Label = levels_VisualElement_List[i].Q<Label>("CurrencyAmount_Label");

            levelNumber_Label.text = LanguageTextsData.level[SettingsData.currentLanguageIndex] + (i + 1);//+ " "
            levelNumber_Label.languageDirection =
                LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
            levelNumber_Label.style.unityFont =
                LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;

            scoresNeeded_Label.text = LanguageTextsData.scoresNeeded[SettingsData.currentLanguageIndex] +
                ((i + 1) * 100);
            scoresNeeded_Label.languageDirection =
                LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
            scoresNeeded_Label.style.unityFont =
                LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;

            reward_Label.text = LanguageTextsData.reward[SettingsData.currentLanguageIndex];
            reward_Label.languageDirection =
                LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
            reward_Label.style.unityFont =
                LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;

            currencyAmount_Label.text = "" + LevelsData.Levels[i].Coins;
            currencyAmount_Label.languageDirection =
                LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
            currencyAmount_Label.style.unityFont =
                LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;


        }
    }

    private void OnFontSizeChanged()
    {
        Label levelNumber_Label;
        Label scoresNeeded_Label;
        Label reward_Label;
        Label currencyAmount_Label;

        for (int i = 0; i < levels_VisualElement_List.Count; i++)
        {
            levelNumber_Label = levels_VisualElement_List[i].Q<Label>("LevelNumber_Label");
            scoresNeeded_Label = levels_VisualElement_List[i].Q<Label>("ScoresNeeded_Label");
            reward_Label = levels_VisualElement_List[i].Q<Label>("Reward_Label");
            currencyAmount_Label = levels_VisualElement_List[i].Q<Label>("CurrencyAmount_Label");

            levelNumber_Label.style.fontSize =
                LanguageTextsData.fontSize_CategoryAverage[SettingsData.currentFontSizeIndex];

            scoresNeeded_Label.style.fontSize =
                LanguageTextsData.fontSize_CategorySmall[SettingsData.currentFontSizeIndex];
            reward_Label.style.fontSize =
                LanguageTextsData.fontSize_CategorySmall[SettingsData.currentFontSizeIndex];
            currencyAmount_Label.style.fontSize =
                LanguageTextsData.fontSize_CategorySmall[SettingsData.currentFontSizeIndex];
        }
    }


    public void OnWinLevel()
    {
        if (GameData.CurrentLevelNumber < LevelsData.Levels.Length)
        {
            LevelsData.Levels[GameData.CurrentLevelNumber].IsOpen = true;
            VisualElement lock_VisualElement =
                levels_VisualElement_List[GameData.CurrentLevelNumber].Q<VisualElement>("Lock_VisualElement");
            lock_VisualElement.style.display = DisplayStyle.None;
            levels_VisualElement_List[GameData.CurrentLevelNumber].RegisterCallback<ClickEvent>(OnLevelSelected);
        }

        LevelsData.Levels[GameData.CurrentLevelNumber - 1].Progress = 100;
        AchievementsData.coins += LevelsData.Levels[GameData.CurrentLevelNumber - 1].Coins;

        Levels_SaveSystem.Save_Levels();
        Achievements_SaveSystem.Save_Achievements();
    }


    public void OnLoseLevel()
    {
        if (GameData.currentLevelProgress > LevelsData.Levels[GameData.CurrentLevelNumber - 1].Progress)
        {
            LevelsData.Levels[GameData.CurrentLevelNumber - 1].Progress = GameData.currentLevelProgress;
        }


        //add stars based on progress
        Levels_SaveSystem.Save_Levels();
        //Achievements_SaveSystem.Save_Achievements();
    }

    #endregion


    private void FillLevelsScrollView()
    {
        VisualElement levelsHolderHorizontal_VisulaElement =
                                levelsHolderHorizontal_VisulaElement_Template.Instantiate();

        for (int i = 0; i < LevelsData.Levels.Length; i++)
        {
            if (i % 3 == 0)
            {
                levelsHolderHorizontal_VisulaElement =
                                levelsHolderHorizontal_VisulaElement_Template.Instantiate();

                UI_Utilities.FixLevelsHolderSize(levelsHolderHorizontal_VisulaElement);

                levels_ScrollView.Add(levelsHolderHorizontal_VisulaElement);
            }

            VisualElement level_VisualElement = level_Template.Instantiate();

            UI_Utilities.FixLevelSize(level_VisualElement);

            /*
            Label label = level_VisualElement.Q<Label>("LevelNumber_Label");
            label.text = "Level " + (i + 1);
            label.name = "" + (i + 1);
            */

            VisualElement lock_VisualElement = level_VisualElement.Q<VisualElement>("Lock_VisualElement");
            if (LevelsData.Levels[i].IsOpen)
            {
                lock_VisualElement.style.display = DisplayStyle.None;
                level_VisualElement.RegisterCallback<ClickEvent>(OnLevelSelected);
            }
            else
            {
                lock_VisualElement.style.display = DisplayStyle.Flex;
            }

            levelsHolderHorizontal_VisulaElement.
                Q<VisualElement>("Parent_VisualElement").Add(level_VisualElement);

            levels_VisualElement_List.Add(level_VisualElement);
        }
    }

}
