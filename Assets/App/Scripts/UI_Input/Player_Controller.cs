using System;
using Unity.Mathematics;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    Pool<Bullet> bulletsPool;

    int moveDirection;

    [SerializeField] GameObject barrel;
    [SerializeField] GameObject pointOfShoot;


    private void Awake()
    {
        UI_Input_EventManager.OnMove_Event += OnMove;
        UI_Input_EventManager.OnRotate_Event += OnRotate;
        UI_Input_EventManager.OnFire_Event += OnFire;


        bulletsPool = new Pool<Bullet>(bulletPrefab, 3);
    }

    void Update()
    {
        MoveYellowTank();
    }

    void MoveYellowTank()
    {
        transform.Translate(moveDirection * PlayerData.MoveSpeed * Time.deltaTime, 0f, 0f);
        float x = (float)math.clamp(transform.position.x, -11.5, 11.5);
        transform.position = new Vector3(x, transform.position.y, transform.position.z);
    }

    public void OnMove(object o, PlayerMoveInput_EventArgs e)
    {
        moveDirection = e.MoveDirection;
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
            }
            else if (PlayerData.CurrentRotateDirection == PlayerData.Right)
            {
                barrel.transform.Rotate(0f, 0f, 45f);
                PlayerData.CurrentRotateDirection = PlayerData.Up;
            }
        }
        else if (e.RotateDirection == PlayerData.Right)
        {
            if (PlayerData.CurrentRotateDirection == PlayerData.Left)
            {
                barrel.transform.Rotate(0f, 0f, -45f);
                PlayerData.CurrentRotateDirection = PlayerData.Up;
            }
            else if (PlayerData.CurrentRotateDirection == PlayerData.Up)
            {
                barrel.transform.Rotate(0f, 0f, -45f);
                PlayerData.CurrentRotateDirection = PlayerData.Right;
            }
            else if (PlayerData.CurrentRotateDirection == PlayerData.Right)
            {
                return;
            }
        }

    }

    public void OnFire(object o, EventArgs e)
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
    }
}
