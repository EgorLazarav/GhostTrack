using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private bool _isHead = false;

    private float _current;

    private const float Max = 1;

    public event UnityAction Overed;
    public event UnityAction HeadShoted;

    private void Start()
    {
        _current = Max;
    }

    public void ApplyDamage(float amount)
    {
        if (_isHead)
        {
            _current = 0;
            HeadShoted?.Invoke();
            print("headshot");
        }
        else
        {
            _current = Mathf.Clamp(_current - amount, 0, _current);
        }

        if (_current == 0)
            Overed?.Invoke();
    }
}
