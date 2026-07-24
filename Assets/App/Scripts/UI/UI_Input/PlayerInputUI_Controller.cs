using System;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerInputUI_Controller : MonoBehaviour
{
    PanelRenderer panelRenderer;

    VisualElement moveLeft_TemplateContainer;
    VisualElement moveRight_TemplateContainer;

    VisualElement shoot_TemplateContainer;

    VisualElement turnLeft_TemplateContainer;
    VisualElement turnRight_TemplateContainer;



    public void OnEnable()
    {
        panelRenderer = GetComponent<PanelRenderer>();
        panelRenderer.RegisterUIReloadCallback(OnUIReloadCallback);
    }

    private void OnDisable()
    {
        RemoveFunctionality();

        DisconnctEvents();

        panelRenderer.UnregisterUIReloadCallback(OnUIReloadCallback);
    }

    private void OnUIReloadCallback(PanelRenderer panelRenderer, VisualElement root)
    {
        ScreenSafeArea.RemoveUnSafeAreaFromUI(root);

        moveLeft_TemplateContainer = root.Q<VisualElement>("MoveLeft_TemplateContainer");
        UI_Utilities.FixPlayerInputUIElementSize(moveLeft_TemplateContainer);

        moveRight_TemplateContainer = root.Q<VisualElement>("MoveRight_TemplateContainer");
        UI_Utilities.FixPlayerInputUIElementSize(moveRight_TemplateContainer);

        shoot_TemplateContainer = root.Q<VisualElement>("Shoot_TemplateContainer");
        UI_Utilities.FixPlayerInputUIElementSize(shoot_TemplateContainer);

        turnLeft_TemplateContainer = root.Q<VisualElement>("TurnLeft_TemplateContainer");
        UI_Utilities.FixPlayerInputUIElementSize(turnLeft_TemplateContainer);

        turnRight_TemplateContainer = root.Q<VisualElement>("TurnRight_TemplateContainer");
        UI_Utilities.FixPlayerInputUIElementSize(turnRight_TemplateContainer);


        AddFunctionality();
        ConnctEvents();
    }


    #region Functionality

    private void AddFunctionality()
    {
        //Move Left
        moveLeft_TemplateContainer.RegisterCallback<PointerDownEvent>(MovinigLeft_PointerDown);
        moveLeft_TemplateContainer.RegisterCallback<PointerLeaveEvent>(MovinigLeft_PointerLeave);
        moveLeft_TemplateContainer.RegisterCallback<PointerUpEvent>(MovinigLeft_PointerUP);

        //Move Right
        moveRight_TemplateContainer.RegisterCallback<PointerDownEvent>(MovinigRight_PointerDown);
        moveRight_TemplateContainer.RegisterCallback<PointerLeaveEvent>(MovinigRight_PointerLeave);
        moveRight_TemplateContainer.RegisterCallback<PointerUpEvent>(MovinigRight_PointerUP);

        //Turn
        turnLeft_TemplateContainer.RegisterCallback<ClickEvent>(TurnLeft);
        turnRight_TemplateContainer.RegisterCallback<ClickEvent>(TurnRight);

        //Fire
        shoot_TemplateContainer.RegisterCallback<ClickEvent>(TryedFire);
    }

    private void RemoveFunctionality()
    {
        //Move Left
        moveLeft_TemplateContainer.UnregisterCallback<PointerDownEvent>(MovinigLeft_PointerDown);
        moveLeft_TemplateContainer.UnregisterCallback<PointerLeaveEvent>(MovinigLeft_PointerLeave);
        moveLeft_TemplateContainer.UnregisterCallback<PointerUpEvent>(MovinigLeft_PointerUP);

        //Move Right
        moveRight_TemplateContainer.UnregisterCallback<PointerDownEvent>(MovinigRight_PointerDown);
        moveRight_TemplateContainer.UnregisterCallback<PointerLeaveEvent>(MovinigRight_PointerLeave);
        moveRight_TemplateContainer.UnregisterCallback<PointerUpEvent>(MovinigRight_PointerUP);

        //Turn
        turnLeft_TemplateContainer.UnregisterCallback<ClickEvent>(TurnLeft);
        turnRight_TemplateContainer.UnregisterCallback<ClickEvent>(TurnRight);

        //Fire
        shoot_TemplateContainer.UnregisterCallback<ClickEvent>(TryedFire);
    }


    #region Moving Left

    public void MovinigLeft_PointerDown(PointerDownEvent pointerDownEvent)
    {
        EventsManager.InvokeOnMove(this, new PlayerMoveInput_EventArgs(-1));
        shoot_TemplateContainer.SetEnabled(false);
        SetTurnState(false);
    }

    public void MovinigLeft_PointerLeave(PointerLeaveEvent pointerLeaveEvent)
    {
        SetShootStateTrue();
    }

    private void MovinigLeft_PointerUP(PointerUpEvent pointerUpEvent)
    {
        SetShootStateTrue();
    }

    #endregion

    #region Moving Right

    private void MovinigRight_PointerDown(PointerDownEvent pointerDownEvent)
    {
        EventsManager.InvokeOnMove(this, new PlayerMoveInput_EventArgs(1));
        shoot_TemplateContainer.SetEnabled(false);
        SetTurnState(false);
    }

    private void MovinigRight_PointerLeave(PointerLeaveEvent pointerLeaveEvent)
    {
        SetShootStateTrue();
    }

    private void MovinigRight_PointerUP(PointerUpEvent pointerUpEvent)
    {
        SetShootStateTrue();
    }

    #endregion

    #region Turn

    public void TurnLeft(ClickEvent clickEvent)
    {
        EventsManager.InvokeOnRotate(this, new PlayerRotateInput_EventArgs(PlayerData.Left));
    }

    public void TurnRight(ClickEvent clickEvent)
    {
        EventsManager.InvokeOnRotate(this, new PlayerRotateInput_EventArgs(PlayerData.Right));
    }

    #endregion

    #region Fire

    public void TryedFire(ClickEvent clickEvent)
    {
        EventsManager.InvokeOnTryedFire();
    }

    private void SetShootStateTrue()
    {
        EventsManager.InvokeOnMove(this, new PlayerMoveInput_EventArgs(0));
        if (PlayerData.CurrentBullets > 0)
        {
            shoot_TemplateContainer.SetEnabled(true);
        }
        SetTurnState(true);
    }

    private void SetTurnState(bool state)
    {
        turnLeft_TemplateContainer.SetEnabled(state);
        turnRight_TemplateContainer.SetEnabled(state);
    }

    #endregion

    #endregion



    #region Events Manager

    private void ConnctEvents()
    {
        EventsManager.OnCanFire_Event += SetShoot_VisualElement;
    }

    private void DisconnctEvents()
    {
        EventsManager.OnCanFire_Event -= SetShoot_VisualElement;
    }

    public void SetShoot_VisualElement(object o, PlayerFireInput_EventArgs e)
    {
        shoot_TemplateContainer.SetEnabled(e.Fire);
    }

    #endregion

}
