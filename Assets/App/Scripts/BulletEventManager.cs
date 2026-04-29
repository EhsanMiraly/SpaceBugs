using UnityEngine;
using UnityEngine.Events;


[System.Serializable]
public class Int_UnityEvent : UnityEvent<int> { }

public class BulletEventManager : MonoBehaviour
{
    [HideInInspector] public UnityEvent OnBulletShot;
    [HideInInspector] public UnityEvent OnBulletDestroyed;
}
