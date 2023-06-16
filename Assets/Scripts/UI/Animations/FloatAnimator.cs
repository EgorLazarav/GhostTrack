using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FloatAnimator : AnimatedUI
{
    [SerializeField] private float _floatingDelta = 1;

    private Vector3 _startPosition;

    protected override IEnumerator Animating()
    {
        yield return new WaitForSecondsRealtime(0.1f);

        while (true)
        {
            _startPosition = transform.position;
            transform.DOMove(transform.position.RandomTranslateXY(_floatingDelta), 1 / AnimationSpeed);
            transform.DORotate(new Vector3(0, 0, Random.Range(-_floatingDelta, _floatingDelta)), 1 / AnimationSpeed);
            yield return new WaitForSecondsRealtime(1 / AnimationSpeed);

            transform.DOMove(_startPosition, 1 / AnimationSpeed);
            transform.DORotate(new Vector3(0, 0, Random.Range(-_floatingDelta, _floatingDelta)), 1 / AnimationSpeed);
            yield return new WaitForSecondsRealtime(1 / AnimationSpeed);
        }
    }
}