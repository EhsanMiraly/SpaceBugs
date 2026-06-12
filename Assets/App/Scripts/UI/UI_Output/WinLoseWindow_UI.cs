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
    Button oK_Button;

    Menu_UIConnector menu_UIConnector;


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
        oK_Button = root.Q<Button>("OK_Button");

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
        oK_Button.text =
            LanguageTextsData.ok[SettingsData.currentLanguageIndex];
        oK_Button.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        oK_Button.style.unityFont =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        oK_Button.style.fontSize =
            LanguageTextsData.fontSize_CategoryAverage[SettingsData.currentFontSizeIndex];
        #endregion

        menu_UIConnector = UnityEngine.Object.FindAnyObjectByType<Menu_UIConnector>().GetComponent<Menu_UIConnector>();

        oK_Button.RegisterCallback<ClickEvent>(evt =>
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
            menu_UIConnector.InitialPage();
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
            oK_Button = null;
            menu_UIConnector = null;
        }
    }
}
