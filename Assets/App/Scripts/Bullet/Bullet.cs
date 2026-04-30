using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour, IObjectInPool
{
    private Rigidbody2D rb2D;
    private float speed = 10f;
    public bool IsEnable { get; set; }


    void OnEnable()
    {
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

        //Rotate Object
        Vector2 v = rb2D.linearVelocity;
        if (v.sqrMagnitude < 0.0001f)
            return;
        float angle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }


    public void StartMoving(Vector2 direction)
    {
        BulletEventManager.InvokeOnBulletShot(this.gameObject, true);
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

}
