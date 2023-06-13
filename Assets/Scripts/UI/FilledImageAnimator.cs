using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class FilledImageAnimator : MonoBehaviour, IAnimatedUI
{
    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _image.type = Image.Type.Filled;
    }

    public void StartAnimation()
    {

    }

    public void StopAnimation()
    {

    }
}
