#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;
using UnityEngine.UIElements;

public class MainPage_Controller : MonoBehaviour
{
    Menu_UIConnector menu_UIConnector;

    public void Initialize(Menu_UIConnector menu_UIConnector)
    {
        this.menu_UIConnector = menu_UIConnector;

        menu_UIConnector.resume_Button.RegisterCallback<ClickEvent>(OnResume_ButtonSelected);
        menu_UIConnector.levels_Button.RegisterCallback<ClickEvent>(OnLevels_ButtonSelected);
        menu_UIConnector.settings_Button.RegisterCallback<ClickEvent>(OnSettings_ButtonSelected);
        menu_UIConnector.exit_Button.RegisterCallback<ClickEvent>(OnExit_ButtonSelected);
    }


    private void OnResume_ButtonSelected(ClickEvent clickEvent)
    {
        menu_UIConnector.menu_VisualElement.style.display = DisplayStyle.Flex;
        menu_UIConnector.pageHolder_VisualElement.style.display = DisplayStyle.None;
        GameState_EventManager.InvokeOnResumeLevel(this, new GameState_EventArgs(GameData.CurrentLevelNumber));
    }

    private void OnLevels_ButtonSelected(ClickEvent clickEvent)
    {
        menu_UIConnector.resume_Button.style.display = DisplayStyle.None;
        menu_UIConnector.SwitchPage(menu_UIConnector.levelsPage_VisualElement);
        GameState_EventManager.InvokeOnStopLevel(this, new GameState_EventArgs(0));
    }

    private void OnSettings_ButtonSelected(ClickEvent clickEvent)
    {
        menu_UIConnector.SwitchPage(menu_UIConnector.settingsPage_VisualElement);
    }

    private void OnExit_ButtonSelected(ClickEvent clickEvent)
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
