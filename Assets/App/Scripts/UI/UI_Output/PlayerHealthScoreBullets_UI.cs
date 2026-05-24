using System;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerHealthScoreBullets_UI : MonoBehaviour
{
    UIDocument uIDocument;
    VisualElement root;

    VisualElement playerHealthBackground_VisualElement;
    VisualElement playerHealthForeground_VisualElement;

    Label score_Label;
    Label bullets_Label;




    public void Initialize()
    {
        ConnectUI();
        InitializeUI();
    }

    private void ConnectUI()
    {
        uIDocument = GetComponent<UIDocument>();
        root = uIDocument.rootVisualElement;

        playerHealthBackground_VisualElement = root.Q<VisualElement>("PlayerHealthBackground_VisualElement");
        playerHealthForeground_VisualElement = root.Q<VisualElement>("PlayerHealthForeground_VisualElement");
        score_Label = root.Q<Label>("Score_Label");
        bullets_Label = root.Q<Label>("Bullets_Label");
    }

    public void InitializeUI()
    {
        int x = (Screen.width / 100) * 20;
        int y = (Screen.height / 100) * 5;

        playerHealthBackground_VisualElement.style.width = x;
        playerHealthBackground_VisualElement.style.height = y;

        playerHealthForeground_VisualElement.style.width = Length.Percent(100);
        playerHealthForeground_VisualElement.style.height = Length.Percent(100);

        score_Label.text = "Score: " + PlayerData.Score;
        bullets_Label.text = "Bullets: " + PlayerData.CurrentBullets;
    }


    public void UpdateBulletsInUI()
    {
        bullets_Label.text = "Bullets: " + PlayerData.CurrentBullets;
    }

    public void UpdateScoreInUI()
    {
        score_Label.text = "Score: " + PlayerData.Score;
    }

    public void UpdateHealthInUI()
    {
        float x = (100 * PlayerData.CurrentHealth) / PlayerData.MaxHealth;

        playerHealthForeground_VisualElement.style.width = Length.Percent(x);
    }

}
