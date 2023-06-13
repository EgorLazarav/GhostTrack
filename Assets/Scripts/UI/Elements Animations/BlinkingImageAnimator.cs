using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class BlinkingImageAnimator : AnimatedUI
{
    [SerializeField][Range(0,1)] private float _minAlpha = 0;
    [SerializeField][Range(0, 1)] private float _maxAlpha = 1;
    [SerializeField][Range(1, 10)] private int _frequency = 1;

    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
    } 

    protected override IEnumerator Animating()
    {
        while (true)
        {
            yield return null;
        }
    }
}
