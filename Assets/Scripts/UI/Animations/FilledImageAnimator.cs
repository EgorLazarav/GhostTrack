using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class FilledImageAnimator : AnimatedUI
{
    private Image _image;

    protected override void Awake()
    {
        _image = GetComponent<Image>();
        _image.type = Image.Type.Filled;
        base.Awake();
    }

    protected override IEnumerator Animating()
    {
        while (_image.fillAmount < 1)
        {
            yield return null;

            _image.fillAmount += Time.unscaledDeltaTime * AnimationSpeed;
        }
    }
}
