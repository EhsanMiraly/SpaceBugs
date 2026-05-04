using System;
using UnityEngine;
using UnityEngine.Events;

public class BulletEventManager
{
    public static event EventHandler<Bullet_EventArgs> OnBulletDestroyed_Event;
    public static void InvokeOnBulletDestroyed(GameObject sender, bool bullet)
    {
        OnBulletDestroyed_Event?.Invoke(sender, new Bullet_EventArgs(bullet));
    }
}
