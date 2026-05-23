using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData_00", menuName = "ScriptableObjects/LevelData")]
public class LevelData_SO : ScriptableObject
{
    [Range(1, 1000)]
    [SerializeField] private int scoreNeeded;
    public int ScoreNeeded => scoreNeeded;

    [Range(1, 10)]
    [SerializeField] private int enemyGenerationRate;
    public int EnemyGenerationRate => enemyGenerationRate;

    [SerializeField] private List<EnemyData_SO> enemies;
    public List<EnemyData_SO> Enemies => enemies;


}
