using System;

public class GameState_EventArgs : EventArgs
{
    public int LevelNumber { get; }

    public GameState_EventArgs(int levelNumber)
    {
        LevelNumber = levelNumber;
    }
}
