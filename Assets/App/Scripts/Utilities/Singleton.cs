using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance { get; private set; }

    [SerializeField]
    private bool dontDestroyOnLoad = true;

    protected virtual void Awake()
    {
        if (Instance == null)
        {
            Instance = this as T;

            if (dontDestroyOnLoad)
            {
                DontDestroyOnLoad(gameObject);
            }
        }
        else if (Instance != this)
        {
            Debug.LogWarning($"{typeof(T).Name} already exists. Destroying duplicate.");
            Destroy(gameObject);
        }
    }

    protected virtual void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }
}

//How To Use
/*
public class GameManager : Singleton<GameManager>
{
    public int Score;

    protected override void Awake()
    {
        base.Awake();

        // Initialization
    }

    public void AddScore(int amount)
    {
        Score += amount;
    }
}

GameManager.Instance.AddScore(10);

AudioManager.Instance.PlayMusic();
*/