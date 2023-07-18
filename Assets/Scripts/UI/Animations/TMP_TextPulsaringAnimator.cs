using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

[RequireComponent(typeof(TMP_Text))]
public class TMP_TextPulsaringAnimator : AnimatedUI
{
    [SerializeField] private float _maxIncreaseSize = 1.5f;
    [SerializeField] private float _maxDecreaseSize = 0.5f;

    private TMP_Text _text;

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
    }

    protected override IEnumerator Animating()
    {
        while (true)
        {
            transform.DOScale(_maxIncreaseSize, AnimationSpeed);
            yield return new WaitForSeconds(AnimationSpeed);

            transform.DOScale(_maxDecreaseSize, AnimationSpeed);
            yield return new WaitForSeconds(AnimationSpeed);
        }
    }
}
