using System;

public class GameState_EventArgs : EventArgs
{
    public bool IsPlaying { get; }
    public bool IsPaused { get; }
    public int LevelNumber { get; }
    public string LevelID { get; }

    public GameState_EventArgs(bool isPlaying, bool isPaused, int levelNumber)
    {
        IsPlaying = isPlaying;
        IsPaused = isPaused;
        LevelNumber = levelNumber;
        LevelID = Guid.NewGuid().ToString("N");
    }
}
