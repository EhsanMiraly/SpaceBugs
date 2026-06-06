using System;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerInputUI_Controller : MonoBehaviour
{
    UIDocument uIDocument;
    VisualElement root;

    VisualElement moveLeft_TemplateContainer;
    VisualElement moveRight_TemplateContainer;

    VisualElement shoot_TemplateContainer;

    VisualElement turnLeft_TemplateContainer;
    VisualElement turnRight_TemplateContainer;



    public void Initialize()
    {
        ConnectUI();
        RegisterEventsOnUI();
    }

    private void OnDisable()
    {
        EventsManager.OnCanFire_Event -= SetShoot_VisualElement;

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



    public void ConnectUI()
    {
        uIDocument = GetComponent<UIDocument>();
        root = uIDocument.rootVisualElement;

        int buttonSize = Screen.width / 15;

        moveLeft_TemplateContainer = root.Q<VisualElement>("MoveLeft_TemplateContainer");
        moveLeft_TemplateContainer.style.width = buttonSize;
        moveLeft_TemplateContainer.style.height = buttonSize;

        moveRight_TemplateContainer = root.Q<VisualElement>("MoveRight_TemplateContainer");
        moveRight_TemplateContainer.style.width = buttonSize;
        moveRight_TemplateContainer.style.height = buttonSize;

        shoot_TemplateContainer = root.Q<VisualElement>("Shoot_TemplateContainer");
        shoot_TemplateContainer.style.width = buttonSize;
        shoot_TemplateContainer.style.height = buttonSize;

        turnLeft_TemplateContainer = root.Q<VisualElement>("TurnLeft_TemplateContainer");
        turnLeft_TemplateContainer.style.width = buttonSize;
        turnLeft_TemplateContainer.style.height = buttonSize;

        turnRight_TemplateContainer = root.Q<VisualElement>("TurnRight_TemplateContainer");
        turnRight_TemplateContainer.style.width = buttonSize;
        turnRight_TemplateContainer.style.height = buttonSize;
    }



    public void RegisterEventsOnUI()
    {
        EventsManager.OnCanFire_Event += SetShoot_VisualElement;

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

    public void SetShoot_VisualElement(object o, PlayerFireInput_EventArgs e)
    {
        shoot_TemplateContainer.SetEnabled(e.Fire);
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

}
