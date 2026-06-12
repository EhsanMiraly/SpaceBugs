using System;
using UnityEngine;
using UnityEngine.UIElements;

public class LoadingWindow_UI : IDisposable
{
    GameObject parent;

    VisualTreeAsset loadingWindow_Template;

    UIDocument uIDocument;
    VisualElement root;
    Label loading_Label;

    VisualElement sliderForeground_VisualElement;



    public LoadingWindow_UI(GameObject parent)
    {
        this.parent = parent;
        parent.name = "LoadingWindow_UI";
        parent.layer = LayerMask.NameToLayer("UI");

        loadingWindow_Template = Resources.Load<VisualTreeAsset>("UI/Basic_Templates/LoadingWindow/LoadingWindow_Template");

        uIDocument = parent.AddComponent<UIDocument>();
        uIDocument.panelSettings = Resources.Load<PanelSettings>("UI/PopUpWindows_UI_PanelSettings");
        uIDocument.visualTreeAsset = loadingWindow_Template;
        uIDocument.sortingOrder = 100;

        root = uIDocument.rootVisualElement;

        ScreenSafeArea.RemoveUnSafeAreaFromUI(root);

        loading_Label = root.Q<Label>("Loading_Label");
        loading_Label.text =
            LanguageTextsData.loading[SettingsData.currentLanguageIndex];
        loading_Label.languageDirection =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].languageDirection;
        loading_Label.style.unityFont =
            LanguageTextsData.languages[SettingsData.currentLanguageIndex].font;
        loading_Label.style.fontSize =
            LanguageTextsData.fontSize_CategoryAverage[SettingsData.currentFontSizeIndex];

        sliderForeground_VisualElement = root.Q<VisualElement>("Foreground_VisualElement");

        SetProgress(0);
    }

    public void SetProgress(int progress)
    {
        sliderForeground_VisualElement.style.width = Length.Percent(progress);
    }


    public void Dispose()
    {
        if (parent != null)
        {
            UnityEngine.Object.Destroy(parent);
            parent = null;
            loadingWindow_Template = null;
            uIDocument = null;
            root = null;
            sliderForeground_VisualElement = null;
        }
    }
}
