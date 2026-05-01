using UnityEngine;
using UnityEngine.UIElements;

public class Screen_UI : MonoBehaviour
{
    int score = 0;
    int bullets = 3;

    UIDocument uIDocument;
    VisualElement root;

    Label score_Label;
    Label bullets_Label;

    private void Awake()
    {
        EnemyEventManager.OnEnemyDied_Event += OnUpdateScoreInUI;
        BulletEventManager.OnBulletShot_Event += OnUpdateBulletsInUIMinus;
        BulletEventManager.OnBulletDestroyed_Event += OnUpdateBulletsInUIPlus;



        uIDocument = GetComponent<UIDocument>();
        root = uIDocument.rootVisualElement;

        score_Label = root.Q<Label>("Score_Label");
        bullets_Label = root.Q<Label>("Bullets_Label");

        score_Label.text = "Score: " + score;
        bullets_Label.text = "Bullets: " + bullets;
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

}
