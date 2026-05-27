using UnityEngine;
using UnityEngine.UIElements;

public class LevelsPage_Controller : MonoBehaviour
{
    Menu_UIConnector menu_UIConnector;

    VisualTreeAsset level_Template;

    public void Initialize(Menu_UIConnector menu_UIConnector)
    {
        this.menu_UIConnector = menu_UIConnector;

        level_Template = Resources.Load<VisualTreeAsset>("UI/Level_Template");

        menu_UIConnector.backButton_InLevelsPage_Template.
            RegisterCallback<ClickEvent>(OnBackButton_InLevelsPageSelected);

        for (int i = 0; i < SettingsData.NumberOfLevels; i++)
        {
            TemplateContainer templateContainer = level_Template.Instantiate();

            templateContainer.style.width = Screen.width / SettingsData.Level_Template_Size;
            templateContainer.style.height = Screen.width / SettingsData.Level_Template_Size;

            templateContainer.style.paddingLeft = Screen.width / SettingsData.Level_Template_Padding;
            templateContainer.style.paddingTop = Screen.width / SettingsData.Level_Template_Padding;
            templateContainer.style.paddingRight = Screen.width / SettingsData.Level_Template_Padding;
            templateContainer.style.paddingBottom = Screen.width / SettingsData.Level_Template_Padding;

            Button button = templateContainer.Q<Button>("LevelNumber_Button");
            button.text = "Level " + (i + 1);
            button.name = "" + (i + 1);
            button.RegisterCallback<ClickEvent>(OnLevelSelected);

            menu_UIConnector.levelsHolder_VisualElement.Add(templateContainer);
        }
    }

    private void OnBackButton_InLevelsPageSelected(ClickEvent clickEvent)
    {
        menu_UIConnector.SwitchPage(menu_UIConnector.mainPage_VisualElement);
    }

    private void OnLevelSelected(ClickEvent clickEvent)
    {
        VisualElement visualElement = clickEvent.currentTarget as VisualElement;
        string name = visualElement.Q<Button>().name;
        int levelNumber = int.Parse(name);

        GameState_EventManager.InvokeOnStartLevel(this, new GameState_EventArgs(levelNumber));

        menu_UIConnector.menu_VisualElement.style.display = DisplayStyle.Flex;
        menu_UIConnector.pageHolder_VisualElement.style.display = DisplayStyle.None;
    }
}
