using System;
using NUnit.Framework.Internal;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class UI_Input_Controller : MonoBehaviour
{
    UIDocument uIDocument;
    VisualElement root;

    VisualElement moveLeft_VisualElement;
    VisualElement moveRight_VisualElement;

    VisualElement turnLeft_VisualElement;
    VisualElement turnRight_VisualElement;

    VisualElement shoot_VisualElement;



    private void Awake()
    {
        UI_Input_EventManager.OnCanFire_Event += SetShoot_VisualElement;
        ConnectUI();
        RegisterEventsOnUI();
    }

    public void SetShoot_VisualElement(object o, PlayerFireInput_EventArgs e)
    {
        shoot_VisualElement.SetEnabled(e.Fire);
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
        #region Moving Left

        moveLeft_VisualElement.RegisterCallback<PointerDownEvent>(evt =>
        {
            UI_Input_EventManager.InvokeOnMove(this, new PlayerMoveInput_EventArgs(-1));
            shoot_VisualElement.SetEnabled(false);
            SetTurnState(false);
        });

        moveLeft_VisualElement.RegisterCallback<PointerLeaveEvent>(evt =>
        {
            UI_Input_EventManager.InvokeOnMove(this, new PlayerMoveInput_EventArgs(0));
            if (PlayerData.CurrentBullets > 0)
            {
                shoot_VisualElement.SetEnabled(true);
            }
            SetTurnState(true);
        });

        moveLeft_VisualElement.RegisterCallback<PointerUpEvent>(evt =>
        {
            UI_Input_EventManager.InvokeOnMove(this, new PlayerMoveInput_EventArgs(0));
            if (PlayerData.CurrentBullets > 0)
            {
                shoot_VisualElement.SetEnabled(true);
            }
            SetTurnState(true);
        });

        #endregion

        #region Moving Right

        moveRight_VisualElement.RegisterCallback<PointerDownEvent>(vt =>
        {
            UI_Input_EventManager.InvokeOnMove(this, new PlayerMoveInput_EventArgs(1));
            shoot_VisualElement.SetEnabled(false);
            SetTurnState(false);
        });

        moveRight_VisualElement.RegisterCallback<PointerLeaveEvent>(vt =>
        {
            UI_Input_EventManager.InvokeOnMove(this, new PlayerMoveInput_EventArgs(0));
            if (PlayerData.CurrentBullets > 0)
            {
                shoot_VisualElement.SetEnabled(true);
            }
            SetTurnState(true);
        });

        moveRight_VisualElement.RegisterCallback<PointerUpEvent>(vt =>
        {
            UI_Input_EventManager.InvokeOnMove(this, new PlayerMoveInput_EventArgs(0));
            if (PlayerData.CurrentBullets > 0)
            {
                shoot_VisualElement.SetEnabled(true);
            }
            SetTurnState(true);
        });

        #endregion

        //Turn
        turnLeft_VisualElement.RegisterCallback<ClickEvent>(evt =>
        {
            UI_Input_EventManager.InvokeOnRotate(this, new PlayerRotateInput_EventArgs(PlayerData.Left));
        });

        turnRight_VisualElement.RegisterCallback<ClickEvent>(evt =>
        {
            UI_Input_EventManager.InvokeOnRotate(this, new PlayerRotateInput_EventArgs(PlayerData.Right));
        });

        //Fire
        shoot_VisualElement.RegisterCallback<ClickEvent>(evt =>
        {
            UI_Input_EventManager.InvokeOnFire(this, new PlayerFireInput_EventArgs(true));
        });

    }


    private void SetTurnState(bool state)
    {
        turnLeft_VisualElement.SetEnabled(state);
        turnRight_VisualElement.SetEnabled(state);
    }

}
