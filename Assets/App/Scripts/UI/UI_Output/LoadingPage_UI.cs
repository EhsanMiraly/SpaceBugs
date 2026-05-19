using System;
using Unity.InferenceEngine;
using UnityEngine;
using UnityEngine.UIElements;

public class LoadingPage_UI : IDisposable
{
    GameObject parent;

    VisualTreeAsset loadingPage_UI_Template;

    UIDocument uIDocument;
    VisualElement root;

    VisualElement sliderForeground_VisualElement;



    public LoadingPage_UI(GameObject parent)
    {
        this.parent = parent;
        parent.name = "LoadingPage_UI";
        parent.layer = LayerMask.NameToLayer("UI");

        loadingPage_UI_Template = Resources.Load<VisualTreeAsset>("UI/LoadingPage_UI_Template");

        uIDocument = parent.AddComponent<UIDocument>();
        uIDocument.panelSettings = Resources.Load<PanelSettings>("UI/LoadingPage_UI_PanelSettings");
        uIDocument.visualTreeAsset = loadingPage_UI_Template;
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
            loadingPage_UI_Template = null;
            uIDocument = null;
            root = null;
            sliderForeground_VisualElement = null;
        }
    }
}
