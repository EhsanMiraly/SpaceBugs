using System;
using UnityEngine;


public class EnemyEventManager
{
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

}

