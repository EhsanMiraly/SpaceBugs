using UnityEngine;
using UnityEngine.UIElements;


public class SettingsPage : MonoBehaviour
{
    PanelRenderer panelRenderer;
    Menu menu;


    VisualTreeAsset previousNextSelector_VisualTreeAsset;
    VisualTreeAsset sound_VisualTreeAsset;


    private VisualElement settingsPage_VisualElement;
    private VisualElement back_TemplateContainer;
    private ScrollView settings_ScrollView;


    #region Language_Setting
    VisualElement language_VisualElement;
    VisualElement language_ChevronLeft_VisualElement;
    Label language_Label;
    VisualElement language_ChevronRight_VisualElement;
    #endregion

    #region FontSize_Setting
    VisualElement fontSize_VisualElement;
    VisualElement fontSize_ChevronLeft_VisualElement;
    Label fontSize_Label;
    VisualElement fontSize_ChevronRight_VisualElement;
    #endregion

    #region BackgroundMusic_Setting
    VisualElement backgroundMusic_VisualElement;
    Label backgroundMusic_WhatAmI_Label;
    VisualElement backgroundMusic_CheckMark_VisualElement;
    VisualElement backgroundMusic_CheckMark_Foreground_VisualElement;
    VisualElement backgroundMusic_Minus_VisualElement;
    VisualElement backgroundMusic_InvisibleForeground_VisualElement;
    VisualElement backgroundMusic_Plus_VisualElement;
    #endregion

    #region SoundEffects_Setting
    VisualElement soundEffects_VisualElement;
    Label soundEffects_WhatAmI_Label;
    VisualElement soundEffects_CheckMark_VisualElement;
    VisualElement soundEffects_CheckMark_Foreground_VisualElement;
    VisualElement soundEffects_Minus_VisualElement;
    VisualElement soundEffects_InvisibleForeground_VisualElement;
    VisualElement soundEffects_Plus_VisualElement;
    #endregion


    private void OnEnable()
    {
        panelRenderer = GetComponent<PanelRenderer>();
        panelRenderer.RegisterUIReloadCallback(OnUIReloadCallback);
        menu = GetComponent<Menu>();

        previousNextSelector_VisualTreeAsset =
            Resources.Load<VisualTreeAsset>("UI/Basic_Templates/PreviousNextSelector/previousNextSelector_Template");
        sound_VisualTreeAsset =
            Resources.Load<VisualTreeAsset>("UI/Basic_Templates/Sound/Sound_Template");
    }

    private void OnDisable()
    {
        RemoveFunctionality();

        DisconnctEvents();

        panelRenderer.UnregisterUIReloadCallback(OnUIReloadCallback);
    }

    private void OnUIReloadCallback(PanelRenderer panelRenderer, VisualElement root)
    {
        settingsPage_VisualElement = root.Q<VisualElement>("SettingsPage_VisualElement");
        back_TemplateContainer = settingsPage_VisualElement.Q<VisualElement>("Back_TemplateContainer");
        UI_Utilities.FixBackButtonSize(back_TemplateContainer);
        settings_ScrollView = settingsPage_VisualElement.Q<ScrollView>("Settings_ScrollView");
        ScrollViewController.InitializeScrollView(settings_ScrollView);

        FillSettings_ScrollView();

        AddFunctionality();
        ConnctEvents();
    }



    #region Functionality
    private void AddFunctionality()
    {
        back_TemplateContainer.RegisterCallback<ClickEvent>(OnBackSelected);
    }

    private void RemoveFunctionality()
    {
        back_TemplateContainer.UnregisterCallback<ClickEvent>(OnBackSelected);
    }

    private void OnBackSelected(ClickEvent clickEvent)
    {
        menu.SwitchPage(menu.mainPage_VisualElement);
    }

    #endregion


    #region Events Manager

    private void ConnctEvents()
    {
        EventsManager.OnLanguageChanged_Event += OnLanguageChanged;
        EventsManager.OnFontSizeChanged_Event += OnFontSizeChanged;
    }

    private void DisconnctEvents()
    {
        EventsManager.OnLanguageChanged_Event -= OnLanguageChanged;
        EventsManager.OnFontSizeChanged_Event -= OnFontSizeChanged;
    }


    private void OnLanguageChanged()
    {
        #region Language
        language_Label.text =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].language;
        language_Label.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        language_Label.style.unityFont =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        #endregion

        #region FontSize
        fontSize_Label.text =
            LanguageTextsData.fontSize_Text[SettingsData.currentFontSizeIndex].
            FontSizeLanguage[SettingsData.currentLanguageIndex];
        fontSize_Label.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        fontSize_Label.style.unityFont =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        #endregion

