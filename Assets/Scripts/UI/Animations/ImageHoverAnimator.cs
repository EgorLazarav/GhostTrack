using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

[RequireComponent(typeof(Image))]
public class ImageHoverAnimator : AnimatedUI, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private float _hoverScaleSize = 1.1f;
    [SerializeField] private Color _hoverColor = Color.white;

    private Image _image;

    private Color _baseColor;
    private Vector3 _baseScale;

    protected override void OnEnable()
    {
        _image = GetComponent<Image>();

        _baseColor = _image.color;
        _baseScale = _image.transform.localScale;

        base.OnEnable();
    }

    protected override IEnumerator Animating()
    {
        while (true)
        {
            _image.color = _hoverColor;
            _image.transform.DOScale(_hoverScaleSize, 1 / AnimationSpeed);
            yield return new WaitForSecondsRealtime(1 / AnimationSpeed);

            _image.color = _hoverColor;
            _image.transform.DOScale(_baseScale, 1 / AnimationSpeed);
            yield return new WaitForSecondsRealtime(1 / AnimationSpeed);
        }
    }

    public override void StopAnimation()
    {
        base.StopAnimation();

        _image.color = _baseColor;
        _image.transform.DOScale(_baseScale, 1 / AnimationSpeed);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        StartAnimation();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StopAnimation();
    }
}