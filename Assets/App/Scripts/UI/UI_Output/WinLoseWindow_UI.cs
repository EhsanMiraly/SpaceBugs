using System;
using UnityEngine;
using UnityEngine.UIElements;

public class WinLoseWindow_UI
{
    bool winOrLose;

    GameObject parent;

    VisualTreeAsset winLoseWindow_Template;

    UIDocument uIDocument;
    VisualElement root;

    Label winLose_Label;
    VisualElement oK_Button_TemplateContainer;
    Label oK_Label;

    Menu menu;


    public WinLoseWindow_UI(GameObject parent, bool winOrLose)
    {
        this.parent = parent;
        this.winOrLose = winOrLose;

        parent.name = "WinLoseWindow_UI";
        parent.layer = LayerMask.NameToLayer("UI");

        winLoseWindow_Template =
            Resources.Load<VisualTreeAsset>("UI/Basic_Templates/WinLosePopUp/WinLosePopUp_Template");

        uIDocument = parent.AddComponent<UIDocument>();
        uIDocument.panelSettings = Resources.Load<PanelSettings>("UI/PopUpWindows_UI_PanelSettings");
        uIDocument.visualTreeAsset = winLoseWindow_Template;
        uIDocument.sortingOrder = 100;

        root = uIDocument.rootVisualElement;

        ScreenSafeArea.RemoveUnSafeAreaFromUI(root);

        winLose_Label = root.Q<Label>("WinLose_Label");
        oK_Button_TemplateContainer = root.Q<VisualElement>("OK_Button_TemplateContainer");
        oK_Label = oK_Button_TemplateContainer.Q<Label>();

        #region WinLose Label
        if (winOrLose)
        {
            winLose_Label.text =
                LanguageTextsData.win[SettingsData.currentLanguageIndex];
        }
        else
        {
            winLose_Label.text =
                LanguageTextsData.lose[SettingsData.currentLanguageIndex];
        }
        winLose_Label.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        winLose_Label.style.unityFont =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        winLose_Label.style.fontSize =
            LanguageTextsData.fontSize_CategoryAverage[SettingsData.currentFontSizeIndex];
        #endregion

        #region OK Button
        oK_Label.text =
            LanguageTextsData.ok[SettingsData.currentLanguageIndex];
        oK_Label.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        oK_Label.style.unityFont =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        oK_Label.style.fontSize =
            LanguageTextsData.fontSize_CategoryAverage[SettingsData.currentFontSizeIndex];
        #endregion

        menu = UnityEngine.Object.FindAnyObjectByType<Menu>().GetComponent<Menu>();

        oK_Button_TemplateContainer.RegisterCallback<ClickEvent>(evt =>
        {
            if (winOrLose)
            {
                EventsManager.InvokeOnWinLevel();
            }
            else
            {
                EventsManager.InvokeOnLoseLevel();
            }
            EventsManager.InvokeOnStopLevel();
            menu.SwitchPage(menu.mainPage_VisualElement);
            Dispose();
        });
    }



    private void Dispose()
    {
        if (parent != null)
        {
            UnityEngine.Object.Destroy(parent);
            parent = null;
            winLoseWindow_Template = null;
            uIDocument = null;
            root = null;
            winLose_Label = null;
            oK_Button_TemplateContainer = null;
            menu = null;
        }
    }
}
