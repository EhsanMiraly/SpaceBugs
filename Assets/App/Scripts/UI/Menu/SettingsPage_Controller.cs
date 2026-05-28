using UnityEngine;
using UnityEngine.UIElements;

public class SettingsPage_Controller : MonoBehaviour
{
    Menu_UIConnector menu_UIConnector;

    VisualTreeAsset language_VisualTreeAsset;
    VisualTreeAsset sound_VisualTreeAsset;



    #region Language_Setting
    VisualElement language_VisualElement;
    VisualElement chevronLeft_VisualElement;
    Label language_Label;
    VisualElement chevronRight_VisualElement;
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



    public void Initialize(Menu_UIConnector menu_UIConnector)
    {
        LanguageEventManager.OnLanguageChanged_Event += OnLanguageChanged;

        this.menu_UIConnector = menu_UIConnector;

        language_VisualTreeAsset =
            Resources.Load<VisualTreeAsset>("UI/Settings_Templates/Language/Language_Template");
        sound_VisualTreeAsset =
            Resources.Load<VisualTreeAsset>("UI/Settings_Templates/Sound/Sound_Template");

        menu_UIConnector.backButton_InSettingsPage_Template.
            RegisterCallback<ClickEvent>(OnBackButton_InSettingsPageSelected);

        FillSettings_ScrollView();
    }


    private void OnBackButton_InSettingsPageSelected(ClickEvent clickEvent)
    {
        menu_UIConnector.SwitchPage(menu_UIConnector.mainPage_VisualElement);
    }

    public void FillSettings_ScrollView()
    {
        Add_Language_Setting();
        Add_BackgroundMusic_Setting();
        Add_SoundEffects_Setting();
    }


    #region Events Handler
    private void OnLanguageChanged(object o, LanguageData_EventArgs languageData_EventArgs)
    {
        #region Language
        //Change text
        language_Label.text =
            SettingsData.languages[SettingsData.currentLanguageIndex].language;
        //Change RTL
        language_Label.languageDirection =
            SettingsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        //Change Font
        //language_Label.style.font
        //Change Size?????
        #endregion

    }
    #endregion


    #region Language

    public void Add_Language_Setting()
    {
        language_VisualElement = language_VisualTreeAsset.Instantiate();

        chevronLeft_VisualElement = language_VisualElement.Q<VisualElement>("ChevronLeft_VisualElement");
        language_Label = language_VisualElement.Q<Label>("Language_Label");
        chevronRight_VisualElement = language_VisualElement.Q<VisualElement>("ChevronRight_VisualElement");

        language_VisualElement.style.width = Length.Percent(100);
        language_VisualElement.style.height = 300;

        language_Label.text =
            SettingsData.languages[SettingsData.currentLanguageIndex].language;
        language_Label.languageDirection =
            SettingsData.languages[SettingsData.currentLanguageIndex].languageDirection;

        menu_UIConnector.settings_ScrollView.Add(language_VisualElement);

        chevronLeft_VisualElement.RegisterCallback<ClickEvent>(OnLanguageChevronLeftSelected);
        chevronRight_VisualElement.RegisterCallback<ClickEvent>(OnLanguageChevronRightSelected);

    }

    private void OnLanguageChevronLeftSelected(ClickEvent clickEvent)
    {
        SettingsData.currentLanguageIndex--;
        if (SettingsData.currentLanguageIndex < 0)
        {
            SettingsData.currentLanguageIndex = SettingsData.languages.Count - 1;
        }
        LanguageEventManager.InvokeOnLanguageChanged(this.gameObject,
            new LanguageData_EventArgs(SettingsData.currentLanguageIndex));
    }

    private void OnLanguageChevronRightSelected(ClickEvent clickEvent)
    {
        SettingsData.currentLanguageIndex++;
        if (SettingsData.currentLanguageIndex > SettingsData.languages.Count - 1)
        {
            SettingsData.currentLanguageIndex = 0;
        }
        LanguageEventManager.InvokeOnLanguageChanged(this.gameObject,
            new LanguageData_EventArgs(SettingsData.currentLanguageIndex));
    }

    #endregion


    #region BackgroundMusic

