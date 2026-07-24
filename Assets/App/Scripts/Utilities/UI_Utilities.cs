using UnityEngine;
using UnityEngine.UIElements;

public class UI_Utilities
{
    private static float baseSize =
        (Screen.width * ((Screen.safeArea.xMax - Screen.safeArea.xMin) / Screen.width)) / 100f;


    public static void FixBackButtonSize(VisualElement bakcButton)
    {
        bakcButton.style.width = baseSize * 20;
        bakcButton.style.height = baseSize * 20;
    }

    #region LevelsPage

    public static void FixLevelsHolderSize(VisualElement visualElement)
    {
        visualElement.style.flexGrow = 0;
        visualElement.style.flexShrink = 0;
        visualElement.style.width = Length.Percent(100);
        visualElement.style.height = baseSize * 50;
    }

    public static void FixLevelSize(VisualElement visualElement)
    {
        visualElement.style.flexGrow = 0;
        visualElement.style.flexShrink = 0;
        visualElement.style.width = baseSize * 45;
        visualElement.style.height = baseSize * 45;
    }
    #endregion


    #region 

    public static void FixInventoryShopItemSize(VisualElement visualElement)
    {
        visualElement.style.width = Length.Percent(100);
        visualElement.style.height = baseSize * 20;
    }

    #endregion


    #region Settings

    public static void FixSettingItemSizeOneRow(VisualElement visualElement)
    {
        visualElement.style.width = Length.Percent(100);
        visualElement.style.height = baseSize * 30;

        visualElement.style.marginTop = baseSize;
        visualElement.style.marginBottom = baseSize;
    }

    public static void FixSettingItemTwoRow(VisualElement visualElement)
    {
        visualElement.style.width = Length.Percent(100);
        visualElement.style.height = baseSize * 60;

        visualElement.style.marginTop = baseSize;
        visualElement.style.marginBottom = baseSize;
    }

    public static void FixElementSize(VisualElement visualElement)
    {
        visualElement.style.width = baseSize * 20;
        visualElement.style.height = baseSize * 20;
    }

    #endregion


    #region PlayerCombatInfo

    public static void FixCombatInfoSize(VisualElement health, VisualElement bullets, VisualElement progress)
    {
        health.style.width = Length.Percent(100);
        health.style.height = baseSize * 10;

        bullets.style.width = Length.Percent(100);
        bullets.style.height = baseSize * 10;

        progress.style.width = Length.Percent(100);
        progress.style.height = baseSize * 10;
    }

    public static void FixCombatInfoItemSize(VisualElement item)
    {
        item.style.width = baseSize * 10;
        item.style.height = Length.Percent(100);
    }

    #endregion

    #region PlayerInputUI

    public static void FixPlayerInputUIElementSize(VisualElement visualElement)
    {
        visualElement.style.width = baseSize * 26;
        visualElement.style.height = baseSize * 26;
    }

    #endregion

}
