using UnityEngine;

public class LevelManager : MonoBehaviour
{
    PlayerCombatInfo_UI screen_UI;

    private int movingState;

    public void Initialize()
    {
        screen_UI = FindAnyObjectByType<PlayerCombatInfo_UI>().GetComponent<PlayerCombatInfo_UI>();

        EventsManager.OnFired_Event += OnUpdateBulletsMinus;
        EventsManager.OnBulletDestroyed_Event += OnUpdateBulletsPlus;
        EventsManager.OnMove_Event += OnMoveState;

        EventsManager.OnEnemyDied_Event += OnUpdateScore;
        EventsManager.OnEnemyDied_Event += OnUpdateProgress;
        EventsManager.OnEnemyPassedLine_Event += OnUpdateHealth;

        screen_UI.UpdateHealthInUI();
        screen_UI.UpdateScoreInUI();
        screen_UI.UpdateBulletsInUI();
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

        screen_UI.UpdateBulletsInUI();
    }

    public void OnUpdateBulletsPlus()
    {
        PlayerData.CurrentBullets++;

        if (PlayerData.CurrentBullets > 0 && movingState == 0)
        {
            EventsManager.InvokeOnCanFire(this, new PlayerFireInput_EventArgs(true));
        }

        screen_UI.UpdateBulletsInUI();
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
            WinLoseWindow_UI winLoseWindow_UI = new WinLoseWindow_UI(new GameObject());
            winLoseWindow_UI.SetWin();
        }

        screen_UI.UpdateScoreInUI();
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
            WinLoseWindow_UI winLoseWindow_UI = new WinLoseWindow_UI(new GameObject());
            winLoseWindow_UI.SetLose();
        }

        screen_UI.UpdateHealthInUI();
    }

}
