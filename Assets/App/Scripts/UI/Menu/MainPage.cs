using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;


public class MainPage : MonoBehaviour
{
    PanelRenderer panelRenderer;
    Menu menu;


    private VisualElement mainPage_VisualElement;
    private VisualElement resume_Button_TemplateContainer;
    private VisualElement levels_Button_TemplateContainer;
    private VisualElement inventoryShop_Button_TemplateContainer;
    private VisualElement settings_Button_TemplateContainer;
    private VisualElement exit_Button_TemplateContainer;




    private void OnEnable()
    {
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
        mainPage_VisualElement = root.Q<VisualElement>("MainPage_VisualElement");
        resume_Button_TemplateContainer = mainPage_VisualElement.Q<VisualElement>("Resume_Button_TemplateContainer");
        levels_Button_TemplateContainer = mainPage_VisualElement.Q<VisualElement>("Levels_Button_TemplateContainer");
        inventoryShop_Button_TemplateContainer = mainPage_VisualElement.Q<VisualElement>("Shop_Button_TemplateContainer");
        settings_Button_TemplateContainer = mainPage_VisualElement.Q<VisualElement>("Settings_Button_TemplateContainer");
        exit_Button_TemplateContainer = mainPage_VisualElement.Q<VisualElement>("Exit_Button_TemplateContainer");

        resume_Button_TemplateContainer.style.display = DisplayStyle.None;

        AddFunctionality();
        ConnctEvents();

        OnLanguageChanged();
        OnFontSizeChanged();
    }




    #region Functionality
    private void AddFunctionality()
    {
        resume_Button_TemplateContainer.RegisterCallback<ClickEvent>(OnResume_ButtonSelected);
        levels_Button_TemplateContainer.RegisterCallback<ClickEvent>(OnLevels_ButtonSelected);
        inventoryShop_Button_TemplateContainer.RegisterCallback<ClickEvent>(OnInventoryShop_ButtonSelected);
        settings_Button_TemplateContainer.RegisterCallback<ClickEvent>(OnSettings_ButtonSelected);
        exit_Button_TemplateContainer.RegisterCallback<ClickEvent>(OnExit_ButtonSelected);
    }

    private void RemoveFunctionality()
    {
        resume_Button_TemplateContainer.UnregisterCallback<ClickEvent>(OnResume_ButtonSelected);
        levels_Button_TemplateContainer.UnregisterCallback<ClickEvent>(OnLevels_ButtonSelected);
        inventoryShop_Button_TemplateContainer.UnregisterCallback<ClickEvent>(OnInventoryShop_ButtonSelected);
        settings_Button_TemplateContainer.UnregisterCallback<ClickEvent>(OnSettings_ButtonSelected);
        exit_Button_TemplateContainer.UnregisterCallback<ClickEvent>(OnExit_ButtonSelected);
    }

    private void OnResume_ButtonSelected(ClickEvent clickEvent)
    {
        menu.SwitchPage(menu.menu_VisualElement_TemplateContainer);
        EventsManager.InvokeOnResumeLevel();
    }

    private void OnLevels_ButtonSelected(ClickEvent clickEvent)
    {
        resume_Button_TemplateContainer.style.display = DisplayStyle.None;
        menu.SwitchPage(menu.levelsPage_VisualElement);
        EventsManager.InvokeOnStopLevel();
    }

    private void OnInventoryShop_ButtonSelected(ClickEvent clickEvent)
    {
        resume_Button_TemplateContainer.style.display = DisplayStyle.None;
        menu.SwitchPage(menu.inventoryShopPage_VisualElement);
        EventsManager.InvokeOnStopLevel();
    }

    private void OnSettings_ButtonSelected(ClickEvent clickEvent)
    {
        menu.SwitchPage(menu.settingsPage_VisualElement);
    }

    private void OnExit_ButtonSelected(ClickEvent clickEvent)
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    #endregion


    #region Events Manager

    private void ConnctEvents()
    {
        EventsManager.OnLanguageChanged_Event += OnLanguageChanged;
        EventsManager.OnFontSizeChanged_Event += OnFontSizeChanged;

        EventsManager.OnPauseLevel_Event += ShowResume;
    }

    private void DisconnctEvents()
    {
        EventsManager.OnLanguageChanged_Event -= OnLanguageChanged;
        EventsManager.OnFontSizeChanged_Event -= OnFontSizeChanged;

        EventsManager.OnPauseLevel_Event -= ShowResume;
    }


    private void OnLanguageChanged()
    {
        #region Resume
        Label resume_Label = resume_Button_TemplateContainer.Q<Label>();
        resume_Label.text =
            LanguageTextsData.resume[SettingsData.currentLanguageIndex];
        resume_Label.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        resume_Label.style.unityFont =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        #endregion

        #region Levels
        Label levels_Label = levels_Button_TemplateContainer.Q<Label>();
        levels_Label.text =
            LanguageTextsData.levels[SettingsData.currentLanguageIndex];
        levels_Label.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        levels_Label.style.unityFont =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        #endregion

        #region InventoryShop
        Label inventoryShop_Label = inventoryShop_Button_TemplateContainer.Q<Label>();
        inventoryShop_Label.text =
            LanguageTextsData.inventoryShop[SettingsData.currentLanguageIndex];
        inventoryShop_Label.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        inventoryShop_Label.style.unityFont =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        #endregion

        #region Settings
        Label settings_Label = settings_Button_TemplateContainer.Q<Label>();
        settings_Label.text =
            LanguageTextsData.settings[SettingsData.currentLanguageIndex];
        settings_Label.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        settings_Label.style.unityFont =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        #endregion

        #region Exit
        Label exit_Label = exit_Button_TemplateContainer.Q<Label>();
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
        #region Resume
        Label resume_Label = resume_Button_TemplateContainer.Q<Label>();
        resume_Label.style.fontSize =
            LanguageTextsData.fontSize_CategoryAverage[SettingsData.currentFontSizeIndex];
        #endregion

        #region Levels
        Label levels_Label = levels_Button_TemplateContainer.Q<Label>();
        levels_Label.style.fontSize =
            LanguageTextsData.fontSize_CategoryAverage[SettingsData.currentFontSizeIndex];
        #endregion

        #region InventoryShop
        Label inventoryShop_Label = inventoryShop_Button_TemplateContainer.Q<Label>();
        inventoryShop_Label.style.fontSize =
            LanguageTextsData.fontSize_CategoryAverage[SettingsData.currentFontSizeIndex];
        #endregion

        #region Settings
        Label settings_Label = settings_Button_TemplateContainer.Q<Label>();
        settings_Label.style.fontSize =
            LanguageTextsData.fontSize_CategoryAverage[SettingsData.currentFontSizeIndex];
        #endregion

        #region Exit
        Label exit_Label = exit_Button_TemplateContainer.Q<Label>();
        exit_Label.style.fontSize =
            LanguageTextsData.fontSize_CategoryAverage[SettingsData.currentFontSizeIndex];
        #endregion
    }


    private void ShowResume()
    {
        resume_Button_TemplateContainer.style.display = DisplayStyle.Flex;
    }

    #endregion



}
