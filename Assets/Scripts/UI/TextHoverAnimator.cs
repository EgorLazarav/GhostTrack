using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(TMP_Text))]
public class TextHoverAnimator : MonoBehaviour, IAnimatedUI
{
    [SerializeField] private float _hoverScaleSize = 1.1f;
    [SerializeField] private float _animationSpeed = 1;
    [SerializeField] private Color _hoverColor = Color.white;

    private TMP_Text _text;
    private Coroutine _growingCoroutine;

    private VertexGradient _vertexHoverColor;
    private Vector3 _baseScale;
    private VertexGradient _baseColor;

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();

        _baseColor = _text.colorGradient;
        _baseScale = _text.transform.localScale;
        _vertexHoverColor = new VertexGradient(_hoverColor);
    }

    private IEnumerator Hovering()
    {
        while (true)
        {
            _text.transform.DOScale(_hoverScaleSize, 1/_animationSpeed);
            yield return new WaitForSeconds(1 / _animationSpeed);

            _text.transform.DOScale(_baseScale, 1 / _animationSpeed);
            yield return new WaitForSeconds(1 / _animationSpeed);
        }
    }

    public void StartAnimation()
    {
        _growingCoroutine = StartCoroutine(Hovering());
        _text.colorGradient = _vertexHoverColor;
    }

    public void StopAnimation()
    {
        if (_growingCoroutine != null)
            StopCoroutine(_growingCoroutine);

        _text.colorGradient = _baseColor;
        _text.transform.DOScale(_baseScale, 1 / _animationSpeed);
    }
}
