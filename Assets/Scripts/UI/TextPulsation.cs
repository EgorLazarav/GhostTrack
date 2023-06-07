using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextPulsation : MonoBehaviour
{
    [SerializeField] private float _animationSpeed;
    [SerializeField] private float _growScaleCoef;

    private Vector3 _normalScale;
    private Vector3 _growScale;

    private void Start()
    {
        _normalScale = transform.localScale;
        _growScale = new Vector3(_growScaleCoef, _growScaleCoef, _growScaleCoef);

        StartCoroutine(Pulsating());
    }

    IEnumerator Pulsating()
    {
        while (true)
        {
            while (transform.localScale != _growScale)
            {
                transform.localScale = Vector3.MoveTowards(transform.localScale, _growScale, _animationSpeed * Time.deltaTime);
                yield return null;
            }

            while (transform.localScale != _normalScale)
            {
                transform.localScale = Vector3.MoveTowards(transform.localScale, _normalScale, _animationSpeed * Time.deltaTime);
                yield return null;
            }
        }
    }
}
