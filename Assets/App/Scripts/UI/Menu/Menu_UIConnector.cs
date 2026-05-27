using UnityEngine;
using UnityEngine.UIElements;

public class Menu_UIConnector : MonoBehaviour
{
    UIDocument uIDocument;
    VisualElement root;

    public VisualElement menu_VisualElement;
    public VisualElement pageHolder_VisualElement;

    //MainPage
    public VisualElement mainPage_VisualElement;
    public Button resume_Button;
    public Button levels_Button;
    public Button settings_Button;
    public Button exit_Button;

    //LevelsPage
    public VisualElement levelsPage_VisualElement;
    public VisualElement backButton_InLevelsPage_Template;
    public VisualElement levelsHolder_VisualElement;

    //SettingsPage
    public VisualElement settingsPage_VisualElement;
    public VisualElement backButton_InSettingsPage_Template;
    public ScrollView settings_ScrollView;



    public void Initialize()
    {
        uIDocument = GetComponent<UIDocument>();
        root = uIDocument.rootVisualElement;

        menu_VisualElement = root.Q<VisualElement>("Menu_VisualElement");
        pageHolder_VisualElement = root.Q<VisualElement>("PageHolder_VisualElement");

        mainPage_VisualElement = root.Q<VisualElement>("MainPage_VisualElement");
        resume_Button = mainPage_VisualElement.Q<Button>("Resume_Button");
        levels_Button = mainPage_VisualElement.Q<Button>("Levels_Button");
        settings_Button = mainPage_VisualElement.Q<Button>("Settings_Button");
        exit_Button = mainPage_VisualElement.Q<Button>("Exit_Button");

        levelsPage_VisualElement = root.Q<VisualElement>("LevelsPage_VisualElement");
        backButton_InLevelsPage_Template = levelsPage_VisualElement.Q<VisualElement>("BackButton_Template");
        levelsHolder_VisualElement = levelsPage_VisualElement.Q<VisualElement>("LevelsHolder_VisualElement");

        settingsPage_VisualElement = root.Q<VisualElement>("SettingsPage_VisualElement");
        backButton_InSettingsPage_Template = settingsPage_VisualElement.Q<VisualElement>("BackButton_Template");
        settings_ScrollView = settingsPage_VisualElement.Q<ScrollView>("Settings_ScrollView");


        //Add Functionality To menu_VisualElement
        menu_VisualElement.RegisterCallback<ClickEvent>(OnMenuSelected);

        GetComponent<MainPage_Controller>().Initialize(this);
        GetComponent<LevelsPage_Controller>().Initialize(this);
        GetComponent<SettingsPage_Controller>().Initialize(this);

        InitialPage();
    }

    public void InitialPage()
    {
        menu_VisualElement.style.display = DisplayStyle.None;

        pageHolder_VisualElement.style.display = DisplayStyle.Flex;
        resume_Button.style.display = DisplayStyle.None;

        SwitchPage(mainPage_VisualElement);
    }

    private void OnMenuSelected(ClickEvent clickEvent)
    {
        menu_VisualElement.style.display = DisplayStyle.None;
        pageHolder_VisualElement.style.display = DisplayStyle.Flex;

        GameState_EventManager.InvokeOnPauseLevel(this, new GameState_EventArgs(GameData.CurrentLevelNumber));

        resume_Button.style.display = DisplayStyle.Flex;
        SwitchPage(mainPage_VisualElement);
    }

    public void SwitchPage(VisualElement page)
    {
        mainPage_VisualElement.style.display = DisplayStyle.None;
        levelsPage_VisualElement.style.display = DisplayStyle.None;
        settingsPage_VisualElement.style.display = DisplayStyle.None;

        page.style.display = DisplayStyle.Flex;
    }

}
