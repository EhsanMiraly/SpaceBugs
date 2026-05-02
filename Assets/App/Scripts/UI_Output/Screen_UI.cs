using System;
using UnityEngine;
using UnityEngine.UIElements;

public class Screen_UI : MonoBehaviour
{
    int score = 0;
    int bullets = 3;

    UIDocument uIDocument;
    VisualElement root;

    VisualElement playerHealthBackground_VisualElement;
    VisualElement playerHealthForeground_VisualElement;

    Label score_Label;
    Label bullets_Label;

    private void Awake()
    {
        EnemyEventManager.OnEnemyDied_Event += OnUpdateScoreInUI;
        BulletEventManager.OnBulletShot_Event += OnUpdateBulletsInUIMinus;
        BulletEventManager.OnBulletDestroyed_Event += OnUpdateBulletsInUIPlus;



        uIDocument = GetComponent<UIDocument>();
        root = uIDocument.rootVisualElement;

        playerHealthBackground_VisualElement = root.Q<VisualElement>("PlayerHealthBackground_VisualElement");
        playerHealthForeground_VisualElement = root.Q<VisualElement>("PlayerHealthForeground_VisualElement");
        score_Label = root.Q<Label>("Score_Label");
        bullets_Label = root.Q<Label>("Bullets_Label");


        score_Label.text = "Score: " + score;
        bullets_Label.text = "Bullets: " + bullets;
    }

    private void Start()
    {
        Debug.Log(playerHealthBackground_VisualElement.style.width.value.value);
        OnUpdatePlayerHealthInUI(this, new EventArgs());//Delete Later
    }


    public void OnUpdateScoreInUI(object sender, EnemyData_EventArgs e)
    {
        this.score += e.EnemyData.MaxHealth;
        score_Label.text = "Score: " + this.score;
    }

    public void OnUpdateBulletsInUIMinus(object sender, Bullet_EventArgs e)
    {
        bullets--;
        bullets_Label.text = "Bullets: " + this.bullets;
    }
    public void OnUpdateBulletsInUIPlus(object sender, Bullet_EventArgs e)
    {
        bullets++;
        bullets_Label.text = "Bullets: " + this.bullets;
    }

    public void OnUpdatePlayerHealthInUI(object sender, EventArgs e)//PlayerData_EventArgs
    {
        int maxHealth = 10;//Delete Later
        int currentHealth = 8;//Delete Later
        Debug.Log(playerHealthBackground_VisualElement.style.width);
        int x = (int)(playerHealthBackground_VisualElement.style.width.value.value / maxHealth) * currentHealth;
        Debug.Log(x);
        playerHealthForeground_VisualElement.style.width = x;
    }

}
