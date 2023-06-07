using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FloatAnimator : MonoBehaviour
{
    [SerializeField] private Transform _objectTransform;

    [Header("Settings")]
    [SerializeField] private float _animationSpeed = 1;
    [SerializeField] private float _floatingDelta = 1;

    private Vector3 _startPosition;

    private void Start()
    {
        _startPosition = _objectTransform.position;
    }

    private IEnumerator Floating()
    {
        while (true)
        {
            _objectTransform.DOMove(_objectTransform.position + GetRandomPosition(_floatingDelta), 1 / _animationSpeed);
            yield return new WaitForSeconds(1 / _animationSpeed);

            _objectTransform.DOMove(_startPosition, 1 / _animationSpeed);
            yield return new WaitForSeconds(1 / _animationSpeed);
        }
    }

    private Vector3 GetRandomPosition(float maxDistance)
    {
        float randomX = Random.Range(-maxDistance, maxDistance);
        float randomY = Random.Range(-maxDistance, maxDistance);

        return new Vector3(randomX, randomY);
    }
}