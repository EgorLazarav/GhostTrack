using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Door : MonoBehaviour
{
    [SerializeField] private float _closingSpeed = 20;

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
        yield return new WaitForSeconds(0.2f);

        while (Mathf.Abs(_rigidbody.rotation) > 1)
        {
            print(_rigidbody.rotation);

            if (_rigidbody.rotation > 0)
            {
                _rigidbody.rotation -= Time.deltaTime * _closingSpeed;
            }
            else
            {
                _rigidbody.rotation += Time.deltaTime * _closingSpeed;
            }

            yield return null;
        }

        _rigidbody.velocity = Vector2.zero;
        _rigidbody.rotation = 0;
        _rigidbody.angularDrag = 10;
        _coroutine = null;
    }
}
