using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LevelsPage_Controller : MonoBehaviour
{
    Menu_UIConnector menu_UIConnector;

    VisualTreeAsset level_Template;
    List<VisualElement> levels_VisualElement;

    public void Initialize(Menu_UIConnector menu_UIConnector)
    {

        EventsManager.OnLanguageChanged_Event += OnLanguageChanged;
        EventsManager.OnFontSizeChanged_Event += OnFontSizeChanged;


        this.menu_UIConnector = menu_UIConnector;

        level_Template = Resources.Load<VisualTreeAsset>("UI/Basic_Templates/Level/Level_Template");
        levels_VisualElement = new List<VisualElement>();

        menu_UIConnector.back_VisualElement_InLevelsPage.
            RegisterCallback<ClickEvent>(OnBack_VisualElement_InLevelsPageSelected);

        for (int i = 0; i < SettingsData.NumberOfLevels; i++)
        {
            VisualElement level_VisualElement = level_Template.Instantiate();

            level_VisualElement.style.width = Screen.width / SettingsData.Level_Template_Size;
            level_VisualElement.style.height = Screen.width / SettingsData.Level_Template_Size;

            level_VisualElement.style.paddingLeft = Screen.width / SettingsData.Level_Template_Padding;
            level_VisualElement.style.paddingTop = Screen.width / SettingsData.Level_Template_Padding;
            level_VisualElement.style.paddingRight = Screen.width / SettingsData.Level_Template_Padding;
            level_VisualElement.style.paddingBottom = Screen.width / SettingsData.Level_Template_Padding;

            Label label = level_VisualElement.Q<Label>("LevelNumber_Label");
            label.text = "Level " + (i + 1);
            label.name = "" + (i + 1);
            level_VisualElement.RegisterCallback<ClickEvent>(OnLevelSelected);

            menu_UIConnector.levelsHolder_VisualElement.Add(level_VisualElement);

            levels_VisualElement.Add(level_VisualElement);
        }

        OnLanguageChanged();
        OnFontSizeChanged();

    }

    private void OnDisable()
    {
        EventsManager.OnLanguageChanged_Event -= OnLanguageChanged;
        EventsManager.OnFontSizeChanged_Event -= OnFontSizeChanged;
    }

    private void OnBack_VisualElement_InLevelsPageSelected(ClickEvent clickEvent)
    {
        menu_UIConnector.SwitchPage(menu_UIConnector.mainPage_VisualElement);
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


    private void OnLanguageChanged()
    {
        for (int i = 0; i < levels_VisualElement.Count; i++)
        {
            Label label = levels_VisualElement[i].Q<Label>("" + (i + 1));
            label.text = LanguageTextsData.level[SettingsData.currentLanguageIndex] + (i + 1);
            label.languageDirection =
                LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
            label.style.unityFont =
                LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        }
    }

    private void OnFontSizeChanged()
    {
        for (int i = 0; i < levels_VisualElement.Count; i++)
        {
            Label label = levels_VisualElement[i].Q<Label>("" + (i + 1));
            label.style.fontSize =
                LanguageTextsData.fontSize_CategorySmall[SettingsData.currentFontSizeIndex];
        }

    }

}
