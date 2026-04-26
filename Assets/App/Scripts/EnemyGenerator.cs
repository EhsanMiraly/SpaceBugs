using System.Threading.Tasks;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField] bool isPlaying = true;

    EnemiesPool enemiesPool;

    private int enemyGenerationRate = 5;


    private void Awake()
    {
        enemiesPool = GetComponent<EnemiesPool>();
    }

    async void Start()
    {
        while (isPlaying)
        {
            await Awaitable.WaitForSecondsAsync(enemyGenerationRate);
            if (enemiesPool.CanGetEnemy(0))
            {
                float x = Random.Range(-11f, 11f);
                GameObject enemy = enemiesPool.GetEnemy(0);
                enemy.transform.position = transform.position + new Vector3(x, 0f, 0f);
                //enemy.transform.position += new Vector3(x, 0f, 0f);
                enemy.transform.rotation = Quaternion.identity;
                enemy.transform.parent = this.transform;
                enemy.GetComponent<Enemy>().StartMoving();
            }
        }
    }
}
