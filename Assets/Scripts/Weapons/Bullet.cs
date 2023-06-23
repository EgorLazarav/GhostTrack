using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    [SerializeField] protected Rigidbody2D Rigidbody;

    protected int DamagePercent;

    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Health health))
            health.ApplyDamage(DamagePercent);

        gameObject.SetActive(false);
    }

    public virtual void Init(Vector3 position, Quaternion rotation, float shotPower, int damagePercent)
    {
        DamagePercent = damagePercent;
        transform.rotation = rotation;
        transform.position = position;

        Rigidbody.velocity = Vector2.zero;
        Rigidbody.velocity = transform.right * shotPower;
    }
}
