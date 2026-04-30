using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;


public class Player : MonoBehaviour
{

    [SerializeField] GameObject bulletPrefab;
    Pool<Bullet> bulletsPool;

    Vector2 moveDirection;

    [SerializeField] GameObject barrel;
    [SerializeField] GameObject pointOfShoot;


    private void Awake()
    {
        bulletsPool = new Pool<Bullet>(bulletPrefab, 3);
    }

    void Update()
    {
        MoveYellowTank();
    }

    void MoveYellowTank()
    {
        transform.Translate(moveDirection.x * PlayerData.MoveSpeed * Time.deltaTime, 0f, 0f);
        float x = (float)math.clamp(transform.position.x, -11.5, 11.5);
        transform.position = new Vector3(x, transform.position.y, transform.position.z);
    }

    void OnMove(InputValue inputValue)
    {
        moveDirection = inputValue.Get<Vector2>();
    }

    void OnRotate(InputValue inputValue)
    {
        Vector2 rotateDirection = inputValue.Get<Vector2>();

        if (PlayerData.CurrentRotateDirection == PlayerData.Left)
        {
            if (rotateDirection.x == 1)
            {
                barrel.transform.Rotate(0f, 0f, -45f);
                PlayerData.CurrentRotateDirection = PlayerData.Up;
            }
        }
        else if (PlayerData.CurrentRotateDirection == PlayerData.Up)
        {
            if (rotateDirection.x == -1)
            {
                barrel.transform.Rotate(0f, 0f, 45f);
                PlayerData.CurrentRotateDirection = PlayerData.Left;
            }
            else if (rotateDirection.x == 1)
            {
                barrel.transform.Rotate(0f, 0f, -45f);
                PlayerData.CurrentRotateDirection = PlayerData.Right;
            }
        }
        else if (PlayerData.CurrentRotateDirection == PlayerData.Right)
        {
            if (rotateDirection.x == -1)
            {
                barrel.transform.Rotate(0f, 0f, 45f);
                PlayerData.CurrentRotateDirection = PlayerData.Up;
            }
        }
    }

    void OnFire(InputValue inputValue)
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

        float isFiring = inputValue.Get<float>();

        if (isFiring == 1)
        {
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


}
