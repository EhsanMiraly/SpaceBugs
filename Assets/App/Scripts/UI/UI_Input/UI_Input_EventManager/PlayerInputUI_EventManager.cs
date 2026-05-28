using System;
using UnityEngine;

public class PlayerInputUI_EventManager
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

    public static event EventHandler<PlayerFireInput_EventArgs> OnTryedFire_Event;
    public static void InvokeOnTryedFire(object o, PlayerFireInput_EventArgs e)
    {
        OnTryedFire_Event?.Invoke(o, e);
    }


    public static event EventHandler<PlayerFireInput_EventArgs> OnFired_Event;
    public static void InvokeOnFired(object o, PlayerFireInput_EventArgs e)
    {
        OnFired_Event?.Invoke(o, e);
    }


    public static event EventHandler<PlayerFireInput_EventArgs> OnCanFire_Event;
    public static void InvokeOnCanFire(object o, PlayerFireInput_EventArgs e)
    {
        OnCanFire_Event?.Invoke(o, e);
    }

}
