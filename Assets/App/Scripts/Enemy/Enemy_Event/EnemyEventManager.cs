using System;
using UnityEngine;


public class EnemyEventManager
{
    public static event EventHandler<EnemyData_EventArgs> OnEnemyDied_Event;
    public static void InvokeOnEnemyDied(GameObject sender, EnemyData_SO enemyData)
    {
        EnemyEventManager.OnEnemyDied_Event?.Invoke(sender, new EnemyData_EventArgs(enemyData));
    }
}

