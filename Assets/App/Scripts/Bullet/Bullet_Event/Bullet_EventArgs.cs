using System;

public class Bullet_EventArgs : EventArgs
{
    public bool Exists { get; }

    public Bullet_EventArgs(bool exists)
    {
        Exists = exists;
    }
}
