using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Door : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private Coroutine _coroutine;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(Closing());
    }

    private IEnumerator Closing()
    {
        _rigidbody.angularDrag = 0;
        float speed = 10;

        while (Mathf.Abs(_rigidbody.rotation) > 1)
        {
            print(_rigidbody.rotation);

            if (_rigidbody.rotation > 0)
            {
                _rigidbody.rotation -= Time.deltaTime * speed;
            }
            else
            {
                _rigidbody.rotation += Time.deltaTime * speed;
            }

            yield return null;
        }

        _rigidbody.velocity = Vector2.zero;
        _rigidbody.rotation = 0;
        _rigidbody.angularDrag = 10;

        _coroutine = null;
    }
}
