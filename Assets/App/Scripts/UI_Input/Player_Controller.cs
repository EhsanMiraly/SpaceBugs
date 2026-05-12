using System;
using Unity.Mathematics;
using UnityEditor.Animations;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    Animator animator;

    [SerializeField] GameObject bulletPrefab;
    Pool<Bullet> bulletsPool;

    int moveDirection;

    [SerializeField] GameObject barrel;
    [SerializeField] GameObject pointOfShoot;
    Sprite spriteFire;


    private void Awake()
    {
        //Time.timeScale = 0f;

        animator = GetComponent<Animator>();

        bulletsPool = new Pool<Bullet>(bulletPrefab, PlayerData.MaxBullets);

        UI_Input_EventManager.OnMove_Event += OnMove;
        UI_Input_EventManager.OnRotate_Event += OnRotate;
        UI_Input_EventManager.OnFire_Event += OnFire;

        GameState_EventManager.OnStartLevel_Event +=
            (object g, GameState_EventArgs gameState_EventArgs) =>
            {
                PlayerData.IsPlaying = gameState_EventArgs.IsPlaying;
                PlayerData.IsPaused = gameState_EventArgs.IsPaused;
                PlayerData.CurrentLevelNumber = gameState_EventArgs.LevelNumber;
                PlayerData.CurrentLevelID = gameState_EventArgs.LevelID;
            };

        GameState_EventManager.OnPauseLevel_Event +=
            (object g, GameState_EventArgs gameState_EventArgs) =>
            {
                PlayerData.IsPlaying = gameState_EventArgs.IsPlaying;
                PlayerData.IsPaused = gameState_EventArgs.IsPaused;
                PlayerData.CurrentLevelNumber = gameState_EventArgs.LevelNumber;
                //PlayerData.CurrentLevelID = gameState_EventArgs.LevelID;
            };

        GameState_EventManager.OnResumeLevel_Event +=
            (object g, GameState_EventArgs gameState_EventArgs) =>
            {
                PlayerData.IsPlaying = gameState_EventArgs.IsPlaying;
                PlayerData.IsPaused = gameState_EventArgs.IsPaused;
                PlayerData.CurrentLevelNumber = gameState_EventArgs.LevelNumber;
                //PlayerData.CurrentLevelID = gameState_EventArgs.LevelID;
            };

        GameState_EventManager.OnStopLevel_Event +=
            (object g, GameState_EventArgs gameState_EventArgs) =>
            {
                PlayerData.IsPlaying = gameState_EventArgs.IsPlaying;
                PlayerData.IsPaused = gameState_EventArgs.IsPaused;
                PlayerData.CurrentLevelNumber = gameState_EventArgs.LevelNumber;
                PlayerData.CurrentLevelID = "";
            };



        spriteFire = pointOfShoot.GetComponent<SpriteRenderer>().sprite;
        pointOfShoot.GetComponent<SpriteRenderer>().sprite = null;
    }

    void Update()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        transform.Translate(moveDirection * PlayerData.MoveSpeed * Time.deltaTime, 0f, 0f);
        float x = (float)math.clamp(transform.position.x, -11.5, 11.5);
        transform.position = new Vector3(x, transform.position.y, transform.position.z);
    }

    public void OnMove(object o, PlayerMoveInput_EventArgs e)
    {
        moveDirection = e.MoveDirection;

        if (moveDirection == -1)
        {
            SetAnimation("WalkingLeft");
        }
        else if (moveDirection == 1)
        {
            SetAnimation("WalkingRight");
        }
        else
        {
            if (PlayerData.CurrentRotateDirection == PlayerData.Left)
            {
                SetAnimation("Left");
            }
            else if (PlayerData.CurrentRotateDirection == PlayerData.Up)
            {
                SetAnimation("Up");
            }
            else if (PlayerData.CurrentRotateDirection == PlayerData.Right)
            {
                SetAnimation("Right");
            }
        }

    }

    public void OnRotate(object o, PlayerRotateInput_EventArgs e)
    {
        if (e.RotateDirection == PlayerData.Left)
        {
            if (PlayerData.CurrentRotateDirection == PlayerData.Left)
            {
                return;
            }
            else if (PlayerData.CurrentRotateDirection == PlayerData.Up)
            {
                barrel.transform.Rotate(0f, 0f, 45f);
                PlayerData.CurrentRotateDirection = PlayerData.Left;
                SetAnimation("Left");
            }
            else if (PlayerData.CurrentRotateDirection == PlayerData.Right)
            {
                barrel.transform.Rotate(0f, 0f, 45f);
                PlayerData.CurrentRotateDirection = PlayerData.Up;
                SetAnimation("Up");
            }
        }
        else if (e.RotateDirection == PlayerData.Right)
        {
            if (PlayerData.CurrentRotateDirection == PlayerData.Left)
            {
                barrel.transform.Rotate(0f, 0f, -45f);
                PlayerData.CurrentRotateDirection = PlayerData.Up;
                SetAnimation("Up");
            }
            else if (PlayerData.CurrentRotateDirection == PlayerData.Up)
            {
                barrel.transform.Rotate(0f, 0f, -45f);
                PlayerData.CurrentRotateDirection = PlayerData.Right;
                SetAnimation("Right");
            }
            else if (PlayerData.CurrentRotateDirection == PlayerData.Right)
            {
                return;
            }
        }

    }

    public async void OnFire(object o, EventArgs e)
    {
        GameObject bullet;
        Bullet bulletMovement;

        if (bulletsPool.CanGetGameObject())
        {
            bullet = bulletsPool.GetGameObject();
            bullet.transform.position = pointOfShoot.transform.position;
            bullet.transform.rotation = Quaternion.identity;

            bulletMovement = bullet.GetComponent<Bullet>();
        }
        else
        {
            return;
        }

        Vector2 direction = Vector2.zero;

        if (PlayerData.CurrentRotateDirection == PlayerData.Left)
        {
            direction = new Vector2(-1, 1);
            bullet.transform.Rotate(0, 0, 135);
        }
        else if (PlayerData.CurrentRotateDirection == PlayerData.Up)
        {
            direction = new Vector2(0, 1);
            bullet.transform.Rotate(0, 0, 90);
        }
        else if (PlayerData.CurrentRotateDirection == PlayerData.Right)
        {
            direction = new Vector2(1, 1);
            bullet.transform.Rotate(0, 0, 45);
        }

        bulletMovement.StartMoving(direction);

        pointOfShoot.GetComponent<SpriteRenderer>().sprite = spriteFire;
        await Awaitable.WaitForSecondsAsync(0.1f);
        pointOfShoot.GetComponent<SpriteRenderer>().sprite = null;
    }

    public void SetAnimation(string animation)
    {
        //Deactive All
        animator.SetBool("Up", false);
        animator.SetBool("Right", false);
        animator.SetBool("Left", false);
        animator.SetBool("WalkingLeft", false);
        animator.SetBool("WalkingRight", false);

        //Active This
        animator.SetBool(animation, true);
    }
}
