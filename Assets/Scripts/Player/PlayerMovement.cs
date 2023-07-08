using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private float _speed;
    private float _noWeaponBonusSpeed;

    public void Init(float speed, float noWeaponBonusSpeed)
    {
        _speed = speed;
        _noWeaponBonusSpeed = noWeaponBonusSpeed;   
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
        _rigidbody.velocity = direction * (_speed + (Convert.ToInt32(isWeaponEquiped) * _noWeaponBonusSpeed));
    }
}
