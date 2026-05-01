using System;
using UnityEngine;

public class UI_Input_EventManager
{
    public static event EventHandler<PlayerMoveInput_EventArgs> OnMove_Event;
    public static void InvokeOnMove(object o, PlayerMoveInput_EventArgs e)
    {
        OnMove_Event?.Invoke(o, e);
    }

    public static event EventHandler<PlayerRotateInput_EventArgs> OnRotate_Event;
    public static void InvokeOnRotate(object o, PlayerRotateInput_EventArgs e)
    {
        OnRotate_Event?.Invoke(o, e);
    }

    public static event EventHandler<PlayerFireInput_EventArgs> OnFire_Event;
    public static void InvokeOnFire(object o, PlayerFireInput_EventArgs e)
    {
        OnFire_Event?.Invoke(o, e);
    }

}
