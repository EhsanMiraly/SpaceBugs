using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LevelsPage_Controller : MonoBehaviour
{
    Menu_UIConnector menu_UIConnector;

    VisualTreeAsset level_Template;
    List<VisualElement> levels_VisualElement_List;


    public void Initialize(Menu_UIConnector menu_UIConnector)
    {
        Levels_SaveSystem.Load_Levels();

        EventsManager.OnLanguageChanged_Event += OnLanguageChanged;
        EventsManager.OnFontSizeChanged_Event += OnFontSizeChanged;
        EventsManager.OnWinLevel_Event += OnWinLevel;
        EventsManager.OnLoseLevel_Event += OnLoseLevel;

        this.menu_UIConnector = menu_UIConnector;

        level_Template = Resources.Load<VisualTreeAsset>("UI/Basic_Templates/Level/Level_Template");
        levels_VisualElement_List = new List<VisualElement>();

        menu_UIConnector.back_TemplateContainer_InLevelsPage.
            RegisterCallback<ClickEvent>(OnBack_VisualElement_InLevelsPageSelected);

        FixUIElementsSize();

        FillLevelsScrollView();

        OnLanguageChanged();
        OnFontSizeChanged();

    }

    private void FixUIElementsSize()
    {
        int backButtonSize = Screen.width / 15;

        menu_UIConnector.back_TemplateContainer_InLevelsPage.style.width = backButtonSize;
        menu_UIConnector.back_TemplateContainer_InLevelsPage.style.height = backButtonSize;
    }

    private void OnDisable()
    {
        EventsManager.OnLanguageChanged_Event -= OnLanguageChanged;
        EventsManager.OnFontSizeChanged_Event -= OnFontSizeChanged;
        EventsManager.OnWinLevel_Event -= OnWinLevel;
        EventsManager.OnLoseLevel_Event -= OnLoseLevel;

        menu_UIConnector.back_TemplateContainer_InLevelsPage.
            UnregisterCallback<ClickEvent>(OnBack_VisualElement_InLevelsPageSelected);

        for (int i = 0; i < LevelsData.Levels.Length; i++)
        {
            levels_VisualElement_List[i].UnregisterCallback<ClickEvent>(OnLevelSelected);
        }
    }

    private void OnBack_VisualElement_InLevelsPageSelected(ClickEvent clickEvent)
    {
        menu_UIConnector.SwitchPage(menu_UIConnector.mainPage_VisualElement);
    }

    private void FillLevelsScrollView()
    {
        for (int i = 0; i < LevelsData.Levels.Length; i++)
        {
            VisualElement level_VisualElement = level_Template.Instantiate();

            level_VisualElement.style.width = Screen.width / 10;
            level_VisualElement.style.height = Screen.width / 10;

            level_VisualElement.style.marginLeft = Screen.width / 100;
            level_VisualElement.style.marginTop = Screen.width / 100;
            level_VisualElement.style.marginRight = Screen.width / 100;
            level_VisualElement.style.marginBottom = Screen.width / 100;

            Label label = level_VisualElement.Q<Label>("LevelNumber_Label");
            label.text = "Level " + (i + 1);
            label.name = "" + (i + 1);

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

            menu_UIConnector.levels_ScrollView.Add(level_VisualElement);

            levels_VisualElement_List.Add(level_VisualElement);
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

    private void OnLevelSelected(ClickEvent clickEvent)
    {
        VisualElement visualElement = clickEvent.currentTarget as VisualElement;
        string name = visualElement.Q<Label>().name;
        int levelNumber = int.Parse(name);

        EventsManager.InvokeOnStartLevel(this, new GameState_EventArgs(levelNumber));

        menu_UIConnector.menu_VisualElement.style.display = DisplayStyle.Flex;
        menu_UIConnector.pageHolder_VisualElement.style.display = DisplayStyle.None;
    }


    #region Events Handler

    private void OnLanguageChanged()
    {
        for (int i = 0; i < levels_VisualElement_List.Count; i++)
        {
            Label label = levels_VisualElement_List[i].Q<Label>("" + (i + 1));
            label.text = LanguageTextsData.level[SettingsData.currentLanguageIndex] + (i + 1);
            label.languageDirection =
                LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
            label.style.unityFont =
                LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        }
    }

    private void OnFontSizeChanged()
    {
        for (int i = 0; i < levels_VisualElement_List.Count; i++)
        {
            Label label = levels_VisualElement_List[i].Q<Label>("" + (i + 1));
            label.style.fontSize =
                LanguageTextsData.fontSize_CategorySmall[SettingsData.currentFontSizeIndex];
        }

    }

    #endregion

}
