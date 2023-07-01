using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    private float _speed = 5;
    private Rigidbody2D _rigidbody;

    public void Init(float speed)
    {
        _speed = speed;
    }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector2 moveVector = Vector2.zero;

        if (Input.GetKey(KeyCode.W))
        {
            moveVector += Vector2.up;
        }

        if (Input.GetKey(KeyCode.A))
        {
            moveVector += Vector2.left;
        }

        if (Input.GetKey(KeyCode.S))
        {
            moveVector += Vector2.down;
        }

        if (Input.GetKey(KeyCode.D))
        {
            moveVector += Vector2.right;
        }

        _rigidbody.velocity = moveVector * _speed;
    }
}
