using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    protected Rigidbody2D Rigidbody;
    protected float BaseDamage = 1;

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out Health health))
            health.ApplyDamage(BaseDamage);

        gameObject.SetActive(false);
    }

    public virtual void Init(Vector3 position, Quaternion rotation, float shotPower, float damage = 1)
    {
        transform.rotation = rotation;
        transform.position = position;

        Rigidbody.velocity = Vector2.zero;
        Rigidbody.velocity = transform.right * shotPower;
    }
}
