using UnityEngine;

public class PlayerData
{
    public static bool IsPlaying { get; set; } = false;
    public static bool IsPaused { get; set; } = false;
    public static int CurrentLevelNumber { get; set; } = 0;
    public static string CurrentLevelID { get; set; }


    public static int MaxHealth => 10;
    public static int CurrentHealth { get; set; } = MaxHealth;

    public static int Score { get; set; } = 0;

    public static int MaxBullets => 3;
    public static int CurrentBullets { get; set; } = MaxBullets;





    public static int MoveSpeed => 10;

    public static string CurrentRotateDirection { get; set; } = Up;
    public static string Left => "Left";
    public static string Up => "UP";
    public static string Right => "Right";
}
