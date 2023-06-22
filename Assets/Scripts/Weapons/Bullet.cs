using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        print(collision.collider.name);
        gameObject.SetActive(false);
    }

    public void Init(Vector3 position, Quaternion rotation, float shotPower)
    {
        transform.rotation = rotation;
        transform.position = position;

        _rigidbody.velocity = Vector2.zero;
        _rigidbody.velocity = transform.right * shotPower;
    }
}
