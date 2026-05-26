#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;
using UnityEngine.UIElements;

public class MenuController : MonoBehaviour
{
    private int numberOfLevels = 2;//Move To ...

    UIDocument uIDocument;
    VisualElement root;

    VisualElement menu_VisualElement;
    VisualElement pageHolder_VisualElement;

    VisualElement mainPage_VisualElement;
    Button resume_Button;
    Button levels_Button;
    Button settings_Button;
    Button exit_Button;

    VisualElement levelsPage_VisualElement;
    VisualElement backButton_InLevelsPage_Template;
    VisualElement levelsHolder_VisualElement;
    //public stattic
    VisualTreeAsset level_Template;
    int level_Template_Size = 10;
    int level_Template_Padding = 100;

    VisualElement settingsPage_VisualElement;
    VisualElement backButton_InSettingsPage_Template;
    ScrollView settings_ScrollView;
    VisualTreeAsset sound_VisualTreeAsset;


    public void Initialize()
    {
        sound_VisualTreeAsset = Resources.Load<VisualTreeAsset>("UI/Settings_Templates/Sound_Template");

        ConnectUI();
        AddFunctionality();
        InitialPage();
    }



    private void ConnectUI()
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
        level_Template = Resources.Load<VisualTreeAsset>("UI/Level_Template");

        for (int i = 0; i < numberOfLevels; i++)
        {
            TemplateContainer templateContainer = level_Template.Instantiate();

            templateContainer.style.width = Screen.width / level_Template_Size;
            templateContainer.style.height = Screen.width / level_Template_Size;

            templateContainer.style.paddingLeft = Screen.width / level_Template_Padding;
            templateContainer.style.paddingTop = Screen.width / level_Template_Padding;
            templateContainer.style.paddingRight = Screen.width / level_Template_Padding;
            templateContainer.style.paddingBottom = Screen.width / level_Template_Padding;

            Button button = templateContainer.Q<Button>("LevelNumber_Button");
            button.text = "Level " + (i + 1);
            button.name = "" + (i + 1);
            button.RegisterCallback<ClickEvent>(OnLevelSelected);

            levelsHolder_VisualElement.Add(templateContainer);
        }

        settingsPage_VisualElement = root.Q<VisualElement>("SettingsPage_VisualElement");
        backButton_InSettingsPage_Template = settingsPage_VisualElement.Q<VisualElement>("BackButton_Template");
        settings_ScrollView = settingsPage_VisualElement.Q<ScrollView>("Settings_ScrollView");

        VisualElement visualElement = sound_VisualTreeAsset.Instantiate();
        visualElement.style.width = Length.Percent(100);
        visualElement.style.height = 300;
        settings_ScrollView.Add(visualElement);



    }

    private void OnLevelSelected(ClickEvent clickEvent)
    {
        VisualElement visualElement = clickEvent.currentTarget as VisualElement;
        string name = visualElement.Q<Button>().name;
        int levelNumber = int.Parse(name);

        GameState_EventManager.InvokeOnStartLevel(this, new GameState_EventArgs(levelNumber));

        menu_VisualElement.style.display = DisplayStyle.Flex;
        pageHolder_VisualElement.style.display = DisplayStyle.None;
    }

    private void AddFunctionality()
    {
        menu_VisualElement.RegisterCallback<ClickEvent>(OnMenuSelected);

        resume_Button.RegisterCallback<ClickEvent>(OnResume_ButtonSelected);

        levels_Button.RegisterCallback<ClickEvent>(OnLevels_ButtonSelected);
        backButton_InLevelsPage_Template.RegisterCallback<ClickEvent>(OnBackButton_InLevelsPageSelected);

        settings_Button.RegisterCallback<ClickEvent>(OnSettings_ButtonSelected);
        backButton_InSettingsPage_Template.RegisterCallback<ClickEvent>(OnBackButton_InSettingsPageSelected);

        exit_Button.RegisterCallback<ClickEvent>(OnExit_ButtonSelected);

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
        SwitchPage(mainPage_VisualElement);//Change Later?
    }

    private void OnResume_ButtonSelected(ClickEvent clickEvent)
    {
        menu_VisualElement.style.display = DisplayStyle.Flex;
        pageHolder_VisualElement.style.display = DisplayStyle.None;
        GameState_EventManager.InvokeOnResumeLevel(this, new GameState_EventArgs(GameData.CurrentLevelNumber));
    }

    private void OnLevels_ButtonSelected(ClickEvent clickEvent)
    {
        resume_Button.style.display = DisplayStyle.None;
        SwitchPage(levelsPage_VisualElement);
        GameState_EventManager.InvokeOnStopLevel(this, new GameState_EventArgs(0));
    }

    private void OnBackButton_InLevelsPageSelected(ClickEvent clickEvent)
    {
        SwitchPage(mainPage_VisualElement);
    }

    private void OnSettings_ButtonSelected(ClickEvent clickEvent)
    {
        SwitchPage(settingsPage_VisualElement);
    }

    private void OnBackButton_InSettingsPageSelected(ClickEvent clickEvent)
    {
        SwitchPage(mainPage_VisualElement);
    }

    private void OnExit_ButtonSelected(ClickEvent clickEvent)
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }


    private void SwitchPage(VisualElement page)
    {
        mainPage_VisualElement.style.display = DisplayStyle.None;
        levelsPage_VisualElement.style.display = DisplayStyle.None;
        settingsPage_VisualElement.style.display = DisplayStyle.None;

        page.style.display = DisplayStyle.Flex;
    }

}
