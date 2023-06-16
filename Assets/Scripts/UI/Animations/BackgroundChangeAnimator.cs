using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class BackgroundChangeAnimator : AnimatedUI
{
    [SerializeField] private Sprite[] _frames;

    private Image _image;

    protected override void OnEnable()
    {
        _image = GetComponent<Image>();
        base.OnEnable();
    }

    protected override IEnumerator Animating()
    {
        while (true)
        {
            foreach (var frame in _frames)
            {
                _image.sprite = frame;
                yield return new WaitForSecondsRealtime(1 / AnimationSpeed);
            }
        }
    }
}
