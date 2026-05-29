using System;

public class EnemyData_EventArgs : EventArgs
{
    public EnemyData_SO EnemyData { get; }

    public EnemyData_EventArgs(EnemyData_SO enemyData)
    {
        EnemyData = enemyData;
    }
}
