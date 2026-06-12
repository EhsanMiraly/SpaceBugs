using UnityEngine;
using UnityEngine.UIElements;

public class ScreenSafeArea
{
    public static void RemoveUnSafeAreaFromGame()
    {
        Rect safeArea = Screen.safeArea;
        float xMin = safeArea.xMin / (float)Screen.width;
        float yMin = safeArea.yMin / (float)Screen.height;
        Camera.main.rect = new Rect(xMin, yMin, (1f - (2f * xMin)), (1f - (2f * yMin)));
    }

    public static void RemoveUnSafeAreaFromUI(VisualElement root_VisualElement)
    {
        VisualElement parent_VisualElement = root_VisualElement.Q<VisualElement>("Parent_VisualElement");

        Rect safeArea = Screen.safeArea;

        parent_VisualElement.style.paddingLeft = safeArea.xMin;
        parent_VisualElement.style.paddingRight = safeArea.xMin;
        parent_VisualElement.style.paddingTop = safeArea.yMin;
        parent_VisualElement.style.paddingBottom = safeArea.yMin;
    }
}
