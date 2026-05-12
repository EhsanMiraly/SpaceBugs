using System;
using UnityEngine;

public class GameState_EventManager
{
    public static event EventHandler<GameState_EventArgs> OnStartLevel_Event;
    public static void InvokeOnStartLevel(object o, GameState_EventArgs gameState_EventArgs)
    {
        OnStartLevel_Event?.Invoke(o, gameState_EventArgs);
    }

    public static event EventHandler<GameState_EventArgs> OnPauseLevel_Event;
    public static void InvokeOnPauseLevel(object o, GameState_EventArgs gameState_EventArgs)
    {
        OnPauseLevel_Event?.Invoke(o, gameState_EventArgs);
    }

    public static event EventHandler<GameState_EventArgs> OnResumeLevel_Event;
    public static void InvokeOnResumeLevel(object o, GameState_EventArgs gameState_EventArgs)
    {
        OnResumeLevel_Event?.Invoke(o, gameState_EventArgs);
    }

    public static event EventHandler<GameState_EventArgs> OnStopLevel_Event;
    public static void InvokeOnStopLevel(object o, GameState_EventArgs gameState_EventArgs)
    {
        OnStopLevel_Event?.Invoke(o, gameState_EventArgs);
    }

}
