using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InventoryShopPage : MonoBehaviour
{
    PanelRenderer panelRenderer;
    Menu menu;


    private VisualElement inventoryShopPage_VisualElement;

    #region Inventory
    private Label inventory_Label;
    private ScrollView inventory_ScrollView;
    private VisualElement watchAd_TemplateContainer;
    private Label watchAd_Label;
    private Label reward_Label;
    #endregion

    #region Shop
    private Label shop_Label;
    private ScrollView shop_ScrollView;
    private Label currencyAmount_Label;
    private VisualElement buy_TemplateContainer;
    private Label buy_Label;
    private VisualElement exit_TemplateContainer;
    private Label exit_Label;
    #endregion


    VisualTreeAsset inventoryShopItem_Template;
    List<VisualElement> inventoryShopItems_List;
    int currentVisualElementIndex = -1;



    private void OnEnable()
    {
        InventoryShop_SaveSystem.Load_InventoryShopItems();
        inventoryShopItem_Template =
            Resources.Load<VisualTreeAsset>("UI/Basic_Templates/InventoryShopItem/InventoryShopItem_Template");
        inventoryShopItems_List = new List<VisualElement>();

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
        inventoryShopPage_VisualElement = root.Q<VisualElement>("InventoryShopPage_VisualElement");

        #region Inventory
        inventory_Label = inventoryShopPage_VisualElement.Q<Label>("Inventory_Label");
        inventory_ScrollView = inventoryShopPage_VisualElement.Q<ScrollView>("Inventory_ScrollView");
        watchAd_TemplateContainer = inventoryShopPage_VisualElement.Q<VisualElement>("WatchAd_TemplateContainer");
        watchAd_Label = inventoryShopPage_VisualElement.Q<Label>("WatchAd_Label");
        reward_Label = inventoryShopPage_VisualElement.Q<Label>("Reward_Label");
        #endregion

        #region Shop
        shop_Label = inventoryShopPage_VisualElement.Q<Label>("Shop_Label");
        shop_ScrollView = inventoryShopPage_VisualElement.Q<ScrollView>("Shop_ScrollView");
        currencyAmount_Label = inventoryShopPage_VisualElement.Q<Label>("CurrencyAmount_Label");
        buy_TemplateContainer = inventoryShopPage_VisualElement.Q<VisualElement>("Buy_TemplateContainer");
        buy_Label = inventoryShopPage_VisualElement.Q<Label>("Buy_Label");
        exit_TemplateContainer = inventoryShopPage_VisualElement.Q<VisualElement>("Exit_TemplateContainer");
        exit_Label = inventoryShopPage_VisualElement.Q<Label>("Exit_Label");
        #endregion

        RmoveCurrentItemSelected();

        ScrollViewController.InitializeScrollView(inventory_ScrollView);

        ScrollViewController.InitializeScrollView(shop_ScrollView);

        FillScrollViews();


        AddFunctionality();
        ConnctEvents();

        OnLanguageChanged();
        OnFontSizeChanged();
    }




    #region Functionality
    private void AddFunctionality()
    {
        watchAd_TemplateContainer.
            RegisterCallback<ClickEvent>(OnWatchAd_TemplateContainerSelected);

        buy_TemplateContainer.
             RegisterCallback<ClickEvent>(OnBuy_TemplateContainerSelected);
        exit_TemplateContainer.
             RegisterCallback<ClickEvent>(OnExit_TemplateContainerSelected);
    }

    private void RemoveFunctionality()
    {
        watchAd_TemplateContainer.
            UnregisterCallback<ClickEvent>(OnWatchAd_TemplateContainerSelected);

        buy_TemplateContainer.
            UnregisterCallback<ClickEvent>(OnBuy_TemplateContainerSelected);
        exit_TemplateContainer.
            UnregisterCallback<ClickEvent>(OnExit_TemplateContainerSelected);

        for (int i = 0; i < inventoryShopItems_List.Count; i++)
        {
            inventoryShopItems_List[i].UnregisterCallback<ClickEvent>(OnItemSelected);
        }
    }


    private void OnWatchAd_TemplateContainerSelected(ClickEvent clickEvent)
    {
        Debug.Log("Watch Ad.");
    }


    private void OnBuy_TemplateContainerSelected(ClickEvent clickEvent)
    {
        if (currentVisualElementIndex == -1)
        {
            return;
        }

        if (AchievementsData.coins >= InventoryShopData.InventoryShopItems[currentVisualElementIndex].Price)
        {
            inventoryShopItems_List[currentVisualElementIndex].UnregisterCallback<ClickEvent>(OnItemSelected);
            inventory_ScrollView.Add(inventoryShopItems_List[currentVisualElementIndex]);

            AchievementsData.coins -= InventoryShopData.InventoryShopItems[currentVisualElementIndex].Price;
            InventoryShopData.InventoryShopItems[currentVisualElementIndex].IsBought = true;
            if (InventoryShopData.InventoryShopItems[currentVisualElementIndex].ItemName[0] == "Bullet")
            {
                AchievementsData.bullets++;
            }
            else if (InventoryShopData.InventoryShopItems[currentVisualElementIndex].ItemName[0] == "Health")
            {
                AchievementsData.health++;
            }

            Achievements_SaveSystem.Save_Achievements();
            InventoryShop_SaveSystem.Save_InventoryShopItems();

            RmoveCurrentItemSelected();

            OnCurrencyChanged();
        }
    }

    private void OnExit_TemplateContainerSelected(ClickEvent clickEvent)
    {
        RmoveCurrentItemSelected();
        menu.SwitchPage(menu.mainPage_VisualElement);
    }

    private void OnItemSelected(ClickEvent clickEvent)
    {
        RmoveCurrentItemSelected();

        VisualElement parent_VisualElement = clickEvent.currentTarget as VisualElement;

        VisualElement background_VisualElement =
            parent_VisualElement.Q<VisualElement>("Background_VisualElement");
        background_VisualElement.RemoveFromClassList("UnSelected");
        background_VisualElement.AddToClassList("Selected");

        string[] parts = parent_VisualElement.name.Split('_');
        currentVisualElementIndex = int.Parse(parts[0]);
    }


    #endregion


    #region Events Manager

    private void ConnctEvents()
    {
        EventsManager.OnLanguageChanged_Event += OnLanguageChanged;
        EventsManager.OnFontSizeChanged_Event += OnFontSizeChanged;

        EventsManager.OnWinLevel_Event += OnCurrencyChanged;
    }

    private void DisconnctEvents()
    {
        EventsManager.OnLanguageChanged_Event -= OnLanguageChanged;
        EventsManager.OnFontSizeChanged_Event -= OnFontSizeChanged;

        EventsManager.OnWinLevel_Event -= OnCurrencyChanged;
    }


    private void OnLanguageChanged()
    {
        for (int i = 0; i < inventoryShopItems_List.Count; i++)
        {
            #region ItemName
            Label label = inventoryShopItems_List[i].Q<Label>("ItemName_Label");
            label.text = InventoryShopData.InventoryShopItems[i].ItemName[SettingsData.currentLanguageIndex];
            label.languageDirection =
                LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
            label.style.unityFont =
                LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
            #endregion

            #region ItemPrice
            label = inventoryShopItems_List[i].Q<Label>("ItemPrice_Label");
            label.text = "" + InventoryShopData.InventoryShopItems[i].Price;
            label.languageDirection =
                LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
            label.style.unityFont =
                LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
            #endregion
        }

        #region Inventory
        inventory_Label.text =
            LanguageTextsData.inventory[SettingsData.currentLanguageIndex];
        inventory_Label.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        inventory_Label.style.unityFont =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        #endregion

        #region WatchAd
        watchAd_Label.text =
            LanguageTextsData.watchAd[SettingsData.currentLanguageIndex];
        watchAd_Label.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        watchAd_Label.style.unityFont =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        #endregion

        #region Reward
        reward_Label.text =
            LanguageTextsData.reward[SettingsData.currentLanguageIndex];
        reward_Label.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        reward_Label.style.unityFont =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        #endregion


        #region Shop
        shop_Label.text =
            LanguageTextsData.shop[SettingsData.currentLanguageIndex];
        shop_Label.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        shop_Label.style.unityFont =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        #endregion

        #region Currency
        currencyAmount_Label.text = "" + AchievementsData.coins;
        currencyAmount_Label.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        currencyAmount_Label.style.unityFont =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        #endregion

        #region Buy
        buy_Label.text =
            LanguageTextsData.buy[SettingsData.currentLanguageIndex];
        buy_Label.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        buy_Label.style.unityFont =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        #endregion

        #region Exit
        exit_Label.text =
            LanguageTextsData.exit[SettingsData.currentLanguageIndex];
        exit_Label.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        exit_Label.style.unityFont =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        #endregion
    }

    private void OnFontSizeChanged()
    {
        for (int i = 0; i < inventoryShopItems_List.Count; i++)
        {
            #region ItemName
            Label label = inventoryShopItems_List[i].Q<Label>("ItemName_Label");
            label.style.fontSize =
                LanguageTextsData.fontSize_CategoryAverage[SettingsData.currentFontSizeIndex];
            #endregion

            #region ItemPrice
            label = inventoryShopItems_List[i].Q<Label>("ItemPrice_Label");
            label.style.fontSize =
                LanguageTextsData.fontSize_CategoryAverage[SettingsData.currentFontSizeIndex];
            #endregion
        }


        #region Inventory
        inventory_Label.style.fontSize =
            LanguageTextsData.fontSize_CategoryAverage[SettingsData.currentFontSizeIndex];
        #endregion

        #region WatchAd
        watchAd_Label.style.fontSize =
            LanguageTextsData.fontSize_CategorySmall[SettingsData.currentFontSizeIndex];
        #endregion

        #region Reward
        reward_Label.style.fontSize =
            LanguageTextsData.fontSize_CategorySmall[SettingsData.currentFontSizeIndex];
        #endregion

        #region Shop
        shop_Label.style.fontSize =
                LanguageTextsData.fontSize_CategoryAverage[SettingsData.currentFontSizeIndex];
        #endregion

        #region Currency
        currencyAmount_Label.style.fontSize =
                LanguageTextsData.fontSize_CategoryAverage[SettingsData.currentFontSizeIndex];
        #endregion

        #region Buy
        buy_Label.style.fontSize =
            LanguageTextsData.fontSize_CategoryAverage[SettingsData.currentFontSizeIndex];
        #endregion

        #region Exit
        exit_Label.style.fontSize =
            LanguageTextsData.fontSize_CategoryAverage[SettingsData.currentFontSizeIndex];
        #endregion
    }


    private void OnCurrencyChanged()//Change to coins changed
    {
        #region Currency
        currencyAmount_Label.text = "" + AchievementsData.coins;
        #endregion
    }

    #endregion



    #region Utilities

    private void RmoveCurrentItemSelected()
    {
        if (currentVisualElementIndex != -1)
        {
            VisualElement background_VisualElement =
                inventoryShopItems_List[currentVisualElementIndex].Q<VisualElement>("Background_VisualElement");

            background_VisualElement.RemoveFromClassList("Selected");
            background_VisualElement.AddToClassList("UnSelected");
        }
        currentVisualElementIndex = -1;
    }


    public void FillScrollViews()
    {
        for (int i = 0; i < InventoryShopData.InventoryShopItems.Length; i++)
        {
            VisualElement inventoryShopItem_VisualElement = inventoryShopItem_Template.Instantiate();
            inventoryShopItem_VisualElement.name = i + "_" + InventoryShopData.InventoryShopItems[i].ItemName[0];

            inventoryShopItem_VisualElement.AddToClassList("InventoryShopItem_Template");
            UI_Utilities.FixInventoryShopItemSize(inventoryShopItem_VisualElement);

            VisualElement itemImage_VisualElement =
                inventoryShopItem_VisualElement.Q<VisualElement>("ItemImage_VisualElement");
            itemImage_VisualElement.style.backgroundImage =
                Resources.Load<Texture2D>("InventoryShopItems_Images/" +
                    InventoryShopData.InventoryShopItems[i].ItemName[0]);
            if (InventoryShopData.InventoryShopItems[i].ItemName[0] == "Health")
            {
                itemImage_VisualElement.style.unityBackgroundImageTintColor = Color.red;
            }

            VisualElement itemCurrencyImage_VisualElement =
                inventoryShopItem_VisualElement.Q<VisualElement>("ItemCurrencyImage_VisualElement");
            itemCurrencyImage_VisualElement.style.backgroundImage =
                Resources.Load<Texture2D>("Images/Currency");

            inventoryShopItems_List.Add(inventoryShopItem_VisualElement);

            if (InventoryShopData.InventoryShopItems[i].IsBought)
            {
                inventory_ScrollView.Add(inventoryShopItem_VisualElement);
            }
            else
            {
                inventoryShopItem_VisualElement.RegisterCallback<ClickEvent>(OnItemSelected);
                shop_ScrollView.Add(inventoryShopItem_VisualElement);
            }
        }
    }

    #endregion

}
