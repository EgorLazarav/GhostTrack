using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class BlinkingImageAnimator : AnimatedUI
{
    [SerializeField][Range(1f, 10f)] private float _frequency = 10;
    [SerializeField][Range(0f, 1f)] private float _minAlpha = 0.65f;
    [SerializeField][Range(0f, 1f)] private float _maxAlpha = 0.9f;

    private Image _image;
    private float _baseAlpha;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _baseAlpha = _image.color.a;
    }

    protected override IEnumerator Animating()
    {
        _image.color.SetAlpha(_baseAlpha);

        while (true)
        {
            _image.color = _image.color.SetAlpha(Random.Range(_minAlpha, _maxAlpha));
            Color newColor = _image.color.SetAlpha(_baseAlpha);

            float timer = 1 / _frequency;

            while (timer > 0)
            {
                timer -= AnimationSpeed * Time.unscaledDeltaTime;
                _image.color = Color.Lerp(_image.color, newColor, AnimationSpeed * Time.unscaledDeltaTime);
                yield return null;
            }
        }
    }
}
