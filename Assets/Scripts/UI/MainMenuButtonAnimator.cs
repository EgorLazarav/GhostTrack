using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(FloatAnimator))]
[RequireComponent(typeof(TextGradientAnimator))]
[RequireComponent(typeof(TextHoverAnimator))]
public class MainMenuButtonAnimator : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private FloatAnimator _floatAnimator;
    private TextGradientAnimator _gradientAnimator;
    private TextHoverAnimator _hoverAnimator;

    private void Start()
    {
        _floatAnimator = GetComponent<FloatAnimator>();
        _gradientAnimator = GetComponent<TextGradientAnimator>();
        _hoverAnimator = GetComponent<TextHoverAnimator>();

        _floatAnimator.StartAnimation();
        _gradientAnimator.StartAnimation();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _floatAnimator.StopAnimation();
        _gradientAnimator.StopAnimation();
        _hoverAnimator.StartAnimation();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _floatAnimator.StartAnimation();
        _gradientAnimator.StartAnimation();
        _hoverAnimator.StopAnimation();
    }
}
