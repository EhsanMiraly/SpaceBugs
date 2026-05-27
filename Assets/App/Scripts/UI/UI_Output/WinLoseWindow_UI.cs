using System;
using UnityEngine;
using UnityEngine.UIElements;

public class WinLoseWindow_UI
{
    GameObject parent;

    VisualTreeAsset winLoseWindow_UI_Template;

    UIDocument uIDocument;
    VisualElement root;

    Label winLose_Label;
    Button oK_Button;

    Menu_UIConnector menu_UIConnector;


    public WinLoseWindow_UI(GameObject parent)
    {
        this.parent = parent;
        parent.name = "WinLoseWindow_UI";
        parent.layer = LayerMask.NameToLayer("UI");

        winLoseWindow_UI_Template = Resources.Load<VisualTreeAsset>("UI/WinLoseWindow_UI_Template");

        uIDocument = parent.AddComponent<UIDocument>();
        uIDocument.panelSettings = Resources.Load<PanelSettings>("UI/PopUpWindows_UI_PanelSettings");
        uIDocument.visualTreeAsset = winLoseWindow_UI_Template;
        uIDocument.sortingOrder = 100;

        root = uIDocument.rootVisualElement;

        winLose_Label = root.Q<Label>("WinLose_Label");
        oK_Button = root.Q<Button>("OK_Button");

        menu_UIConnector = UnityEngine.Object.FindAnyObjectByType<Menu_UIConnector>().GetComponent<Menu_UIConnector>();

        oK_Button.RegisterCallback<ClickEvent>(evt =>
        {
            //Add points
            //Open Level 2 or next Level
            GameState_EventManager.InvokeOnStopLevel(this, new GameState_EventArgs(GameData.CurrentLevelNumber));
            //Menu Button Selected - Show Menu
            menu_UIConnector.InitialPage();
            Dispose();
        });
    }

    public void SetWin()
    {
        winLose_Label.text = "Win.";
    }

    public void SetLose()
    {
        winLose_Label.text = "Lose.";
    }

    private void Dispose()
    {
        if (parent != null)
        {
            UnityEngine.Object.Destroy(parent);
            parent = null;
            winLoseWindow_UI_Template = null;
            uIDocument = null;
            root = null;
            winLose_Label = null;
            oK_Button = null;
            menu_UIConnector = null;
        }
    }
}
