using UnityEngine;

public class LevelManager : MonoBehaviour
{
    PlayerCombatInfo_UI screen_UI;

    private int movingState;

    public void Initialize()
    {
        EventsManager.OnFired_Event += OnUpdateBulletsMinus;
        EventsManager.OnBulletDestroyed_Event += OnUpdateBulletsPlus;
        EventsManager.OnMove_Event += OnMoveState;

        EventsManager.OnEnemyDied_Event += OnUpdateScore;
        EventsManager.OnEnemyDied_Event += OnUpdateProgress;
        EventsManager.OnEnemyPassedLine_Event += OnUpdateHealth;

        EventsManager.InvokeOnHealthChanged();
        EventsManager.InvokeOnScoreChanged();
        EventsManager.InvokeOnBulletsChanged();
    }

    private void OnDisable()
    {
        EventsManager.OnFired_Event -= OnUpdateBulletsMinus;
        EventsManager.OnBulletDestroyed_Event -= OnUpdateBulletsPlus;
        EventsManager.OnMove_Event -= OnMoveState;

        EventsManager.OnEnemyDied_Event -= OnUpdateScore;
        EventsManager.OnEnemyDied_Event -= OnUpdateProgress;
        EventsManager.OnEnemyPassedLine_Event -= OnUpdateHealth;
    }


    #region Bullets

    public void OnUpdateBulletsMinus()
    {
        PlayerData.CurrentBullets--;
        if (PlayerData.CurrentBullets <= 0)
        {
            EventsManager.InvokeOnCanFire(this, new PlayerFireInput_EventArgs(false));
        }

        EventsManager.InvokeOnBulletsChanged();
    }

    public void OnUpdateBulletsPlus()
    {
        PlayerData.CurrentBullets++;

        if (PlayerData.CurrentBullets > 0 && movingState == 0)
        {
            EventsManager.InvokeOnCanFire(this, new PlayerFireInput_EventArgs(true));
        }

        EventsManager.InvokeOnBulletsChanged();
    }

    public void OnMoveState(object o, PlayerMoveInput_EventArgs e)
    {
        movingState = e.MoveDirection;
    }

    #endregion


    public void OnUpdateScore(object sender, EnemyData_EventArgs e)
    {
        PlayerData.Score += e.EnemyData.MaxHealth;

        if (PlayerData.Score >= GameData.currentLevelData.ScoreNeeded)
        {//Win
            Time.timeScale = 0f;
            WinLoseWindow_UI winLoseWindow_UI = new WinLoseWindow_UI(new GameObject(), true);
        }

        EventsManager.InvokeOnScoreChanged();
    }

    public void OnUpdateProgress(object sender, EnemyData_EventArgs e)
    {
        GameData.currentLevelProgress = (int)
            (((float)PlayerData.Score / (float)GameData.currentLevelData.ScoreNeeded) * 100);
    }

    public void OnUpdateHealth(object sender, EnemyData_EventArgs e)
    {
        PlayerData.CurrentHealth -= e.EnemyData.CurrentHealth;

        if (PlayerData.CurrentHealth <= 0)
        {//Lose
            Time.timeScale = 0f;
            WinLoseWindow_UI winLoseWindow_UI = new WinLoseWindow_UI(new GameObject(), false);
        }

        EventsManager.InvokeOnHealthChanged();
    }

}
