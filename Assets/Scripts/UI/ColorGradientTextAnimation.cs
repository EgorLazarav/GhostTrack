using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ColorGradientTextAnimation : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private float _animationSpeed = 1;
    [SerializeField] private Color _firstColor;
    [SerializeField] private Color _secondColor;

    private Color _currentFirstColor;
    private Color _currentSecondColor;

    private void Start()
    {
        _currentFirstColor = _firstColor;
        _currentSecondColor = _secondColor;

        StartCoroutine(Animating());
    }

    private IEnumerator Animating()
    {
        while (true)
        {
            _currentFirstColor = _firstColor;
            _currentSecondColor = _secondColor;

            while (_currentFirstColor != _secondColor && _currentSecondColor != _firstColor)
            {
                _currentFirstColor = Color.Lerp(_currentFirstColor, _secondColor, Time.deltaTime * _animationSpeed);
                _currentSecondColor = Color.Lerp(_currentSecondColor, _firstColor, Time.deltaTime * _animationSpeed);

                _text.colorGradient = new VertexGradient(_currentFirstColor, _currentSecondColor, _currentFirstColor, _currentSecondColor);

                yield return null;
            }

            var temp = _firstColor;
            _firstColor = _secondColor;
            _secondColor = temp;
        }
    }
}
