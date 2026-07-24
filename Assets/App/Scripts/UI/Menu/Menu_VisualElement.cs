using UnityEngine;
using UnityEngine.UIElements;


public class Menu_VisualElement : MonoBehaviour
{
    PanelRenderer panelRenderer;
    Menu menu;

    private VisualElement menu_VisualElement_TemplateContainer;
    private VisualElement menu_VisualElement;


    private void OnEnable()
    {
        panelRenderer = GetComponent<PanelRenderer>();
        panelRenderer.RegisterUIReloadCallback(OnUIReloadCallback);
        menu = GetComponent<Menu>();
    }

    private void OnDisable()
    {
        menu_VisualElement.UnregisterCallback<ClickEvent>(OnMenuSelected);

        panelRenderer.UnregisterUIReloadCallback(OnUIReloadCallback);
    }


    private void OnUIReloadCallback(PanelRenderer panelRenderer, VisualElement root)
    {
        menu_VisualElement_TemplateContainer = root.Q<VisualElement>("Menu_VisualElement_TemplateContainer");
        menu_VisualElement = menu_VisualElement_TemplateContainer.Q<VisualElement>("Menu_VisualElement");

        menu_VisualElement.RegisterCallback<ClickEvent>(OnMenuSelected);
    }


    private void OnMenuSelected(ClickEvent clickEvent)
    {
        EventsManager.InvokeOnPauseLevel();
        menu.SwitchPage(menu.mainPage_VisualElement);
    }



}
