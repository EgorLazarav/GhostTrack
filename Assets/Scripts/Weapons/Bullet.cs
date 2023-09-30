using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class Bullet : MonoBehaviour
{
    protected Rigidbody2D Rigidbody;
    protected Collider2D Collider;
    protected float DamagePercent;

    private WaitForSeconds _disableDelay;
    private Coroutine _disableCoroutine;

    private const float DisableTime = 0.1f;

    protected virtual void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        Collider = GetComponent<Collider2D>();

        Rigidbody.isKinematic = true;
        Collider.isTrigger = true;

        _disableDelay = new WaitForSeconds(DisableTime);
    }

    private void OnDisable()
    {
        if (_disableCoroutine != null)
            StopCoroutine(_disableCoroutine);
    }

    private void OnBecameInvisible()
    {
        if (gameObject.activeSelf)
            _disableCoroutine = StartCoroutine(DelayedDisabling());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Health health))
            DealDamage(health);
        else
            gameObject.SetActive(false);
    }

    private IEnumerator DelayedDisabling()
    {
        yield return _disableDelay;

        gameObject.SetActive(false);
    }

    protected virtual void DealDamage(Health health)
    {
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