        #region BackgroundMusic
        backgroundMusic_WhatAmI_Label.text =
            LanguageTextsData.backgroundMusic[SettingsData.currentLanguageIndex];
        backgroundMusic_WhatAmI_Label.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        backgroundMusic_WhatAmI_Label.style.unityFont =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        #endregion

        #region SoundEffects
        soundEffects_WhatAmI_Label.text =
            LanguageTextsData.soundEffects[SettingsData.currentLanguageIndex];
        soundEffects_WhatAmI_Label.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        soundEffects_WhatAmI_Label.style.unityFont =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        #endregion
    }

    private void OnFontSizeChanged()
    {
        #region Language
        language_Label.style.fontSize =
            LanguageTextsData.fontSize_CategoryBig[SettingsData.currentFontSizeIndex];
        #endregion

        #region FontSize
        fontSize_Label.style.fontSize =
            LanguageTextsData.fontSize_CategoryBig[SettingsData.currentFontSizeIndex];
        fontSize_Label.text =
            LanguageTextsData.fontSize_Text[SettingsData.currentFontSizeIndex].
            FontSizeLanguage[SettingsData.currentLanguageIndex];
        #endregion

        #region BackgroundMusic
        backgroundMusic_WhatAmI_Label.style.fontSize =
            LanguageTextsData.fontSize_CategoryBig[SettingsData.currentFontSizeIndex];
        #endregion

        #region SoundEffects
        soundEffects_WhatAmI_Label.style.fontSize =
            LanguageTextsData.fontSize_CategoryBig[SettingsData.currentFontSizeIndex];
        #endregion
    }

    private void OnBackgroundMusicChanged()
    {
        if (SettingsData.isBackgroundMusicOn)
        {
            backgroundMusic_CheckMark_Foreground_VisualElement.style.display = DisplayStyle.Flex;
        }
        else
        {
            backgroundMusic_CheckMark_Foreground_VisualElement.style.display = DisplayStyle.None;
        }
        backgroundMusic_InvisibleForeground_VisualElement.style.width =
                 Length.Percent(SettingsData.backgroundMusicVolume * 100);
    }

    private void OnSoundEffectsChanged()
    {
        if (SettingsData.isSoundEffectsOn)
        {
            soundEffects_CheckMark_Foreground_VisualElement.style.display = DisplayStyle.Flex;
        }
        else
        {
            soundEffects_CheckMark_Foreground_VisualElement.style.display = DisplayStyle.None;
        }
        soundEffects_InvisibleForeground_VisualElement.style.width =
                 Length.Percent(SettingsData.soundEffectsVolume * 100);
    }

    #endregion


    #region UI Utilities

    public void FillSettings_ScrollView()
    {
        Add_Language_Setting();
        Add_FontSize_Setting();
        Add_BackgroundMusic_Setting();
        Add_SoundEffects_Setting();

        OnLanguageChanged();
        OnFontSizeChanged();
        OnBackgroundMusicChanged();
        OnSoundEffectsChanged();
    }
    #endregion



    #region Language

    public void Add_Language_Setting()
    {
        language_VisualElement = previousNextSelector_VisualTreeAsset.Instantiate();

        UI_Utilities.FixSettingItemSizeOneRow(language_VisualElement);

        language_ChevronLeft_VisualElement = language_VisualElement.Q<VisualElement>("ChevronLeft_VisualElement");
        UI_Utilities.FixElementSize(language_ChevronLeft_VisualElement.Q<VisualElement>("ChevronLeft_TemplateContainer"));
        language_Label = language_VisualElement.Q<Label>("Option_Label");
        language_ChevronRight_VisualElement = language_VisualElement.Q<VisualElement>("ChevronRight_VisualElement");
        UI_Utilities.FixElementSize(language_ChevronRight_VisualElement.Q<VisualElement>("ChevronRight_TemplateContainer"));

        settings_ScrollView.Add(language_VisualElement);

        language_ChevronLeft_VisualElement.RegisterCallback<ClickEvent>(OnLanguageChevronLeftSelected);
        language_ChevronRight_VisualElement.RegisterCallback<ClickEvent>(OnLanguageChevronRightSelected);

    }

    private void OnLanguageChevronLeftSelected(ClickEvent clickEvent)
    {
        SettingsData.currentLanguageIndex--;
        if (SettingsData.currentLanguageIndex < 0)
        {
            SettingsData.currentLanguageIndex = LanguageTextsData.languages.Count - 1;
        }
        EventsManager.InvokeOnLanguageChanged();
        Settings_SaveSystem.Save_Settings();//Delete
    }

    private void OnLanguageChevronRightSelected(ClickEvent clickEvent)
    {
        SettingsData.currentLanguageIndex++;
        if (SettingsData.currentLanguageIndex > LanguageTextsData.languages.Count - 1)
        {
            SettingsData.currentLanguageIndex = 0;
        }
        EventsManager.InvokeOnLanguageChanged();
        Settings_SaveSystem.Save_Settings();//Delete
    }

    #endregion

    #region FontSize

    public void Add_FontSize_Setting()
    {
        fontSize_VisualElement = previousNextSelector_VisualTreeAsset.Instantiate();

        UI_Utilities.FixSettingItemSizeOneRow(fontSize_VisualElement);

        fontSize_ChevronLeft_VisualElement = fontSize_VisualElement.Q<VisualElement>("ChevronLeft_VisualElement");
        UI_Utilities.FixElementSize(fontSize_ChevronLeft_VisualElement.Q<VisualElement>("ChevronLeft_TemplateContainer"));
        fontSize_Label = fontSize_VisualElement.Q<Label>("Option_Label");
        fontSize_ChevronRight_VisualElement = fontSize_VisualElement.Q<VisualElement>("ChevronRight_VisualElement");
        UI_Utilities.FixElementSize(fontSize_ChevronRight_VisualElement.Q<VisualElement>("ChevronRight_TemplateContainer"));

        settings_ScrollView.Add(fontSize_VisualElement);

        fontSize_ChevronLeft_VisualElement.RegisterCallback<ClickEvent>(OnFontSizeChevronLeftSelected);
        fontSize_ChevronRight_VisualElement.RegisterCallback<ClickEvent>(OnFontSizeChevronRightSelected);

    }

    private void OnFontSizeChevronLeftSelected(ClickEvent clickEvent)
    {
        SettingsData.currentFontSizeIndex--;
        if (SettingsData.currentFontSizeIndex < 0)
        {
            SettingsData.currentFontSizeIndex = LanguageTextsData.fontSize_Text.Count - 1;
        }
        EventsManager.InvokeOnFontSizeChanged();
        Settings_SaveSystem.Save_Settings();//Delete
    }

    private void OnFontSizeChevronRightSelected(ClickEvent clickEvent)
    {
        SettingsData.currentFontSizeIndex++;
        if (SettingsData.currentFontSizeIndex > LanguageTextsData.fontSize_Text.Count - 1)
        {
            SettingsData.currentFontSizeIndex = 0;
        }
        EventsManager.InvokeOnFontSizeChanged();
        Settings_SaveSystem.Save_Settings();//Delete
    }

    #endregion

    #region BackgroundMusic

    public void Add_BackgroundMusic_Setting()
    {
        backgroundMusic_VisualElement = sound_VisualTreeAsset.Instantiate();

        UI_Utilities.FixSettingItemTwoRow(backgroundMusic_VisualElement);

        backgroundMusic_WhatAmI_Label =
            backgroundMusic_VisualElement.Q<Label>("WhatAmI_Label");
        backgroundMusic_CheckMark_VisualElement =
            backgroundMusic_VisualElement.Q<VisualElement>("CheckMark_VisualElement");
        UI_Utilities.FixElementSize(backgroundMusic_CheckMark_VisualElement.Q<VisualElement>("CheckMark_Template"));
        backgroundMusic_CheckMark_Foreground_VisualElement =
            backgroundMusic_CheckMark_VisualElement.Q<VisualElement>("Foreground_VisualElement");
        backgroundMusic_Minus_VisualElement =
            backgroundMusic_VisualElement.Q<VisualElement>("Minus_VisualElement");
        UI_Utilities.FixElementSize(backgroundMusic_Minus_VisualElement.Q<VisualElement>("Minus_Template"));
        backgroundMusic_InvisibleForeground_VisualElement =
            backgroundMusic_VisualElement.Q<VisualElement>("InvisibleForeground_VisualElement");
        backgroundMusic_Plus_VisualElement =
            backgroundMusic_VisualElement.Q<VisualElement>("Plus_VisualElement");
        UI_Utilities.FixElementSize(backgroundMusic_Plus_VisualElement.Q<VisualElement>("Plus_Template"));

        settings_ScrollView.Add(backgroundMusic_VisualElement);

        backgroundMusic_CheckMark_VisualElement.
            RegisterCallback<ClickEvent>(OnBackgroundMusicCheckMarkSelected);
        backgroundMusic_Minus_VisualElement.RegisterCallback<ClickEvent>(OnBackgroundMusicMinusSelected);
        backgroundMusic_Plus_VisualElement.RegisterCallback<ClickEvent>(OnBackgroundMusicPlusSelected);
    }

    public void OnBackgroundMusicCheckMarkSelected(ClickEvent clickEvent)
    {
        SettingsData.isBackgroundMusicOn = !SettingsData.isBackgroundMusicOn;

        EventsManager.InvokeOnBackgroundMusicChanged();
        Settings_SaveSystem.Save_Settings();//Delete

        if (SettingsData.isBackgroundMusicOn)
        {
            backgroundMusic_CheckMark_Foreground_VisualElement.style.display = DisplayStyle.Flex;
        }
        else
        {
            backgroundMusic_CheckMark_Foreground_VisualElement.style.display = DisplayStyle.None;
        }
    }

    public void OnBackgroundMusicMinusSelected(ClickEvent clickEvent)
    {
        SettingsData.backgroundMusicVolume -= 0.1f;
        if (SettingsData.backgroundMusicVolume < 0)
            SettingsData.backgroundMusicVolume = 0;
        EventsManager.InvokeOnBackgroundMusicChanged();
        Settings_SaveSystem.Save_Settings();//Delete

        backgroundMusic_InvisibleForeground_VisualElement.style.width =
         Length.Percent(SettingsData.backgroundMusicVolume * 100);
    }

    public void OnBackgroundMusicPlusSelected(ClickEvent clickEvent)
    {
        SettingsData.backgroundMusicVolume += 0.1f;
        if (SettingsData.backgroundMusicVolume > 1)
            SettingsData.backgroundMusicVolume = 1;
        EventsManager.InvokeOnBackgroundMusicChanged();
        Settings_SaveSystem.Save_Settings();//Delete

        backgroundMusic_InvisibleForeground_VisualElement.style.width
         = Length.Percent(SettingsData.backgroundMusicVolume * 100);
    }

    #endregion

    #region SoundEffects

    public void Add_SoundEffects_Setting()
    {
        soundEffects_VisualElement = sound_VisualTreeAsset.Instantiate();

        UI_Utilities.FixSettingItemTwoRow(soundEffects_VisualElement);

        soundEffects_WhatAmI_Label =
            soundEffects_VisualElement.Q<Label>("WhatAmI_Label");
        soundEffects_CheckMark_VisualElement =
            soundEffects_VisualElement.Q<VisualElement>("CheckMark_VisualElement");
        UI_Utilities.FixElementSize(soundEffects_CheckMark_VisualElement.Q<VisualElement>("CheckMark_Template"));
        soundEffects_CheckMark_Foreground_VisualElement =
            soundEffects_CheckMark_VisualElement.Q<VisualElement>("Foreground_VisualElement");
        soundEffects_Minus_VisualElement =
            soundEffects_VisualElement.Q<VisualElement>("Minus_VisualElement");
        UI_Utilities.FixElementSize(soundEffects_Minus_VisualElement.Q<VisualElement>("Minus_Template"));
        soundEffects_InvisibleForeground_VisualElement =
            soundEffects_VisualElement.Q<VisualElement>("InvisibleForeground_VisualElement");
        soundEffects_Plus_VisualElement =
            soundEffects_VisualElement.Q<VisualElement>("Plus_VisualElement");
        UI_Utilities.FixElementSize(soundEffects_Plus_VisualElement.Q<VisualElement>("Plus_Template"));

        settings_ScrollView.Add(soundEffects_VisualElement);

        soundEffects_CheckMark_VisualElement.
            RegisterCallback<ClickEvent>(OnSoundEffectsCheckMarkSelected);
        soundEffects_Minus_VisualElement.RegisterCallback<ClickEvent>(OnSoundEffectsMinusSelected);
        soundEffects_Plus_VisualElement.RegisterCallback<ClickEvent>(OnSoundEffectsPlusSelected);
    }

    public void OnSoundEffectsCheckMarkSelected(ClickEvent clickEvent)
    {
        SettingsData.isSoundEffectsOn = !SettingsData.isSoundEffectsOn;

        if (SettingsData.isSoundEffectsOn)
        {
            soundEffects_CheckMark_Foreground_VisualElement.style.display = DisplayStyle.Flex;
        }
        else
        {
            soundEffects_CheckMark_Foreground_VisualElement.style.display = DisplayStyle.None;
        }

        Settings_SaveSystem.Save_Settings();//Delete
    }

    public void OnSoundEffectsMinusSelected(ClickEvent clickEvent)
    {
        SettingsData.soundEffectsVolume -= 0.1f;
        if (SettingsData.soundEffectsVolume < 0)
            SettingsData.soundEffectsVolume = 0;

        soundEffects_InvisibleForeground_VisualElement.style.width =
         Length.Percent(SettingsData.soundEffectsVolume * 100);

        Settings_SaveSystem.Save_Settings();//Delete
    }

    public void OnSoundEffectsPlusSelected(ClickEvent clickEvent)
    {
        SettingsData.soundEffectsVolume += 0.1f;
        if (SettingsData.soundEffectsVolume > 1)
            SettingsData.soundEffectsVolume = 1;

        soundEffects_InvisibleForeground_VisualElement.style.width
         = Length.Percent(SettingsData.soundEffectsVolume * 100);

        Settings_SaveSystem.Save_Settings();//Delete
    }

    #endregion

}
