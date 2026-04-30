using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/EnemyData")]
public class EnemyData_SO : ScriptableObject
{
    [Range(1, 100f)]
    [SerializeField] private int respawnPossibility;
    public int RespawnPossibility => respawnPossibility;

    [Range(1, 10)]
    [SerializeField] private int maxInPool;
    public int MaxInPool => maxInPool;


    [SerializeField] private Color color;
    public Color Color => color;

    [Range(0.1f, 1f)]
    [SerializeField] private float size;
    public float Size => size;

    [Range(1, 10)]
    [SerializeField] private int maxHealth;
    public int MaxHealth => maxHealth;
    public int CurrentHealth { get; set; }

    [Range(0.1f, 5f)]
    [SerializeField] private float moveSpeed;
    public float MoveSpeed => moveSpeed;

    [Range(0.1f, 10f)]
    [SerializeField] private float timeBetweenChangingDirection;
    public float TimeBetweenChangingDirection => timeBetweenChangingDirection;

    [Header("Ray")]
    [Range(0.01f, 1f)]
    [SerializeField] private float rayDistance;
    public float RayDistance => rayDistance;

    [Range(0.01f, 1f)]
    [SerializeField] private float rayDistanceFromOrigin;
    public float RayDistanceFromOrigin => rayDistanceFromOrigin;

    [Range(0.01f, 1f)]
    [SerializeField] private float rayDistanceFromSide;
    public float RayDistanceFromSide => rayDistanceFromSide;
}
