using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using DG.Tweening;

public class HoverAnimator<T> : AnimatedUI, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private float _hoverScaleSize = 1.1f;
    [SerializeField] private Color _hoverColor = Color.white;

    protected Color BaseColor;
    private Vector3 _baseScale;

    protected override void OnEnable()
    {
        _baseScale = transform.localScale;
        base.OnEnable();
    }

    protected override IEnumerator Animating()
    {
        while (true)
        {
            transform.DOScale(_hoverScaleSize, 1 / AnimationSpeed);
            yield return new WaitForSecondsRealtime(1 / AnimationSpeed);

            transform.DOScale(_baseScale, 1 / AnimationSpeed);
            yield return new WaitForSecondsRealtime(1 / AnimationSpeed);
        }
    }

    public override void StopAnimation()
    {
        base.StopAnimation();
        transform.DOScale(_baseScale, 1 / AnimationSpeed);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        StartAnimation();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StopAnimation();
    }
}
