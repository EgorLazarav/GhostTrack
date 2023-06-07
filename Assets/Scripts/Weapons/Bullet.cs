using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;

    private Coroutine _coroutine;
    private int _damageReduceCoeff;

    private const float BaseDamage = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Health health))
            health.ApplyDamage(BaseDamage / _damageReduceCoeff);

        gameObject.SetActive(false);
    }

    public void Init(Vector2 direction, Vector3 position, float rotationZ, float fireRange, float shotPower, int damageReduceCoeff)
    {
        _damageReduceCoeff = damageReduceCoeff;

        transform.position = position;
        transform.rotation = Quaternion.Euler(0, 0, rotationZ);

        _rigidbody.velocity = Vector2.zero;
        _rigidbody.velocity = direction.normalized * shotPower;
        
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(Flying(position, fireRange));
    }

    private IEnumerator Flying(Vector2 startPosition, float maxFlyDistance)
    {
        while (Vector2.Distance(startPosition, transform.position) < maxFlyDistance)
            yield return null;

        _coroutine = null;
        gameObject.SetActive(false);
    }
}
