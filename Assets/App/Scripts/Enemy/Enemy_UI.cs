using UnityEngine;
using UnityEngine.UIElements;

public class Enemy_UI : MonoBehaviour
{
    UIDocument uiDocument;
    VisualElement root;

    VisualElement health_VisualElement;


    public void OnUpdateUI(object sender, EnemyData_EventArgs e)
    {
        ConnectUI();

        float x = (uiDocument.worldSpaceSize.x / e.EnemyData.MaxHealth) * e.EnemyData.CurrentHealth;
        health_VisualElement.style.width = x;
    }

    public void UpdateUI2(int maxHealth, int currentHealth)
    {
        ConnectUI();

        float x = (uiDocument.worldSpaceSize.x / maxHealth) * currentHealth;
        health_VisualElement.style.width = x;
    }

    public void ConnectUI()
    {
        uiDocument = GetComponent<UIDocument>();
        root = uiDocument.rootVisualElement;

        health_VisualElement = root.Q<VisualElement>("Health_VisualElement");
    }
}
