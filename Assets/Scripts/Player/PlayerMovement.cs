using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 5;

    private Rigidbody2D _rigidbody;

    public void Init()
    {
        _rigidbody = GetComponent<Rigidbody2D>();   
    }

    private void FixedUpdate()
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
