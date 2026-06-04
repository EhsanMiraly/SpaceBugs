using UnityEngine;

public class PlayerData
{
    public static int CurrentHealth { get; set; } = AchievementsData.health;

    public static int Score { get; set; } = 0;

    public static int CurrentBullets { get; set; } = AchievementsData.bullets;

    public static int MoveSpeed => 10;

    public static string CurrentRotateDirection { get; set; } = Up;
    public static string Left => "Left";
    public static string Up => "UP";
    public static string Right => "Right";

    public static void ResetPlayerData()
    {
        CurrentHealth = AchievementsData.health;
        Score = 0;
        CurrentBullets = AchievementsData.bullets;
        CurrentRotateDirection = Up;
    }
}
