using UnityEngine;

public class LevelManager : MonoBehaviour
{
    PlayerHealthScoreBullets_UI screen_UI;

    private int movingState;

    public void Initialize()
    {
        screen_UI = FindAnyObjectByType<PlayerHealthScoreBullets_UI>().GetComponent<PlayerHealthScoreBullets_UI>();

        PlayerInputUI_EventManager.OnFire_Event += OnUpdateBulletsMinus;
        BulletEventManager.OnBulletDestroyed_Event += OnUpdateBulletsPlus;
        PlayerInputUI_EventManager.OnMove_Event += OnMoveState;

        EnemyEventManager.OnEnemyDied_Event += OnUpdateScore;
        EnemyEventManager.OnEnemyPassedLine_Event += OnUpdateHealth;

        screen_UI.UpdateHealthInUI();
        screen_UI.UpdateScoreInUI();
        screen_UI.UpdateBulletsInUI();
    }

    private void OnDisable()
    {
        PlayerInputUI_EventManager.OnFire_Event -= OnUpdateBulletsMinus;
        BulletEventManager.OnBulletDestroyed_Event -= OnUpdateBulletsPlus;
        PlayerInputUI_EventManager.OnMove_Event -= OnMoveState;

        EnemyEventManager.OnEnemyDied_Event -= OnUpdateScore;
        EnemyEventManager.OnEnemyPassedLine_Event -= OnUpdateHealth;
    }


    #region Bullets

    public void OnUpdateBulletsMinus(object sender, PlayerFireInput_EventArgs e)
    {
        PlayerData.CurrentBullets--;
        if (PlayerData.CurrentBullets <= 0)
        {
            PlayerInputUI_EventManager.InvokeOnCanFire(this, new PlayerFireInput_EventArgs(false));
        }

        screen_UI.UpdateBulletsInUI();
    }

    public void OnUpdateBulletsPlus(object sender, Bullet_EventArgs e)
    {
        PlayerData.CurrentBullets++;

        if (PlayerData.CurrentBullets > 0 && movingState == 0)
        {
            PlayerInputUI_EventManager.InvokeOnCanFire(this, new PlayerFireInput_EventArgs(true));
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
