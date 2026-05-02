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

    private void Awake()
    {
        EnemyEventManager.OnEnemyDied_Event += OnUpdateScoreInUI;
        EnemyEventManager.OnEnemyPassedLine_Event += OnUpdatePlayerHealthInUI;

        BulletEventManager.OnBulletShot_Event += OnUpdateBulletsInUIMinus;
        BulletEventManager.OnBulletDestroyed_Event += OnUpdateBulletsInUIPlus;


        uIDocument = GetComponent<UIDocument>();
        root = uIDocument.rootVisualElement;

        playerHealthBackground_VisualElement = root.Q<VisualElement>("PlayerHealthBackground_VisualElement");
        playerHealthForeground_VisualElement = root.Q<VisualElement>("PlayerHealthForeground_VisualElement");
        score_Label = root.Q<Label>("Score_Label");
        bullets_Label = root.Q<Label>("Bullets_Label");

        InitialPlayerHealthUI();

        score_Label.text = "Score: " + PlayerData.Score;
        bullets_Label.text = "Bullets: " + PlayerData.CurrentBullets;
    }

    public void InitialPlayerHealthUI()
    {
        int x = (Screen.width / 100) * 20;
        int y = (Screen.height / 100) * 5;

        playerHealthBackground_VisualElement.style.width = x;
        playerHealthBackground_VisualElement.style.height = y;

        playerHealthForeground_VisualElement.style.width = x;
        playerHealthForeground_VisualElement.style.height = y;
    }

    public void OnUpdatePlayerHealthInUI(object sender, EnemyData_EventArgs e)
    {
        int x = (int)((playerHealthBackground_VisualElement.style.width.value.value / PlayerData.MaxHealth)
                    * PlayerData.CurrentHealth);
        playerHealthForeground_VisualElement.style.width = x;
    }


    public void OnUpdateScoreInUI(object sender, EnemyData_EventArgs e)
    {
        PlayerData.Score += e.EnemyData.MaxHealth;
        score_Label.text = "Score: " + PlayerData.Score;
    }

    public void OnUpdateBulletsInUIMinus(object sender, Bullet_EventArgs e)
    {
        PlayerData.CurrentBullets--;
        bullets_Label.text = "Bullets: " + PlayerData.CurrentBullets;
    }
    public void OnUpdateBulletsInUIPlus(object sender, Bullet_EventArgs e)
    {
        PlayerData.CurrentBullets++;
        bullets_Label.text = "Bullets: " + PlayerData.CurrentBullets;
    }

}
