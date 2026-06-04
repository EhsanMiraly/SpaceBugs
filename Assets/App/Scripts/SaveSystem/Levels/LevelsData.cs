using System.Collections.Generic;

public class LevelsData
{
    public static Level[] Levels = new Level[]
    {
        new Level(true, 0, 3),
        new Level(false, 0, 6)
    };


    public static void FillLevelsData(Level[] levels)
    {
        for (int i = 0; i < levels.Length; i++)
        {
            Levels[i].IsOpen = levels[i].IsOpen;
            Levels[i].Progress = levels[i].Progress;
            Levels[i].TotalStars = levels[i].TotalStars;
        }
    }

}
