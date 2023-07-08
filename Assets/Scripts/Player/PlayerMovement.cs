using System;
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
        if (!PlayerInput.Instance.enabled)
            _rigidbody.velocity = Vector2.zero;
    }

    public void Move(Vector2 direction, bool isWeaponEquiped)
    {
        float noWeaponSpeedBonus = 1.5f;

        _rigidbody.velocity = direction * (_speed + (Convert.ToInt32(isWeaponEquiped) * noWeaponSpeedBonus));
    }
}