    public void Add_BackgroundMusic_Setting()
    {
        backgroundMusic_VisualElement = sound_VisualTreeAsset.Instantiate();

        backgroundMusic_WhatAmI_Label =
            backgroundMusic_VisualElement.Q<Label>("WhatAmI_Label");
        backgroundMusic_CheckMark_VisualElement =
            backgroundMusic_VisualElement.Q<VisualElement>("CheckMark_VisualElement");
        backgroundMusic_CheckMark_Foreground_VisualElement =
            backgroundMusic_CheckMark_VisualElement.Q<VisualElement>("Foreground_VisualElement");
        backgroundMusic_Minus_VisualElement =
            backgroundMusic_VisualElement.Q<VisualElement>("Minus_VisualElement");
        backgroundMusic_InvisibleForeground_VisualElement =
            backgroundMusic_VisualElement.Q<VisualElement>("InvisibleForeground_VisualElement");
        backgroundMusic_Plus_VisualElement =
            backgroundMusic_VisualElement.Q<VisualElement>("Plus_VisualElement");

        backgroundMusic_VisualElement.style.width = Length.Percent(100);
        backgroundMusic_VisualElement.style.height = 300;
        backgroundMusic_WhatAmI_Label.text = "Background music";
        menu_UIConnector.settings_ScrollView.Add(backgroundMusic_VisualElement);

        backgroundMusic_CheckMark_VisualElement.
            RegisterCallback<ClickEvent>(OnBackgroundMusicCheckMarkSelected);
        backgroundMusic_Minus_VisualElement.RegisterCallback<ClickEvent>(OnBackgroundMusicMinusSelected);
        backgroundMusic_Plus_VisualElement.RegisterCallback<ClickEvent>(OnBackgroundMusicPlusSelected);
    }

    public void OnBackgroundMusicCheckMarkSelected(ClickEvent clickEvent)
    {
        SettingsData.isBackgroundMusicOn = !SettingsData.isBackgroundMusicOn;

        SoundsEventManager.InvokeOnBackgroundMusicChanged(this.gameObject,
            new SoundData_EventArgs(SettingsData.isBackgroundMusicOn, SettingsData.backgroundMusicVolume));

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
        SoundsEventManager.InvokeOnBackgroundMusicChanged(this.gameObject,
            new SoundData_EventArgs(SettingsData.isBackgroundMusicOn, SettingsData.backgroundMusicVolume));

        backgroundMusic_InvisibleForeground_VisualElement.style.width =
         Length.Percent(SettingsData.backgroundMusicVolume * 100);
    }

    public void OnBackgroundMusicPlusSelected(ClickEvent clickEvent)
    {
        SettingsData.backgroundMusicVolume += 0.1f;
        if (SettingsData.backgroundMusicVolume > 1)
            SettingsData.backgroundMusicVolume = 1;
        SoundsEventManager.InvokeOnBackgroundMusicChanged(this.gameObject,
            new SoundData_EventArgs(SettingsData.isBackgroundMusicOn, SettingsData.backgroundMusicVolume));

        backgroundMusic_InvisibleForeground_VisualElement.style.width
         = Length.Percent(SettingsData.backgroundMusicVolume * 100);
    }

    #endregion

    #region SoundEffects

    public void Add_SoundEffects_Setting()
    {
        soundEffects_VisualElement = sound_VisualTreeAsset.Instantiate();

        soundEffects_WhatAmI_Label =
            soundEffects_VisualElement.Q<Label>("WhatAmI_Label");
        soundEffects_CheckMark_VisualElement =
            soundEffects_VisualElement.Q<VisualElement>("CheckMark_VisualElement");
        soundEffects_CheckMark_Foreground_VisualElement =
            soundEffects_CheckMark_VisualElement.Q<VisualElement>("Foreground_VisualElement");
        soundEffects_Minus_VisualElement =
            soundEffects_VisualElement.Q<VisualElement>("Minus_VisualElement");
        soundEffects_InvisibleForeground_VisualElement =
            soundEffects_VisualElement.Q<VisualElement>("InvisibleForeground_VisualElement");
        soundEffects_Plus_VisualElement =
            soundEffects_VisualElement.Q<VisualElement>("Plus_VisualElement");

        soundEffects_VisualElement.style.width = Length.Percent(100);
        soundEffects_VisualElement.style.height = 300;
        soundEffects_WhatAmI_Label.text = "Sound effects";
        menu_UIConnector.settings_ScrollView.Add(soundEffects_VisualElement);

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
    }

    public void OnSoundEffectsMinusSelected(ClickEvent clickEvent)
    {
        SettingsData.soundEffectsVolume -= 0.1f;
        if (SettingsData.soundEffectsVolume < 0)
            SettingsData.soundEffectsVolume = 0;

        soundEffects_InvisibleForeground_VisualElement.style.width =
         Length.Percent(SettingsData.soundEffectsVolume * 100);
    }

    public void OnSoundEffectsPlusSelected(ClickEvent clickEvent)
    {
        SettingsData.soundEffectsVolume += 0.1f;
        if (SettingsData.soundEffectsVolume > 1)
            SettingsData.soundEffectsVolume = 1;

        soundEffects_InvisibleForeground_VisualElement.style.width
         = Length.Percent(SettingsData.soundEffectsVolume * 100);
    }

    #endregion

}
