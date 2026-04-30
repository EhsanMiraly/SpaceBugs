using System;
using UnityEngine;
using UnityEngine.Events;

public class BulletEventManager
{
    public static event EventHandler<Bullet_EventArgs> OnBulletShot_Event;
    public static void InvokeOnBulletShot(GameObject sender, bool bullet)
    {
        BulletEventManager.OnBulletShot_Event?.Invoke(sender, new Bullet_EventArgs(bullet));
    }

    public static event EventHandler<Bullet_EventArgs> OnBulletDestroyed_Event;
    public static void InvokeOnBulletDestroyed(GameObject sender, bool bullet)
    {
        BulletEventManager.OnBulletDestroyed_Event?.Invoke(sender, new Bullet_EventArgs(bullet));
    }
}
