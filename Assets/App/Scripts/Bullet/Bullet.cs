using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour, IObjectInPool
{
    #region Sound effects
    AudioSource audioSource;
    AudioClip bulletHitWall_AudioClip;
    //AudioClip noBullet_AudioClip;
    #endregion


    private Rigidbody2D rb2D;
    private float speed = 10f;
    public bool IsEnable { get; set; }


    void OnEnable()
    {
        audioSource = GetComponent<AudioSource>();
        bulletHitWall_AudioClip = Resources.Load<AudioClip>("Sounds/SoundEffects/BulletHitWall");

        rb2D = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (IsEnable)
        {
            rb2D.linearVelocity = rb2D.linearVelocity.normalized * speed;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "EndOfLine")
        {
            StopMoving();
            return;
        }

        PlayBulletHitWall();

        //Rotate Object
        Vector2 v = rb2D.linearVelocity;
        if (v.sqrMagnitude < 0.0001f)
            return;
        float angle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }


    public void StartMoving(Vector2 direction)
    {
        gameObject.SetActive(true);
        rb2D.linearVelocity = direction.normalized * speed;
        IsEnable = true;
    }

    public void StopMoving()
    {
        BulletEventManager.InvokeOnBulletDestroyed(this.gameObject, false);
        IsEnable = false;
        rb2D.linearVelocity = Vector2.zero;
        gameObject.SetActive(false);
    }



    #region Sound effects

    private void PlayBulletHitWall()
    {
        audioSource.volume = SettingsData.soundEffectsVolume;
        audioSource.clip = bulletHitWall_AudioClip;

        if (SettingsData.isSoundEffectsOn)
        {
            audioSource.Play();
        }
    }

    #endregion

}
