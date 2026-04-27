using UnityEngine;
using UnityEngine.UIElements;

public class Screen_UI : MonoBehaviour
{
    int score = 0;
    int bullets = 1;

    UIDocument uIDocument;
    VisualElement root;

    Label score_Label;
    Label bullets_Label;

    private void Awake()
    {
        uIDocument = GetComponent<UIDocument>();
        root = uIDocument.rootVisualElement;

        score_Label = root.Q<Label>("Score_Label");
        bullets_Label = root.Q<Label>("Bullets_Label");

        score_Label.text = "Score: " + score;
        bullets_Label.text = "Bullets: " + 1;
    }

    public void UpdateScoreInUI(int score)
    {
        this.score += score;
        score_Label.text = "Score: " + this.score;
    }

    public void UpdateBulletsInUI(int bullets)
    {
        this.bullets = bullets;
        bullets_Label.text = "Bullets: " + this.bullets;
    }
}
