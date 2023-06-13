using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FloatAnimator : AnimatedUI
{
    [SerializeField] private float _floatingDelta = 1;

    private Vector3 _startPosition;

    private Vector3 GetRandomPosition(float maxDistance)
    {
        float randomX = Random.Range(-maxDistance, maxDistance);
        float randomY = Random.Range(-maxDistance, maxDistance);

        return new Vector3(randomX, randomY);
    }

    protected override IEnumerator Animating()
    {
        while (true)
        {
            _startPosition = transform.position;
            transform.DOMove(transform.position + GetRandomPosition(_floatingDelta), 1 / AnimationSpeed);
            transform.DORotate(new Vector3(0, 0, _floatingDelta), 1 / AnimationSpeed);
            yield return new WaitForSecondsRealtime(1 / AnimationSpeed);

            transform.DOMove(_startPosition, 1 / AnimationSpeed);
            transform.DORotate(new Vector3(0, 0, -_floatingDelta), 1 / AnimationSpeed);
            yield return new WaitForSecondsRealtime(1 / AnimationSpeed);
        }
    }
}