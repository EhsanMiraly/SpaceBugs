using System.Collections.Generic;

public class LevelsData
{
#if UNITY_EDITOR
    public static Level[] Levels = new Level[]
    {
        new Level(true, 0, 3),
        new Level(true, 0, 6),
        new Level(true, 0, 9),
        new Level(true, 0, 12),
        new Level(true, 0, 15),
        new Level(true, 0, 18),
        new Level(true, 0, 21),
        new Level(true, 0, 24),
        new Level(true, 0, 27),
        new Level(true, 0, 30)
    };
#else
    public static Level[] Levels = new Level[]
    {
        new Level(true, 0, 3),
        new Level(false, 0, 6),
        new Level(false, 0, 9),
        new Level(false, 0, 12),
        new Level(false, 0, 15),
        new Level(false, 0, 18),
        new Level(false, 0, 21),
        new Level(false, 0, 24),
        new Level(false, 0, 27),
        new Level(false, 0, 30)
    };
#endif



    public static void FillLevelsData(Level[] levels)
    {
        if (Levels.Length <= levels.Length)
        {
            Levels = new Level[levels.Length];
        }

        for (int i = 0; i < levels.Length; i++)
        {
            Levels[i].IsOpen = levels[i].IsOpen;
            Levels[i].Progress = levels[i].Progress;
            Levels[i].Coins = levels[i].Coins;
        }
    }

}
