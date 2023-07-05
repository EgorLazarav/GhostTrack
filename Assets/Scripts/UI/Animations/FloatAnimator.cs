using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FloatAnimator : AnimatedUI
{
    [SerializeField] private float _floatingDelta = 1;

    private float _baseRotationZ;

    private void Awake()
    {
        _baseRotationZ = transform.eulerAngles.z;
    }

    protected override IEnumerator Animating()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        transform.eulerAngles = new Vector3(0, 0, _baseRotationZ);

        while (true)
        {
            transform.DORotateZ(_baseRotationZ - _floatingDelta, 1 / AnimationSpeed);
            yield return new WaitForSecondsRealtime(1 / AnimationSpeed);

            transform.DORotateZ(_baseRotationZ + _floatingDelta, 1 / AnimationSpeed);
            yield return new WaitForSecondsRealtime(1 / AnimationSpeed);
        }
    }
}