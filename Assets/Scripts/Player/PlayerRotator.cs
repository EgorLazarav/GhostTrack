using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerRotator : MonoBehaviour
{
    private Rigidbody2D _rigidbody;

    public void Init()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        var lookDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        _rigidbody.rotation = lookAngle;
    }
}
