using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AnimatedUI : MonoBehaviour
{
    [SerializeField] private bool _isPlayingOnAwake;
    [SerializeField] private float _animationSpeed;

    private Coroutine _animationCoroutine;

    public float AnimationSpeed => _animationSpeed;
    public bool IsPlayingOnAwake => _isPlayingOnAwake;

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
