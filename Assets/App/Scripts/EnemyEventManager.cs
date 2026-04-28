using UnityEngine;
using UnityEngine.Events;


[System.Serializable]
public class EnemyData_SO_UnityEvent : UnityEvent<EnemyData_SO> { }


public class EnemyEventManager : MonoBehaviour
{
    [HideInInspector] public EnemyData_SO_UnityEvent OnEnemyDied;
}
