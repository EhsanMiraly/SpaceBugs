using System;
using UnityEngine;

public class PlayerRotateInput_EventArgs : EventArgs
{
    public string RotateDirection { get; }

    public PlayerRotateInput_EventArgs(string rotateDirection)
    {
        RotateDirection = rotateDirection;
    }
}
