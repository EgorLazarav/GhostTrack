using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FloatAnimator : MonoBehaviour, IAnimatedUI
{
    [SerializeField] private float _animationSpeed = 1;
    [SerializeField] private float _floatingDelta = 1;

    private Vector3 _startPosition;
    private Coroutine _floatingCoroutine;



    private IEnumerator Floating()
    {
        while (true)
        {
            _startPosition = transform.position;
            transform.DOMove(transform.position + GetRandomPosition(_floatingDelta), 1 / _animationSpeed);
            transform.DORotate(new Vector3(0, 0, _floatingDelta), 1 / _animationSpeed);
            yield return new WaitForSeconds(1 / _animationSpeed);

            transform.DOMove(_startPosition, 1 / _animationSpeed);
            transform.DORotate(new Vector3(0, 0, -_floatingDelta), 1 / _animationSpeed);
            yield return new WaitForSeconds(1 / _animationSpeed);
        }
    }

    private Vector3 GetRandomPosition(float maxDistance)
    {
        float randomX = Random.Range(-maxDistance, maxDistance);
        float randomY = Random.Range(-maxDistance, maxDistance);

        return new Vector3(randomX, randomY);
    }

    public void StartAnimation()
    {
        _floatingCoroutine = StartCoroutine(Floating());
    }

    public void StopAnimation()
    {
        if (_floatingCoroutine != null)
            StopCoroutine(_floatingCoroutine);
    }
}