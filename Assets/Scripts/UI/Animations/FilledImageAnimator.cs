using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class FilledImageAnimator : AnimatedUI
{
    private Image _image;

    protected override void OnEnable()
    {
        _image = GetComponent<Image>();
        _image.type = Image.Type.Filled;
        base.OnEnable();
    }

    protected override IEnumerator Animating()
    {
        _image.fillAmount = 0;

        while (_image.fillAmount < 1)
        {
            yield return null;

            _image.fillAmount += Time.unscaledDeltaTime * AnimationSpeed;
        }
    }
}
