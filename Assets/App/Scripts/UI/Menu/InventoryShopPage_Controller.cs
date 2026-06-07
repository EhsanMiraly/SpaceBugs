using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InventoryShopPage_Controller : MonoBehaviour
{
    Menu_UIConnector menu_UIConnector;

    VisualTreeAsset inventoryShopItem_Template;
    List<VisualElement> inventoryShopItems_List;
    VisualElement currentVisualElement;
    int currentVisualElementIndex;
    string currentBoughtItem;


    public void Initialize(Menu_UIConnector menu_UIConnector)
    {
        this.menu_UIConnector = menu_UIConnector;

        InventoryShop_SaveSystem.Load_InventoryShopItems();

        EventsManager.OnLanguageChanged_Event += OnLanguageChanged;
        EventsManager.OnFontSizeChanged_Event += OnFontSizeChanged;
        EventsManager.OnWinLevel_Event += OnCurrencyChanged;

        inventoryShopItem_Template =
            Resources.Load<VisualTreeAsset>("UI/Basic_Templates/InventoryShopItem/InventoryShopItem_Template");

        inventoryShopItems_List = new List<VisualElement>();
        RmoveCurrentItemSelected();

        menu_UIConnector.buy_TemplateContainer.
            RegisterCallback<ClickEvent>(OnBuy_TemplateContainerSelected);
        menu_UIConnector.exit_TemplateContainer.
            RegisterCallback<ClickEvent>(OnExit_TemplateContainerSelected);

        ScrollViewController.InitializeScrollView(menu_UIConnector.inventory_ScrollView);

        ScrollViewController.InitializeScrollView(menu_UIConnector.shop_ScrollView);

        FillScrollViews();

        OnLanguageChanged();
        OnFontSizeChanged();
    }

    private void OnDisable()
    {
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

            inventoryShopItem_VisualElement.style.width = Length.Percent(100);
            inventoryShopItem_VisualElement.style.height = Screen.width / 10;

            inventoryShopItem_VisualElement.style.paddingLeft = Screen.width / 100;
            inventoryShopItem_VisualElement.style.paddingTop = Screen.width / 100;
            inventoryShopItem_VisualElement.style.paddingRight = Screen.width / 100;
            inventoryShopItem_VisualElement.style.paddingBottom = Screen.width / 100;

            VisualElement background_VisualElement =
                inventoryShopItem_VisualElement.Q<VisualElement>("Background_VisualElement");

            VisualElement itemImage_VisualElement =
                inventoryShopItem_VisualElement.Q<VisualElement>("ItemImage_VisualElement");
            itemImage_VisualElement.style.backgroundImage =
                Resources.Load<Texture2D>("InventoryShopItems_Images/" +
                    InventoryShopData.InventoryShopItems[i].ItemName);

            Label itemName_Label = inventoryShopItem_VisualElement.Q<Label>("ItemName_Label");
            itemName_Label.text = InventoryShopData.InventoryShopItems[i].ItemName;

            Label itemPrice_Label = inventoryShopItem_VisualElement.Q<Label>("ItemPrice_Label");
            itemPrice_Label.text = "" + InventoryShopData.InventoryShopItems[i].Price;

            VisualElement itemCurrencyImage_VisualElement =
                inventoryShopItem_VisualElement.Q<VisualElement>("ItemCurrencyImage_VisualElement");
            itemCurrencyImage_VisualElement.style.backgroundImage =
                Resources.Load<Texture2D>("Images/Currency");

            inventoryShopItems_List.Add(background_VisualElement);

            if (InventoryShopData.InventoryShopItems[i].IsBought)
            {
                menu_UIConnector.inventory_ScrollView.Add(inventoryShopItem_VisualElement);
            }
            else
            {
                background_VisualElement.RegisterCallback<ClickEvent>(OnItemSelected);
                menu_UIConnector.shop_ScrollView.Add(inventoryShopItem_VisualElement);
            }
        }

    }


    private void OnBuy_TemplateContainerSelected(ClickEvent clickEvent)
    {
        if (currentVisualElement == null)
        {
            return;
        }

        if (AchievementsData.coins >= InventoryShopData.InventoryShopItems[currentVisualElementIndex].Price)
        {
            currentVisualElement.UnregisterCallback<ClickEvent>(OnItemSelected);
            menu_UIConnector.inventory_ScrollView.Add(currentVisualElement.parent);

            AchievementsData.coins -= InventoryShopData.InventoryShopItems[currentVisualElementIndex].Price;
            InventoryShopData.InventoryShopItems[currentVisualElementIndex].IsBought = true;
            if (currentBoughtItem == "Bullet")
            {
                AchievementsData.bullets++;
            }
            else if (currentBoughtItem == "Health")
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

    private void OnItemSelected(ClickEvent clickEvent)
    {
        VisualElement parent_VisualElement = clickEvent.currentTarget as VisualElement;
        VisualElement background_VisualElement = parent_VisualElement.Q<VisualElement>("Background_VisualElement");
        Label label = background_VisualElement.Q<Label>("ItemName_Label");

        if (currentVisualElement != null)
        {
            currentVisualElement.RemoveFromClassList("Selected");
            currentVisualElement.AddToClassList("UnSelected");
        }

        for (int i = 0; i < inventoryShopItems_List.Count; i++)
        {
            if (inventoryShopItems_List[i].Q<Label>("ItemName_Label").text == label.text)
            {
                inventoryShopItems_List[i].RemoveFromClassList("UnSelected");
                inventoryShopItems_List[i].AddToClassList("Selected");
                currentVisualElement = inventoryShopItems_List[i];
                currentVisualElementIndex = i;
                currentBoughtItem = label.text;
            }
        }
    }

    #region EventsHandler

    private void OnLanguageChanged()
    {
        #region Inventory
        menu_UIConnector.inventory_Label.text =
            LanguageTextsData.inventory[SettingsData.currentLanguageIndex];
        menu_UIConnector.inventory_Label.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        menu_UIConnector.inventory_Label.style.unityFont =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        #endregion

        #region Currency
        menu_UIConnector.currencyAmount_Label.text = "" + AchievementsData.coins;
        menu_UIConnector.currencyAmount_Label.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        menu_UIConnector.currencyAmount_Label.style.unityFont =
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
        #region Inventory
        menu_UIConnector.inventory_Label.style.fontSize =
            LanguageTextsData.fontSize_CategoryAverage[SettingsData.currentFontSizeIndex];
        #endregion

        #region Currency
        menu_UIConnector.currencyAmount_Label.style.fontSize =
            LanguageTextsData.fontSize_CategoryAverage[SettingsData.currentFontSizeIndex];
        #endregion

        #region Shop
        menu_UIConnector.shop_Label.style.fontSize =
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
        if (currentVisualElement != null)
        {
            currentVisualElement.RemoveFromClassList("Selected");
            currentVisualElement.AddToClassList("UnSelected");
        }
        currentVisualElement = null;
        currentVisualElementIndex = -1;
        currentBoughtItem = "";
    }
}
