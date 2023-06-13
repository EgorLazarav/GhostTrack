using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

[RequireComponent(typeof(TMP_Text))]
public class TextHoverAnimator : AnimatedUI, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private float _hoverScaleSize = 1.1f;
    [SerializeField] private Color _hoverColor = Color.white;

    private TMP_Text _text;

    private VertexGradient _vertexHoverColor;
    private VertexGradient _baseColor;
    private Vector3 _baseScale;

    protected override void Awake()
    {
        _text = GetComponent<TMP_Text>();

        _baseColor = _text.colorGradient;
        _baseScale = _text.transform.localScale;
        _vertexHoverColor = new VertexGradient(_hoverColor);

        base.Awake();
    }

    protected override IEnumerator Animating()
    {
        while (true)
        {
            _text.colorGradient = _vertexHoverColor;
            _text.transform.DOScale(_hoverScaleSize, 1/ AnimationSpeed);
            yield return new WaitForSecondsRealtime(1 / AnimationSpeed);

            _text.colorGradient = _vertexHoverColor;
            _text.transform.DOScale(_baseScale, 1 / AnimationSpeed);
            yield return new WaitForSecondsRealtime(1 / AnimationSpeed);
        }
    }

    public override void StopAnimation()
    {
        base.StopAnimation();

        _text.colorGradient = _baseColor;
        _text.transform.DOScale(_baseScale, 1 / AnimationSpeed);
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
