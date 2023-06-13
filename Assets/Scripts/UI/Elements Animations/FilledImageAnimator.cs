using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class FilledImageAnimator : MonoBehaviour, IAnimatedUI
{
    [SerializeField] private float _speed;

    private Image _image;
    private Coroutine _fillingCoroutine;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _image.type = Image.Type.Filled;
        StartAnimation();
    }

    public void StartAnimation()
    {
        _fillingCoroutine = StartCoroutine(Filling());
    }

    public void StopAnimation()
    {
        if (_fillingCoroutine != null)
            StopCoroutine(_fillingCoroutine);

        _image.fillAmount = 1;
    }

    private IEnumerator Filling()
    {
        while (_image.fillAmount < 1)
        {
            yield return new WaitForSeconds(Time.deltaTime/_speed);
            _image.fillAmount += Time.deltaTime * _speed;
        }
    }
}
