using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;

    private int _damagePercent;

    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        gameObject.SetActive(false);
    }

    public void Init(Vector3 position, Quaternion rotation, float shotPower, int damagePercent)
    {
        _damagePercent = damagePercent;
        transform.rotation = rotation;
        transform.position = position;

        _rigidbody.velocity = Vector2.zero;
        _rigidbody.velocity = transform.right * shotPower;
    }
}
