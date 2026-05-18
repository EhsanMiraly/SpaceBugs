using System;
using UnityEngine;
using UnityEngine.UIElements;

public class Loading_UI : MonoBehaviour
{
    UIDocument uIDocument;
    VisualElement root;

    VisualElement sliderForeground_VisualElement;

    public void Initialize()
    {
        ConnectUI();
        SetProgress(0);
    }

    public void ConnectUI()
    {
        uIDocument = GetComponent<UIDocument>();
        root = uIDocument.rootVisualElement;

        sliderForeground_VisualElement = root.Q<VisualElement>("SliderForeground_VisualElement");
    }

    public void SetProgress(int progress)
    {
        sliderForeground_VisualElement.style.width = Length.Percent(progress);
    }
}
