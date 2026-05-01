using System;

public class PlayerFireInput_EventArgs : EventArgs
{
    public bool Fire { get; }

    public PlayerFireInput_EventArgs(bool fire)
    {
        Fire = fire;
    }
}
