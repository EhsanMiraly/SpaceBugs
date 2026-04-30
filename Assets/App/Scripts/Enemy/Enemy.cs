using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IObjectInPool
{
    [HideInInspector] public EnemyEventManager enemyEventManager;

    public EnemyData_SO EnemyData { get; set; }


    [SerializeField] private LayerMask layerMask;
    private GameObject enemyBody;
    private SpriteRenderer spriteRenderer;

    Animator animator;

    bool[] movableDirections = new bool[3] { true, true, true };//0=Left 1=Down 2=Right
    bool[] activeDirection = new bool[3] { false, true, false };//0=Left 1=Down 2=Right

    //public bool IsEnable { get; private set; } = false; //For Pool
    public bool CanMove { get; private set; } = false; //For Here
    public bool IsEnable { get; set; }

    private float lastTimeCheckedMovableDirections;


    private void Awake()
    {
        enemyEventManager = new EnemyEventManager();

        enemyBody = transform.GetChild(1).gameObject;
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (CanMove)
        {
            Move();
        }
        if (IsTimeToChangeDirection())
        {
            ChooseNextMoveDirection();
            lastTimeCheckedMovableDirections = Time.time;
        }
        if (lastTimeCheckedMovableDirections + EnemyData.TimeBetweenChangingDirection < Time.time)
        {
            FindMovableDirections();
            ChooseNextMoveDirection();
            lastTimeCheckedMovableDirections = Time.time;
        }
    }

    private bool IsTimeToChangeDirection()
    {
        FindMovableDirections();

        if (activeDirection[0])
        {
            if (!movableDirections[0])
            {
                return true;
            }
        }
        else if (activeDirection[1])
        {
            if (!movableDirections[1])
            {
                return true;
            }
        }
        else if (activeDirection[2])
        {
            if (!movableDirections[2])
            {
                return true;
            }
        }

        return false;
    }


    private void FindMovableDirections()
    {
        movableDirections = new bool[3] { true, true, true };

        Vector2 leftOrigin = new Vector2(transform.position.x - EnemyData.RayDistanceFromOrigin, transform.position.y);
        Vector2 downOrigin = new Vector2(transform.position.x, transform.position.y - EnemyData.RayDistanceFromOrigin);
        Vector2 rightOrigin = new Vector2(transform.position.x + EnemyData.RayDistanceFromOrigin, transform.position.y);

        RaycastHit2D leftRayUp = Physics2D.Raycast(leftOrigin + new Vector2(0, EnemyData.RayDistanceFromSide),
                                                    new Vector2(-1, 0), EnemyData.RayDistance, layerMask);
        RaycastHit2D leftRayDown = Physics2D.Raycast(leftOrigin + new Vector2(0, -EnemyData.RayDistanceFromSide),
                                                    new Vector2(-1, 0), EnemyData.RayDistance, layerMask);
        if (leftRayUp.collider != null || leftRayDown.collider != null)
        {
            movableDirections[0] = false;
        }

        RaycastHit2D downRayLeft = Physics2D.Raycast(downOrigin + new Vector2(-EnemyData.RayDistanceFromSide, 0),
                                                    new Vector2(0, -1), EnemyData.RayDistance, layerMask);
        RaycastHit2D downRayRight = Physics2D.Raycast(downOrigin + new Vector2(EnemyData.RayDistanceFromSide, 0),
                                                    new Vector2(0, -1), EnemyData.RayDistance, layerMask);
        if (downRayLeft.collider != null || downRayRight.collider != null)
        {
            movableDirections[1] = false;
        }

        RaycastHit2D rightRayUp = Physics2D.Raycast(rightOrigin + new Vector2(0, EnemyData.RayDistanceFromSide),
                                                    new Vector2(1, 0), EnemyData.RayDistance, layerMask);
        RaycastHit2D rightRayDown = Physics2D.Raycast(rightOrigin + new Vector2(0, -EnemyData.RayDistanceFromSide),
                                                    new Vector2(1, 0), EnemyData.RayDistance, layerMask);
        if (rightRayUp.collider != null || rightRayDown.collider != null)
        {
            movableDirections[2] = false;
        }

#if UNITY_EDITOR
        if (movableDirections[0])
        {
            Debug.DrawRay(leftOrigin + new Vector2(0, EnemyData.RayDistanceFromSide),
                            new Vector2(-1, 0), Color.green, Time.deltaTime);
            Debug.DrawRay(leftOrigin + new Vector2(0, -EnemyData.RayDistanceFromSide),
                            new Vector2(-1, 0), Color.green, Time.deltaTime);
        }
        else
        {
            Debug.DrawRay(leftOrigin + new Vector2(0, EnemyData.RayDistanceFromSide),
                            new Vector2(-1, 0), Color.red, Time.deltaTime);
            Debug.DrawRay(leftOrigin + new Vector2(0, -EnemyData.RayDistanceFromSide),
                            new Vector2(-1, 0), Color.red, Time.deltaTime);
        }

        if (movableDirections[1])
        {
            Debug.DrawRay(downOrigin + new Vector2(-EnemyData.RayDistanceFromSide, 0),
                            new Vector2(0, -1), Color.green, Time.deltaTime);
            Debug.DrawRay(downOrigin + new Vector2(EnemyData.RayDistanceFromSide, 0),
                            new Vector2(0, -1), Color.green, Time.deltaTime);
        }
        else
        {
            Debug.DrawRay(downOrigin + new Vector2(-EnemyData.RayDistanceFromSide, 0),
                            new Vector2(0, -1), Color.red, Time.deltaTime);
            Debug.DrawRay(downOrigin + new Vector2(EnemyData.RayDistanceFromSide, 0),
                            new Vector2(0, -1), Color.red, Time.deltaTime);
        }

        if (movableDirections[2])
        {
            Debug.DrawRay(rightOrigin + new Vector2(0, EnemyData.RayDistanceFromSide),
                            new Vector2(1, 0), Color.green, Time.deltaTime);
            Debug.DrawRay(rightOrigin + new Vector2(0, -EnemyData.RayDistanceFromSide),
                            new Vector2(1, 0), Color.green, Time.deltaTime);
        }
        else
        {
            Debug.DrawRay(rightOrigin + new Vector2(0, EnemyData.RayDistanceFromSide),
                            new Vector2(1, 0), Color.red, Time.deltaTime);
            Debug.DrawRay(rightOrigin + new Vector2(0, -EnemyData.RayDistanceFromSide),
                            new Vector2(1, 0), Color.red, Time.deltaTime);
        }
#endif
    }

    private void ChooseNextMoveDirection()
    {
        activeDirection = new bool[3] { false, false, false };

        List<int> availableIndexes = new List<int>();

        for (int i = 0; i < movableDirections.Length; i++)
        {
            if (movableDirections[i])
            {
                availableIndexes.Add(i);
            }
        }

        if (availableIndexes.Count == 0)
        {
            activeDirection[1] = true;
        }
        else
        {
            int randomIndex = availableIndexes[Random.Range(0, availableIndexes.Count)];
            activeDirection[randomIndex] = true;
        }

    }

    private void Move()
    {
        if (activeDirection[0])
        {
            transform.Translate(-EnemyData.MoveSpeed * Time.deltaTime, 0f, 0f);
        }
        else if (activeDirection[1])
        {
            transform.Translate(0f, -EnemyData.MoveSpeed * Time.deltaTime, 0f);
        }
        else if (activeDirection[2])
        {
            transform.Translate(EnemyData.MoveSpeed * Time.deltaTime, 0f, 0f);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (CanMove && other.gameObject.tag == "Bullet")
        {
            EnemyData.CurrentHealth--;
            enemyEventManager.InvokeOnEnemyGotHit(this.gameObject, EnemyData);
            if (EnemyData.CurrentHealth <= 0)
            {
                StopMoving();
            }
        }
    }

    public void StartMoving()
    {
        transform.localScale = new Vector3(EnemyData.Size, EnemyData.Size, 1);
        SpriteRenderer[] spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer spriteRenderer in spriteRenderers)
        {
            spriteRenderer.color = EnemyData.Color;
        }

        animator.SetBool("IsDead", false);
        spriteRenderer.sprite = null;
        gameObject.SetActive(true);
        enemyBody.SetActive(true);
        EnemyData.CurrentHealth = EnemyData.MaxHealth;
        enemyEventManager.InvokeOnEnemyGotHit(this.gameObject, EnemyData);//ChangeLater
        IsEnable = true;
        CanMove = true;
    }

    public async void StopMoving()
    {
        EnemyEventManager.InvokeOnEnemyDied(this.gameObject, EnemyData);
        CanMove = false;
        enemyBody.SetActive(false);
        animator.SetBool("IsDead", true);
        await Awaitable.WaitForSecondsAsync(0.75f);
        spriteRenderer.sprite = null;
        IsEnable = false;
        gameObject.SetActive(false);
    }


}
