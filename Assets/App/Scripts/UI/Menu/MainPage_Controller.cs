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

        menu_UIConnector.resume_Button.RegisterCallback<ClickEvent>(OnResume_ButtonSelected);
        menu_UIConnector.levels_Button.RegisterCallback<ClickEvent>(OnLevels_ButtonSelected);
        menu_UIConnector.inventoryShop_Button.RegisterCallback<ClickEvent>(OnInventoryShop_ButtonSelected);
        menu_UIConnector.settings_Button.RegisterCallback<ClickEvent>(OnSettings_ButtonSelected);
        menu_UIConnector.exit_Button.RegisterCallback<ClickEvent>(OnExit_ButtonSelected);

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
        menu_UIConnector.resume_Button.text =
            LanguageTextsData.resume[SettingsData.currentLanguageIndex];
        menu_UIConnector.resume_Button.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        menu_UIConnector.resume_Button.style.unityFont =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        #endregion

        #region Levels
        menu_UIConnector.levels_Button.text =
            LanguageTextsData.levels[SettingsData.currentLanguageIndex];
        menu_UIConnector.levels_Button.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        menu_UIConnector.levels_Button.style.unityFont =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        #endregion

        #region InventoryShop
        menu_UIConnector.inventoryShop_Button.text =
            LanguageTextsData.inventoryShop[SettingsData.currentLanguageIndex];
        menu_UIConnector.inventoryShop_Button.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        menu_UIConnector.inventoryShop_Button.style.unityFont =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        #endregion

        #region Settings
        menu_UIConnector.settings_Button.text =
            LanguageTextsData.settings[SettingsData.currentLanguageIndex];
        menu_UIConnector.settings_Button.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        menu_UIConnector.settings_Button.style.unityFont =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        #endregion

        #region Exit
        menu_UIConnector.exit_Button.text =
            LanguageTextsData.exit[SettingsData.currentLanguageIndex];
        menu_UIConnector.exit_Button.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        menu_UIConnector.exit_Button.style.unityFont =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        #endregion
    }

    private void OnFontSizeChanged()
    {
        #region Resume
        menu_UIConnector.resume_Button.style.fontSize =
            LanguageTextsData.fontSize_CategoryAverage[SettingsData.currentFontSizeIndex];
        #endregion

        #region Levels
        menu_UIConnector.levels_Button.style.fontSize =
            LanguageTextsData.fontSize_CategoryAverage[SettingsData.currentFontSizeIndex];
        #endregion

        #region InventoryShop
        menu_UIConnector.inventoryShop_Button.style.fontSize =
            LanguageTextsData.fontSize_CategoryAverage[SettingsData.currentFontSizeIndex];
        #endregion

        #region Settings
        menu_UIConnector.settings_Button.style.fontSize =
            LanguageTextsData.fontSize_CategoryAverage[SettingsData.currentFontSizeIndex];
        #endregion

        #region Exit
        menu_UIConnector.exit_Button.style.fontSize =
            LanguageTextsData.fontSize_CategoryAverage[SettingsData.currentFontSizeIndex];
        #endregion
    }

    #endregion


    private void OnResume_ButtonSelected(ClickEvent clickEvent)
    {
        //menu_UIConnector.menu_VisualElement.style.display = DisplayStyle.Flex;
        menu_UIConnector.pageHolder_VisualElement.style.display = DisplayStyle.None;
        EventsManager.InvokeOnResumeLevel();
    }

    private void OnLevels_ButtonSelected(ClickEvent clickEvent)
    {
        menu_UIConnector.resume_Button.style.display = DisplayStyle.None;
        menu_UIConnector.SwitchPage(menu_UIConnector.levelsPage_VisualElement);
        EventsManager.InvokeOnStopLevel();
    }

    private void OnInventoryShop_ButtonSelected(ClickEvent clickEvent)
    {
        menu_UIConnector.resume_Button.style.display = DisplayStyle.None;
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
