using Unity.AppUI.MVVM;
using UnityEngine;
using UnityEngine.UIElements;

public class MenuController : MonoBehaviour
{
    private int numberOfLevels = 20;//Move

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
    VisualElement levelsHolder_VisualElement;
    //public stattic
    VisualTreeAsset level_Template;
    int level_Template_Size = 10;
    int level_Template_Padding = 100;

    VisualElement settingsPage_VisualElement;


    private void Awake()
    {
        ConnectUI();
        AddFunctionality();
        InitialUI();
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

    }

    private void AddFunctionality()
    {
        menu_VisualElement.RegisterCallback<ClickEvent>(OnMenuSelected);

        resume_Button.RegisterCallback<ClickEvent>(clickEvent =>
        {
            menu_VisualElement.style.display = DisplayStyle.Flex;
            pageHolder_VisualElement.style.display = DisplayStyle.None;
            GameState_EventManager.InvokeOnResumeLevel(this,
            new GameState_EventArgs(true, false, PlayerData.CurrentLevelNumber));
        });

        levels_Button.RegisterCallback<ClickEvent>(clickEvent =>
        {
            SwitchPage(levelsPage_VisualElement);
            GameState_EventManager.InvokeOnStopLevel(this,
            new GameState_EventArgs(false, true, 0));
        });

        settings_Button.RegisterCallback<ClickEvent>(clickEvent => SwitchPage(settingsPage_VisualElement));
        exit_Button.RegisterCallback<ClickEvent>(clickEvent => Application.Quit());
    }

    private void OnLevelSelected(ClickEvent clickEvent)
    {
        VisualElement visualElement = clickEvent.currentTarget as VisualElement;
        string name = visualElement.Q<Button>().name;
        int levelNumber = int.Parse(name);

        GameState_EventManager.InvokeOnStartLevel(this,
        new GameState_EventArgs(true, false, levelNumber));

        menu_VisualElement.style.display = DisplayStyle.Flex;
        pageHolder_VisualElement.style.display = DisplayStyle.None;
    }

    private void InitialUI()
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

        GameState_EventManager.InvokeOnPauseLevel(this,
        new GameState_EventArgs(true, true, PlayerData.CurrentLevelNumber));

        resume_Button.style.display = DisplayStyle.Flex;
        SwitchPage(mainPage_VisualElement);//Change Later?
    }


    private void SwitchPage(VisualElement page)
    {
        mainPage_VisualElement.style.display = DisplayStyle.None;
        levelsPage_VisualElement.style.display = DisplayStyle.None;
        settingsPage_VisualElement.style.display = DisplayStyle.None;

        page.style.display = DisplayStyle.Flex;
    }

}
