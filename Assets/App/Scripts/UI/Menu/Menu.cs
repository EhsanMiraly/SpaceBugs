using UnityEngine;
using UnityEngine.UIElements;


[RequireComponent(typeof(Menu_VisualElement), typeof(MainPage), typeof(LevelsPage))]
[RequireComponent(typeof(SettingsPage), typeof(InventoryShopPage))]
public class Menu : MonoBehaviour
{
    PanelRenderer panelRenderer;


    #region Pages
    [System.NonSerialized] public VisualElement menu_VisualElement_TemplateContainer;
    [System.NonSerialized] public VisualElement mainPage_VisualElement;
    [System.NonSerialized] public VisualElement levelsPage_VisualElement;
    [System.NonSerialized] public VisualElement settingsPage_VisualElement;
    [System.NonSerialized] public VisualElement inventoryShopPage_VisualElement;
    #endregion



    private void OnEnable()
    {
        panelRenderer = GetComponent<PanelRenderer>();
        panelRenderer.RegisterUIReloadCallback(OnUIReloadCallback);
    }

    private void OnDisable()
    {
        panelRenderer.UnregisterUIReloadCallback(OnUIReloadCallback);
    }


    private void OnUIReloadCallback(PanelRenderer panelRenderer, VisualElement root)
    {
        ScreenSafeArea.RemoveUnSafeAreaFromUI(root);

        menu_VisualElement_TemplateContainer = root.Q<VisualElement>("Menu_VisualElement_TemplateContainer");
        mainPage_VisualElement = root.Q<VisualElement>("MainPage_VisualElement");
        levelsPage_VisualElement = root.Q<VisualElement>("LevelsPage_VisualElement");
        settingsPage_VisualElement = root.Q<VisualElement>("SettingsPage_VisualElement");
        inventoryShopPage_VisualElement = root.Q<VisualElement>("InventoryShopPage_VisualElement");

        SwitchPage(mainPage_VisualElement);
    }



    public void SwitchPage(VisualElement page)
    {
        menu_VisualElement_TemplateContainer.style.display = DisplayStyle.None;
        mainPage_VisualElement.style.display = DisplayStyle.None;
        levelsPage_VisualElement.style.display = DisplayStyle.None;
        inventoryShopPage_VisualElement.style.display = DisplayStyle.None;
        settingsPage_VisualElement.style.display = DisplayStyle.None;

        page.style.display = DisplayStyle.Flex;
    }

}
