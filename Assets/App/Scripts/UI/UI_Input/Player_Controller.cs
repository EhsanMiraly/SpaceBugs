using System;
using Unity.Mathematics;
using UnityEditor.Animations;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Controller : MonoBehaviour
{
    AudioSource audioSource;
    AudioClip shootBullet_AudioClip;


    Animator animator;

    [SerializeField] GameObject bulletPrefab;
    Pool<Bullet> bulletsPool;

    int moveDirection;

    [SerializeField] GameObject barrel;
    [SerializeField] GameObject pointOfShoot;
    Sprite spriteFire;


    public void Initialize()
    {
        audioSource = GetComponent<AudioSource>();
        shootBullet_AudioClip = Resources.Load<AudioClip>("Sounds/SoundEffects/ShootBullet");

        animator = GetComponent<Animator>();

        bulletsPool = new Pool<Bullet>(bulletPrefab, PlayerData.MaxBullets);

        PlayerInputUI_EventManager.OnMove_Event += OnMove;
        PlayerInputUI_EventManager.OnRotate_Event += OnRotate;
        PlayerInputUI_EventManager.OnFire_Event += OnFire;

        spriteFire = pointOfShoot.GetComponent<SpriteRenderer>().sprite;
        pointOfShoot.GetComponent<SpriteRenderer>().sprite = null;
    }

    private void OnDisable()
    {
        PlayerInputUI_EventManager.OnMove_Event -= OnMove;
        PlayerInputUI_EventManager.OnRotate_Event -= OnRotate;
        PlayerInputUI_EventManager.OnFire_Event -= OnFire;
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

        SceneManager.MoveGameObjectToScene(bullet, SceneManager.GetSceneByName(GameData.currentLevelName));

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


        audioSource.clip = shootBullet_AudioClip;
        audioSource.Play();
    }

    public void SetAnimation(string animation)
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
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
