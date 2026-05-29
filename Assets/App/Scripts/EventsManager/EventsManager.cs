using System;
using UnityEngine;


public delegate void OnNotify();

public class EventsManager
{
    #region Bullet Events

    public static event OnNotify OnBulletDestroyed_Event;
    public static void InvokeOnBulletDestroyed()
    {
        OnBulletDestroyed_Event?.Invoke();
    }

    #endregion


    #region Enemy Events

    public event EventHandler<EnemyData_EventArgs> OnEnemyGotHit_Event;
    public void InvokeOnEnemyGotHit(GameObject sender, EnemyData_SO enemyData)
    {
        OnEnemyGotHit_Event?.Invoke(sender, new EnemyData_EventArgs(enemyData));
    }

    public static event EventHandler<EnemyData_EventArgs> OnEnemyDied_Event;
    public static void InvokeOnEnemyDied(GameObject sender, EnemyData_SO enemyData)
    {
        OnEnemyDied_Event?.Invoke(sender, new EnemyData_EventArgs(enemyData));
    }

    public static event EventHandler<EnemyData_EventArgs> OnEnemyPassedLine_Event;
    public static void InvokeOnEnemyPassedLine(GameObject sender, EnemyData_SO enemyData)
    {
        OnEnemyPassedLine_Event?.Invoke(sender, new EnemyData_EventArgs(enemyData));
    }

    #endregion


    #region GameState Evensts

    public static event EventHandler<GameState_EventArgs> OnStartLevel_Event;
    public static void InvokeOnStartLevel(object o, GameState_EventArgs gameState_EventArgs)
    {
        OnStartLevel_Event?.Invoke(o, gameState_EventArgs);
    }

    public static event OnNotify OnPauseLevel_Event;
    public static void InvokeOnPauseLevel()
    {
        OnPauseLevel_Event?.Invoke();
    }

    public static event OnNotify OnResumeLevel_Event;
    public static void InvokeOnResumeLevel()
    {
        OnResumeLevel_Event?.Invoke();
    }

    public static event OnNotify OnStopLevel_Event;
    public static void InvokeOnStopLevel()
    {
        OnStopLevel_Event?.Invoke();
    }

    public static event EventHandler<GameState_EventArgs> OnWinLevel_Event;
    public static void InvokeOnWinLevel(object o, GameState_EventArgs gameState_EventArgs)
    {
        OnWinLevel_Event?.Invoke(o, gameState_EventArgs);
    }

    public static event EventHandler<GameState_EventArgs> OnLoseLevel_Event;
    public static void InvokeOnLoseLevel(object o, GameState_EventArgs gameState_EventArgs)
    {
        OnLoseLevel_Event?.Invoke(o, gameState_EventArgs);
    }

    #endregion


    #region Settings Events

    public static event OnNotify OnLanguageChanged_Event;
    public static void InvokeOnLanguageChanged()
    {
        OnLanguageChanged_Event?.Invoke();
    }

    public static event OnNotify OnBackgroundMusicChanged_Event;
    public static void InvokeOnBackgroundMusicChanged()
    {
        OnBackgroundMusicChanged_Event?.Invoke();
    }

    #endregion


    #region PlayerInputUI Events

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

    public static event OnNotify OnTryedFire_Event;
    public static void InvokeOnTryedFire()
    {
        OnTryedFire_Event?.Invoke();
    }

    public static event OnNotify OnFired_Event;
    public static void InvokeOnFired()
    {
        OnFired_Event?.Invoke();
    }

    public static event EventHandler<PlayerFireInput_EventArgs> OnCanFire_Event;
    public static void InvokeOnCanFire(object o, PlayerFireInput_EventArgs e)
    {
        OnCanFire_Event?.Invoke(o, e);
    }

    #endregion




}
