using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InventoryShopPage_Controller : MonoBehaviour
{
    Menu_UIConnector menu_UIConnector;

    VisualTreeAsset inventoryShopItem_Template;
    List<VisualElement> inventoryShopItems_List;
    int currentVisualElementIndex = -1;


    public void Initialize(Menu_UIConnector menu_UIConnector)
    {
        this.menu_UIConnector = menu_UIConnector;

        InventoryShop_SaveSystem.Load_InventoryShopItems();

        inventoryShopItem_Template =
            Resources.Load<VisualTreeAsset>("UI/Basic_Templates/InventoryShopItem/InventoryShopItem_Template");

        inventoryShopItems_List = new List<VisualElement>();
        RmoveCurrentItemSelected();

        menu_UIConnector.watchAd_TemplateContainer.
            RegisterCallback<ClickEvent>(OnWatchAd_TemplateContainerSelected);

        menu_UIConnector.buy_TemplateContainer.
            RegisterCallback<ClickEvent>(OnBuy_TemplateContainerSelected);
        menu_UIConnector.exit_TemplateContainer.
            RegisterCallback<ClickEvent>(OnExit_TemplateContainerSelected);

        EventsManager.OnLanguageChanged_Event += OnLanguageChanged;
        EventsManager.OnFontSizeChanged_Event += OnFontSizeChanged;
        EventsManager.OnWinLevel_Event += OnCurrencyChanged;

        ScrollViewController.InitializeScrollView(menu_UIConnector.inventory_ScrollView);

        ScrollViewController.InitializeScrollView(menu_UIConnector.shop_ScrollView);

        FillScrollViews();

        OnLanguageChanged();
        OnFontSizeChanged();
    }

    private void OnDisable()
    {
        menu_UIConnector.watchAd_TemplateContainer.
            UnregisterCallback<ClickEvent>(OnWatchAd_TemplateContainerSelected);

        menu_UIConnector.buy_TemplateContainer.
            UnregisterCallback<ClickEvent>(OnBuy_TemplateContainerSelected);
        menu_UIConnector.exit_TemplateContainer.
            UnregisterCallback<ClickEvent>(OnExit_TemplateContainerSelected);

        EventsManager.OnLanguageChanged_Event -= OnLanguageChanged;
        EventsManager.OnFontSizeChanged_Event -= OnFontSizeChanged;
        EventsManager.OnWinLevel_Event -= OnCurrencyChanged;
    }

    public void FillScrollViews()
    {
        for (int i = 0; i < InventoryShopData.InventoryShopItems.Length; i++)
        {
            VisualElement inventoryShopItem_VisualElement = inventoryShopItem_Template.Instantiate();
            inventoryShopItem_VisualElement.name = i + "_" + InventoryShopData.InventoryShopItems[i].ItemName[0];

            inventoryShopItem_VisualElement.style.height = Screen.width / 10;
            inventoryShopItem_VisualElement.AddToClassList("InventoryShopItem_Template");

            VisualElement itemImage_VisualElement =
                inventoryShopItem_VisualElement.Q<VisualElement>("ItemImage_VisualElement");
            itemImage_VisualElement.style.backgroundImage =
                Resources.Load<Texture2D>("InventoryShopItems_Images/" +
                    InventoryShopData.InventoryShopItems[i].ItemName[0]);

            VisualElement itemCurrencyImage_VisualElement =
                inventoryShopItem_VisualElement.Q<VisualElement>("ItemCurrencyImage_VisualElement");
            itemCurrencyImage_VisualElement.style.backgroundImage =
                Resources.Load<Texture2D>("Images/Currency");

            inventoryShopItems_List.Add(inventoryShopItem_VisualElement);

            if (InventoryShopData.InventoryShopItems[i].IsBought)
            {
                menu_UIConnector.inventory_ScrollView.Add(inventoryShopItem_VisualElement);
            }
            else
            {
                inventoryShopItem_VisualElement.RegisterCallback<ClickEvent>(OnItemSelected);
                menu_UIConnector.shop_ScrollView.Add(inventoryShopItem_VisualElement);
            }
        }

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
            menu_UIConnector.inventory_ScrollView.Add(inventoryShopItems_List[currentVisualElementIndex]);

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
        menu_UIConnector.SwitchPage(menu_UIConnector.mainPage_VisualElement);
    }


    #region EventsHandler

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
        menu_UIConnector.inventory_Label.text =
            LanguageTextsData.inventory[SettingsData.currentLanguageIndex];
        menu_UIConnector.inventory_Label.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        menu_UIConnector.inventory_Label.style.unityFont =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        #endregion

        #region WatchAd
        menu_UIConnector.watchAd_Label.text =
            LanguageTextsData.watchAd[SettingsData.currentLanguageIndex];
        menu_UIConnector.watchAd_Label.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        menu_UIConnector.watchAd_Label.style.unityFont =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        #endregion

        #region Reward
        menu_UIConnector.reward_Label.text =
            LanguageTextsData.reward[SettingsData.currentLanguageIndex];
        menu_UIConnector.reward_Label.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        menu_UIConnector.reward_Label.style.unityFont =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        #endregion


        #region Shop
        menu_UIConnector.shop_Label.text =
            LanguageTextsData.shop[SettingsData.currentLanguageIndex];
        menu_UIConnector.shop_Label.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        menu_UIConnector.shop_Label.style.unityFont =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        #endregion

        #region Currency
        menu_UIConnector.currencyAmount_Label.text = "" + AchievementsData.coins;
        menu_UIConnector.currencyAmount_Label.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        menu_UIConnector.currencyAmount_Label.style.unityFont =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        #endregion

        #region Buy
        menu_UIConnector.buy_Label.text =
            LanguageTextsData.buy[SettingsData.currentLanguageIndex];
        menu_UIConnector.buy_Label.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        menu_UIConnector.buy_Label.style.unityFont =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        #endregion

        #region Exit
        menu_UIConnector.exit_Label.text =
            LanguageTextsData.exit[SettingsData.currentLanguageIndex];
        menu_UIConnector.exit_Label.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        menu_UIConnector.exit_Label.style.unityFont =
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
        menu_UIConnector.inventory_Label.style.fontSize =
            LanguageTextsData.fontSize_CategoryAverage[SettingsData.currentFontSizeIndex];
        #endregion

        #region WatchAd
        menu_UIConnector.watchAd_Label.style.fontSize =
            LanguageTextsData.fontSize_CategoryAverage[SettingsData.currentFontSizeIndex];
        #endregion

        #region Reward
        menu_UIConnector.reward_Label.style.fontSize =
            LanguageTextsData.fontSize_CategoryAverage[SettingsData.currentFontSizeIndex];
        #endregion

        #region Shop
        menu_UIConnector.shop_Label.style.fontSize =
                LanguageTextsData.fontSize_CategoryAverage[SettingsData.currentFontSizeIndex];
        #endregion

        #region Currency
        menu_UIConnector.currencyAmount_Label.style.fontSize =
                LanguageTextsData.fontSize_CategoryAverage[SettingsData.currentFontSizeIndex];
        #endregion

        #region Buy
        menu_UIConnector.buy_Label.style.fontSize =
            LanguageTextsData.fontSize_CategoryAverage[SettingsData.currentFontSizeIndex];
        #endregion

        #region Exit
        menu_UIConnector.exit_Label.style.fontSize =
            LanguageTextsData.fontSize_CategoryAverage[SettingsData.currentFontSizeIndex];
        #endregion
    }

    private void OnCurrencyChanged()//Change to coins changed
    {
        #region Currency
        menu_UIConnector.currencyAmount_Label.text = "" + AchievementsData.coins;
        #endregion
    }

    #endregion

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
}
