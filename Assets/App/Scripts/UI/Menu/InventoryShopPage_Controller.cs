using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InventoryShopPage_Controller : MonoBehaviour
{
    Menu_UIConnector menu_UIConnector;

    VisualTreeAsset inventoryShopItem_Template;
    List<VisualElement> inventoryShopItems_List;


    public void Initialize(Menu_UIConnector menu_UIConnector)
    {
        //InventoryShop_SaveSystem.Load_InventoryShop();

        EventsManager.OnLanguageChanged_Event += OnLanguageChanged;
        EventsManager.OnFontSizeChanged_Event += OnFontSizeChanged;

        inventoryShopItem_Template =
            Resources.Load<VisualTreeAsset>("UI/Basic_Templates/InventoryShopItem/InventoryShopItem_Template");

        inventoryShopItems_List = new List<VisualElement>();

        this.menu_UIConnector = menu_UIConnector;



        menu_UIConnector.buy_TemplateContainer.
            RegisterCallback<ClickEvent>(OnBuy_TemplateContainerSelected);
        menu_UIConnector.exit_TemplateContainer.
            RegisterCallback<ClickEvent>(OnExit_TemplateContainerSelected);

        FillScrollViewHolders();

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
    }

    public void FillScrollViewHolders()
    {
        for (int i = 0; i < InventoryShopData.InventoryShopItems.Length; i++)
        {
            VisualElement inventoryShopItem_VisualElement = inventoryShopItem_Template.Instantiate();

            inventoryShopItem_VisualElement.style.width = Screen.width / 10;
            inventoryShopItem_VisualElement.style.height = Screen.width / 5;

            inventoryShopItem_VisualElement.style.marginLeft = Screen.width / 100;
            inventoryShopItem_VisualElement.style.marginTop = Screen.width / 100;
            inventoryShopItem_VisualElement.style.marginRight = Screen.width / 100;
            inventoryShopItem_VisualElement.style.marginBottom = Screen.width / 100;

            inventoryShopItem_VisualElement.name = InventoryShopData.InventoryShopItems[i].ItemName;

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

            inventoryShopItems_List.Add(inventoryShopItem_VisualElement);

            if (InventoryShopData.InventoryShopItems[i].IsBought)
            {
                menu_UIConnector.inventoryScrollViewHolder_VisualElement.Add(inventoryShopItem_VisualElement);
            }
            else
            {
                menu_UIConnector.shopScrollViewHolder_VisualElement.Add(inventoryShopItem_VisualElement);
            }
        }

    }


    private void OnBuy_TemplateContainerSelected(ClickEvent clickEvent)
    {
        //Move Item From Buy To Inventory And Save
    }

    private void OnExit_TemplateContainerSelected(ClickEvent clickEvent)
    {
        menu_UIConnector.SwitchPage(menu_UIConnector.mainPage_VisualElement);
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

    #endregion
}
