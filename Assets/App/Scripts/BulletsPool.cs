using System.Collections.Generic;
using UnityEngine;

public class BulletsPool : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;

    private List<GameObject> bulletsPool;
    private int maxBulletsInPool = 3;
    private int currentBulletsInPool = 0;

    private void Awake()
    {
        bulletsPool = new List<GameObject>();
    }

    public bool CanGetBullet()
    {
        for (int i = 0; i < bulletsPool.Count; i++)
        {
            if (!bulletsPool[i].GetComponent<Bullet>().IsEnable)
            {
                return true;
            }
        }

        if (currentBulletsInPool < maxBulletsInPool)
        {
            return true;
        }

        return false;
    }

    public GameObject GetBullet()
    {
        for (int i = 0; i < bulletsPool.Count; i++)
        {
            if (!bulletsPool[i].GetComponent<Bullet>().IsEnable)
            {
                return bulletsPool[i];
            }
        }
        return AddBulletToPool();
    }

    private GameObject AddBulletToPool()
    {
        GameObject bullet = Instantiate(bulletPrefab);
        bulletsPool.Add(bullet);
        currentBulletsInPool++;
        return bullet;
    }

}
