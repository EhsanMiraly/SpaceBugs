using System;

public class PlayerMoveInput_EventArgs : EventArgs
{
    public int MoveDirection { get; }

    public PlayerMoveInput_EventArgs(int moveDirection)
    {
        MoveDirection = moveDirection;
    }
}
