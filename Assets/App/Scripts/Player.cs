using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;


public class Player : MonoBehaviour
{
    [SerializeField] BulletsPool bulletsPool;

    Vector2 moveDirection;
    int moveSpeed = 10;

    string currentRotateDirection = "UP";
    const string left = "Left";
    const string up = "UP";
    const string right = "Right";

    [SerializeField] GameObject barrel;
    [SerializeField] GameObject pointOfShoot;


    void Update()
    {
        MoveYellowTank();
    }

    void MoveYellowTank()
    {
        transform.Translate(moveDirection.x * moveSpeed * Time.deltaTime, 0f, 0f);
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

        if (currentRotateDirection == left)
        {
            if (rotateDirection.x == 1)
            {
                barrel.transform.Rotate(0f, 0f, -45f);
                currentRotateDirection = up;
            }
        }
        else if (currentRotateDirection == up)
        {
            if (rotateDirection.x == -1)
            {
                barrel.transform.Rotate(0f, 0f, 45f);
                currentRotateDirection = left;
            }
            else if (rotateDirection.x == 1)
            {
                barrel.transform.Rotate(0f, 0f, -45f);
                currentRotateDirection = right;
            }
        }
        else if (currentRotateDirection == right)
        {
            if (rotateDirection.x == -1)
            {
                barrel.transform.Rotate(0f, 0f, 45f);
                currentRotateDirection = up;
            }
        }
    }

    void OnFire(InputValue inputValue)
    {
        GameObject bullet;
        Bullet bulletMovement;

        if (bulletsPool.CanGetBullet())
        {
            bullet = bulletsPool.GetBullet();
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

            if (currentRotateDirection == left)
            {
                direction = new Vector2(-1, 1);
                bullet.transform.Rotate(0, 0, 135);
            }
            else if (currentRotateDirection == up)
            {
                direction = new Vector2(0, 1);
                bullet.transform.Rotate(0, 0, 90);
            }
            else if (currentRotateDirection == right)
            {
                direction = new Vector2(1, 1);
                bullet.transform.Rotate(0, 0, 45);
            }

            bulletMovement.StartMoving(direction);

            FindAnyObjectByType<Screen_UI>().UpdateBulletsInUI(0);//Edit Later 
        }

    }


}
