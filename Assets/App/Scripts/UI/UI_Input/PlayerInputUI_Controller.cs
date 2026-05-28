using System;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerInputUI_Controller : MonoBehaviour
{
    UIDocument uIDocument;
    VisualElement root;

    VisualElement moveLeft_VisualElement;
    VisualElement moveRight_VisualElement;

    VisualElement turnLeft_VisualElement;
    VisualElement turnRight_VisualElement;

    VisualElement shoot_VisualElement;



    public void Initialize()
    {
        ConnectUI();
        RegisterEventsOnUI();
    }

    private void OnDisable()
    {
        PlayerInputUI_EventManager.OnCanFire_Event -= SetShoot_VisualElement;

        //Move Left
        moveLeft_VisualElement.UnregisterCallback<PointerDownEvent>(MovinigLeft_PointerDown);
        moveLeft_VisualElement.UnregisterCallback<PointerLeaveEvent>(MovinigLeft_PointerLeave);
        moveLeft_VisualElement.UnregisterCallback<PointerUpEvent>(MovinigLeft_PointerUP);

        //Move Right
        moveRight_VisualElement.UnregisterCallback<PointerDownEvent>(MovinigRight_PointerDown);
        moveRight_VisualElement.UnregisterCallback<PointerLeaveEvent>(MovinigRight_PointerLeave);
        moveRight_VisualElement.UnregisterCallback<PointerUpEvent>(MovinigRight_PointerUP);

        //Turn
        turnLeft_VisualElement.UnregisterCallback<ClickEvent>(TurnLeft);
        turnRight_VisualElement.UnregisterCallback<ClickEvent>(TurnRight);

        //Fire
        shoot_VisualElement.UnregisterCallback<ClickEvent>(TryedFire);
    }



    public void ConnectUI()
    {
        uIDocument = GetComponent<UIDocument>();
        root = uIDocument.rootVisualElement;

        moveLeft_VisualElement = root.Q<VisualElement>("MoveLeft_VisualElement");
        moveRight_VisualElement = root.Q<VisualElement>("MoveRight_VisualElement");

        turnLeft_VisualElement = root.Q<VisualElement>("TurnLeft_VisualElement");
        turnRight_VisualElement = root.Q<VisualElement>("TurnRight_VisualElement");

        shoot_VisualElement = root.Q<VisualElement>("Shoot_VisualElement");
    }



    public void RegisterEventsOnUI()
    {
        PlayerInputUI_EventManager.OnCanFire_Event += SetShoot_VisualElement;

        //Move Left
        moveLeft_VisualElement.RegisterCallback<PointerDownEvent>(MovinigLeft_PointerDown);
        moveLeft_VisualElement.RegisterCallback<PointerLeaveEvent>(MovinigLeft_PointerLeave);
        moveLeft_VisualElement.RegisterCallback<PointerUpEvent>(MovinigLeft_PointerUP);

        //Move Right
        moveRight_VisualElement.RegisterCallback<PointerDownEvent>(MovinigRight_PointerDown);
        moveRight_VisualElement.RegisterCallback<PointerLeaveEvent>(MovinigRight_PointerLeave);
        moveRight_VisualElement.RegisterCallback<PointerUpEvent>(MovinigRight_PointerUP);

        //Turn
        turnLeft_VisualElement.RegisterCallback<ClickEvent>(TurnLeft);
        turnRight_VisualElement.RegisterCallback<ClickEvent>(TurnRight);

        //Fire
        shoot_VisualElement.RegisterCallback<ClickEvent>(TryedFire);

    }

    public void SetShoot_VisualElement(object o, PlayerFireInput_EventArgs e)
    {
        shoot_VisualElement.SetEnabled(e.Fire);
    }


    #region Moving Left

    public void MovinigLeft_PointerDown(PointerDownEvent pointerDownEvent)
    {
        PlayerInputUI_EventManager.InvokeOnMove(this, new PlayerMoveInput_EventArgs(-1));
        shoot_VisualElement.SetEnabled(false);
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
        PlayerInputUI_EventManager.InvokeOnMove(this, new PlayerMoveInput_EventArgs(1));
        shoot_VisualElement.SetEnabled(false);
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
        PlayerInputUI_EventManager.InvokeOnRotate(this, new PlayerRotateInput_EventArgs(PlayerData.Left));
    }

    public void TurnRight(ClickEvent clickEvent)
    {
        PlayerInputUI_EventManager.InvokeOnRotate(this, new PlayerRotateInput_EventArgs(PlayerData.Right));
    }

    #endregion

    public void TryedFire(ClickEvent clickEvent)
    {
        PlayerInputUI_EventManager.InvokeOnTryedFire(this, new PlayerFireInput_EventArgs(true));
    }

    private void SetShootStateTrue()
    {
        PlayerInputUI_EventManager.InvokeOnMove(this, new PlayerMoveInput_EventArgs(0));
        if (PlayerData.CurrentBullets > 0)
        {
            shoot_VisualElement.SetEnabled(true);
        }
        SetTurnState(true);
    }

    private void SetTurnState(bool state)
    {
        turnLeft_VisualElement.SetEnabled(state);
        turnRight_VisualElement.SetEnabled(state);
    }

}
