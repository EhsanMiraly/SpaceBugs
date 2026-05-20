using System;
using UnityEngine;
using UnityEngine.UIElements;

public class Screen_UI : MonoBehaviour
{
    UIDocument uIDocument;
    VisualElement root;

    VisualElement playerHealthBackground_VisualElement;
    VisualElement playerHealthForeground_VisualElement;

    Label score_Label;
    Label bullets_Label;

    private int movingState;


    public void Initialize()
    {
        ConnectUI();
        ConnectToEvents();
        InitialPlayerHealthUI();
    }

    private void ConnectUI()
    {
        uIDocument = GetComponent<UIDocument>();
        root = uIDocument.rootVisualElement;

        playerHealthBackground_VisualElement = root.Q<VisualElement>("PlayerHealthBackground_VisualElement");
        playerHealthForeground_VisualElement = root.Q<VisualElement>("PlayerHealthForeground_VisualElement");
        score_Label = root.Q<Label>("Score_Label");
        bullets_Label = root.Q<Label>("Bullets_Label");

        score_Label.text = "Score: " + PlayerData.Score;
        bullets_Label.text = "Bullets: " + PlayerData.CurrentBullets;
    }

    private void ConnectToEvents()
    {
        UI_Input_EventManager.OnMove_Event += OnMoveState;
        UI_Input_EventManager.OnFire_Event += OnUpdateBulletsInUIMinus;

        EnemyEventManager.OnEnemyDied_Event += OnUpdateScoreInUI;
        EnemyEventManager.OnEnemyPassedLine_Event += OnUpdatePlayerHealthInUI;

        BulletEventManager.OnBulletDestroyed_Event += OnUpdateBulletsInUIPlus;

        GameState_EventManager.OnLoseLevel_Event += OnLoseGame;//Delete?-----------------------------
    }

    public void OnLoseGame(object o, GameState_EventArgs e)//Delete---------------------
    {
        WinLoseWindow_UI winLoseWindow_UI = new WinLoseWindow_UI(new GameObject());
        winLoseWindow_UI.SetLose();
        //Stop Game Time Scale


    }

    public void InitialPlayerHealthUI()
    {
        int x = (Screen.width / 100) * 20;
        int y = (Screen.height / 100) * 5;

        playerHealthBackground_VisualElement.style.width = x;
        playerHealthBackground_VisualElement.style.height = y;

        playerHealthForeground_VisualElement.style.width = Length.Percent(100);
        playerHealthForeground_VisualElement.style.height = Length.Percent(100);
    }

    public void OnUpdatePlayerHealthInUI(object sender, EnemyData_EventArgs e)
    {
        float x = (100 * PlayerData.CurrentHealth) / PlayerData.MaxHealth;

        playerHealthForeground_VisualElement.style.width = Length.Percent(x);

        //////Here?
    }


    public void OnUpdateScoreInUI(object sender, EnemyData_EventArgs e)
    {
        PlayerData.Score += e.EnemyData.MaxHealth;
        score_Label.text = "Score: " + PlayerData.Score;
    }

    public void OnUpdateBulletsInUIMinus(object sender, PlayerFireInput_EventArgs e)
    {
        PlayerData.CurrentBullets--;
        bullets_Label.text = "Bullets: " + PlayerData.CurrentBullets;

        if (PlayerData.CurrentBullets <= 0)
        {
            UI_Input_EventManager.InvokeOnCanFire(this, new PlayerFireInput_EventArgs(false));
        }
    }
    public void OnUpdateBulletsInUIPlus(object sender, Bullet_EventArgs e)
    {
        PlayerData.CurrentBullets++;
        bullets_Label.text = "Bullets: " + PlayerData.CurrentBullets;

        if (PlayerData.CurrentBullets > 0 && movingState == 0)
        {
            UI_Input_EventManager.InvokeOnCanFire(this, new PlayerFireInput_EventArgs(true));
        }
    }



    public void OnMoveState(object o, PlayerMoveInput_EventArgs e)
    {
        movingState = e.MoveDirection;
    }

}
