using System;
using Unity.InferenceEngine;
using UnityEngine;
using UnityEngine.UIElements;

public class LoadingPage_UI : IDisposable
{
    GameObject parent;

    VisualTreeAsset loading_Template;

    UIDocument uIDocument;
    VisualElement root;

    VisualElement sliderForeground_VisualElement;



    public LoadingPage_UI(GameObject parent)
    {
        this.parent = parent;
        parent.name = "LoadingPage_UI";
        parent.layer = LayerMask.NameToLayer("UI");

        loading_Template = Resources.Load<VisualTreeAsset>("UI/Loading_Template");

        uIDocument = parent.AddComponent<UIDocument>();
        uIDocument.panelSettings = Resources.Load<PanelSettings>("UI/Screen_PanelSettings");
        uIDocument.visualTreeAsset = loading_Template;
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
            loading_Template = null;
            uIDocument = null;
            root = null;
            sliderForeground_VisualElement = null;
        }
    }
}
