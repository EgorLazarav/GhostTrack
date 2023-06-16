using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AnimatedUI : MonoBehaviour
{
    [SerializeField] private bool _isPlayingOnAwake = true;
    [SerializeField] private float _animationSpeed = 1;

    private Coroutine _animationCoroutine;

    public float AnimationSpeed => _animationSpeed;

    protected virtual void OnEnable()
    {
        if (_isPlayingOnAwake)
            StartAnimation();
    }

    protected virtual void OnDisable()
    {
        StopAnimation();
    }

    public virtual void StartAnimation()
    {
        _animationCoroutine = StartCoroutine(Animating());
    }

    public virtual void StopAnimation()
    {
        if (_animationCoroutine != null)
            StopCoroutine(_animationCoroutine);
    }

    protected abstract IEnumerator Animating();
}
