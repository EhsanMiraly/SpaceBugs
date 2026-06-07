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
    public Button inventoryShop_Button;
    public Button settings_Button;
    public Button exit_Button;

    //LevelsPage
    public VisualElement levelsPage_VisualElement;
    public VisualElement back_TemplateContainer_InLevelsPage;
    public VisualElement levelsHolder_VisualElement;
    public ScrollView levels_ScrollView;

    //InventoryShopPage
    public VisualElement inventoryShopPage_VisualElement;
    public Label inventory_Label;
    public ScrollView inventory_ScrollView;
    public Label currencyAmount_Label;
    public Label shop_Label;
    public ScrollView shop_ScrollView;
    public VisualElement buy_TemplateContainer;
    public Label buy_Label;
    public VisualElement exit_TemplateContainer;
    public Label exit_Label;




    //SettingsPage
    public VisualElement settingsPage_VisualElement;
    public VisualElement back_TemplateContainer_InSettingsPage;
    public VisualElement settingsHolder_VisualElement;
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
        inventoryShop_Button = mainPage_VisualElement.Q<Button>("Shop_Button");
        settings_Button = mainPage_VisualElement.Q<Button>("Settings_Button");
        exit_Button = mainPage_VisualElement.Q<Button>("Exit_Button");

        //LevelsPage
        levelsPage_VisualElement = root.Q<VisualElement>("LevelsPage_VisualElement");
        back_TemplateContainer_InLevelsPage = levelsPage_VisualElement.Q<VisualElement>("Back_TemplateContainer");
        levelsHolder_VisualElement = levelsPage_VisualElement.Q<VisualElement>("LevelsHolder_VisualElement");
        levels_ScrollView = levelsPage_VisualElement.Q<ScrollView>("Levels_ScrollView");
        ScrollViewController.InitializeScrollView(levels_ScrollView);

        //InventoryShopPage
        inventoryShopPage_VisualElement = root.Q<VisualElement>("InventoryShopPage_VisualElement");
        inventory_Label = inventoryShopPage_VisualElement.Q<Label>("Inventory_Label");
        inventory_ScrollView = inventoryShopPage_VisualElement.Q<ScrollView>("Inventory_ScrollView");
        currencyAmount_Label = inventoryShopPage_VisualElement.Q<Label>("CurrencyAmount_Label");
        shop_Label = inventoryShopPage_VisualElement.Q<Label>("Shop_Label");
        shop_ScrollView = inventoryShopPage_VisualElement.Q<ScrollView>("Shop_ScrollView");
        buy_TemplateContainer = inventoryShopPage_VisualElement.Q<VisualElement>("Buy_TemplateContainer");
        buy_Label = inventoryShopPage_VisualElement.Q<Label>("Buy_Label");
        exit_TemplateContainer = inventoryShopPage_VisualElement.Q<VisualElement>("Exit_TemplateContainer");
        exit_Label = inventoryShopPage_VisualElement.Q<Label>("Exit_Label");

        //SettingsPage
        settingsPage_VisualElement = root.Q<VisualElement>("SettingsPage_VisualElement");
        back_TemplateContainer_InSettingsPage = settingsPage_VisualElement.Q<VisualElement>("Back_TemplateContainer");
        settingsHolder_VisualElement = settingsPage_VisualElement.Q<VisualElement>("SettingsHolder_VisualElement");
        settings_ScrollView = settingsPage_VisualElement.Q<ScrollView>("Settings_ScrollView");
        ScrollViewController.InitializeScrollView(settings_ScrollView);


        //Add Functionality To menu_VisualElement
        menu_VisualElement.RegisterCallback<ClickEvent>(OnMenuSelected);

        GetComponent<MainPage_Controller>().Initialize(this);
        GetComponent<LevelsPage_Controller>().Initialize(this);
        GetComponent<InventoryShopPage_Controller>().Initialize(this);
        GetComponent<SettingsPage_Controller>().Initialize(this);

        InitialPage();
    }

    private void OnDisable()
    {
        menu_VisualElement.UnregisterCallback<ClickEvent>(OnMenuSelected);
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

        EventsManager.InvokeOnPauseLevel();

        resume_Button.style.display = DisplayStyle.Flex;
        SwitchPage(mainPage_VisualElement);
    }

    public void SwitchPage(VisualElement page)
    {
        mainPage_VisualElement.style.display = DisplayStyle.None;
        levelsPage_VisualElement.style.display = DisplayStyle.None;
        inventoryShopPage_VisualElement.style.display = DisplayStyle.None;
        settingsPage_VisualElement.style.display = DisplayStyle.None;

        page.style.display = DisplayStyle.Flex;
    }

}
