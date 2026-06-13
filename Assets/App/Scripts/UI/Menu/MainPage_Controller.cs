#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;
using UnityEngine.UIElements;

public class MainPage_Controller : MonoBehaviour
{
    Menu_UIConnector menu_UIConnector;

    public void Initialize(Menu_UIConnector menu_UIConnector)
    {
        EventsManager.OnLanguageChanged_Event += OnLanguageChanged;
        EventsManager.OnFontSizeChanged_Event += OnFontSizeChanged;

        this.menu_UIConnector = menu_UIConnector;

        menu_UIConnector.resume_Button_TemplateContainer.RegisterCallback<ClickEvent>(OnResume_ButtonSelected);
        menu_UIConnector.levels_Button_TemplateContainer.RegisterCallback<ClickEvent>(OnLevels_ButtonSelected);
        menu_UIConnector.inventoryShop_Button_TemplateContainer.RegisterCallback<ClickEvent>(OnInventoryShop_ButtonSelected);
        menu_UIConnector.settings_Button_TemplateContainer.RegisterCallback<ClickEvent>(OnSettings_ButtonSelected);
        menu_UIConnector.exit_Button_TemplateContainer.RegisterCallback<ClickEvent>(OnExit_ButtonSelected);

        OnLanguageChanged();
        OnFontSizeChanged();
    }

    private void OnDisable()
    {
        EventsManager.OnLanguageChanged_Event -= OnLanguageChanged;
        EventsManager.OnFontSizeChanged_Event -= OnFontSizeChanged;
    }


    #region EventsHandler

    private void OnLanguageChanged()
    {
        #region Resume
        Label resume_Label = menu_UIConnector.resume_Button_TemplateContainer.Q<Label>();
        resume_Label.text =
            LanguageTextsData.resume[SettingsData.currentLanguageIndex];
        resume_Label.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        resume_Label.style.unityFont =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        #endregion

        #region Levels
        Label levels_Label = menu_UIConnector.levels_Button_TemplateContainer.Q<Label>();
        levels_Label.text =
            LanguageTextsData.levels[SettingsData.currentLanguageIndex];
        levels_Label.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        levels_Label.style.unityFont =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        #endregion

        #region InventoryShop
        Label inventoryShop_Label = menu_UIConnector.inventoryShop_Button_TemplateContainer.Q<Label>();
        inventoryShop_Label.text =
            LanguageTextsData.inventoryShop[SettingsData.currentLanguageIndex];
        inventoryShop_Label.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        inventoryShop_Label.style.unityFont =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        #endregion

        #region Settings
        Label settings_Label = menu_UIConnector.settings_Button_TemplateContainer.Q<Label>();
        settings_Label.text =
            LanguageTextsData.settings[SettingsData.currentLanguageIndex];
        settings_Label.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        settings_Label.style.unityFont =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        #endregion

        #region Exit
        Label exit_Label = menu_UIConnector.exit_Button_TemplateContainer.Q<Label>();
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
        Label resume_Label = menu_UIConnector.resume_Button_TemplateContainer.Q<Label>();
        resume_Label.style.fontSize =
            LanguageTextsData.fontSize_CategoryAverage[SettingsData.currentFontSizeIndex];
        #endregion

        #region Levels
        Label levels_Label = menu_UIConnector.levels_Button_TemplateContainer.Q<Label>();
        levels_Label.style.fontSize =
            LanguageTextsData.fontSize_CategoryAverage[SettingsData.currentFontSizeIndex];
        #endregion

        #region InventoryShop
        Label inventoryShop_Label = menu_UIConnector.inventoryShop_Button_TemplateContainer.Q<Label>();
        inventoryShop_Label.style.fontSize =
            LanguageTextsData.fontSize_CategoryAverage[SettingsData.currentFontSizeIndex];
        #endregion

        #region Settings
        Label settings_Label = menu_UIConnector.settings_Button_TemplateContainer.Q<Label>();
        settings_Label.style.fontSize =
            LanguageTextsData.fontSize_CategoryAverage[SettingsData.currentFontSizeIndex];
        #endregion

        #region Exit
        Label exit_Label = menu_UIConnector.exit_Button_TemplateContainer.Q<Label>();
        exit_Label.style.fontSize =
            LanguageTextsData.fontSize_CategoryAverage[SettingsData.currentFontSizeIndex];
        #endregion
    }

    #endregion


    private void OnResume_ButtonSelected(ClickEvent clickEvent)
    {
        menu_UIConnector.SwitchPage(menu_UIConnector.menu_VisualElement_TemplateContainer);
        EventsManager.InvokeOnResumeLevel();
    }

    private void OnLevels_ButtonSelected(ClickEvent clickEvent)
    {
        menu_UIConnector.resume_Button_TemplateContainer.style.display = DisplayStyle.None;
        menu_UIConnector.SwitchPage(menu_UIConnector.levelsPage_VisualElement);
        EventsManager.InvokeOnStopLevel();
    }

    private void OnInventoryShop_ButtonSelected(ClickEvent clickEvent)
    {
        menu_UIConnector.resume_Button_TemplateContainer.style.display = DisplayStyle.None;
        menu_UIConnector.SwitchPage(menu_UIConnector.inventoryShopPage_VisualElement);
        EventsManager.InvokeOnStopLevel();
    }

    private void OnSettings_ButtonSelected(ClickEvent clickEvent)
    {
        menu_UIConnector.SwitchPage(menu_UIConnector.settingsPage_VisualElement);
    }

    private void OnExit_ButtonSelected(ClickEvent clickEvent)
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
