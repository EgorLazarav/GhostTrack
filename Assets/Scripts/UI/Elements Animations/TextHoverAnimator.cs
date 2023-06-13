using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(TMP_Text))]
public class TextHoverAnimator : AnimatedUI
{
    [SerializeField] private float _hoverScaleSize = 1.1f;
    [SerializeField] private Color _hoverColor = Color.white;

    private TMP_Text _text;

    private VertexGradient _vertexHoverColor;
    private VertexGradient _baseColor;
    private Vector3 _baseScale;

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();

        _baseColor = _text.colorGradient;
        _baseScale = _text.transform.localScale;
        _vertexHoverColor = new VertexGradient(_hoverColor);
    }

    protected override IEnumerator Animating()
    {
        _text.colorGradient = _vertexHoverColor;

        while (true)
        {
            _text.transform.DOScale(_hoverScaleSize, 1/ AnimationSpeed);
            yield return new WaitForSecondsRealtime(1 / AnimationSpeed);

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
}
