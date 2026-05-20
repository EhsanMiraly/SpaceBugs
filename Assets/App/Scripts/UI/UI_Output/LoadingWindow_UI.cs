using System;
using UnityEngine;
using UnityEngine.UIElements;

public class LoadingWindow_UI : IDisposable
{
    GameObject parent;

    VisualTreeAsset loadingWindow_UI_Template;

    UIDocument uIDocument;
    VisualElement root;

    VisualElement sliderForeground_VisualElement;



    public LoadingWindow_UI(GameObject parent)
    {
        this.parent = parent;
        parent.name = "LoadingWindow_UI";
        parent.layer = LayerMask.NameToLayer("UI");

        loadingWindow_UI_Template = Resources.Load<VisualTreeAsset>("UI/LoadingWindow_UI_Template");

        uIDocument = parent.AddComponent<UIDocument>();
        uIDocument.panelSettings = Resources.Load<PanelSettings>("UI/PopUpWindows_UI_PanelSettings");
        uIDocument.visualTreeAsset = loadingWindow_UI_Template;
        uIDocument.sortingOrder = 100;

        root = uIDocument.rootVisualElement;

        sliderForeground_VisualElement = root.Q<VisualElement>("SliderForeground_VisualElement");

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
            loadingWindow_UI_Template = null;
            uIDocument = null;
            root = null;
            sliderForeground_VisualElement = null;
        }
    }
}
