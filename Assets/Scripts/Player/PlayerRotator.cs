using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotator : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _sprite;

    public Vector2 LookDirection { get; private set; }

    Vector3 _currentMousePosition;

    private void Start()
    {
        _currentMousePosition = Input.mousePosition;    
    }

    private void Update()
    {
        _currentMousePosition = Input.mousePosition;
        LookDirection = Camera.main.ScreenToWorldPoint(_currentMousePosition) - transform.position;
        float lookAngle = Mathf.Atan2(LookDirection.y, LookDirection.x) * Mathf.Rad2Deg;
        _sprite.transform.rotation = Quaternion.Euler(0, 0, lookAngle);
    }
}
