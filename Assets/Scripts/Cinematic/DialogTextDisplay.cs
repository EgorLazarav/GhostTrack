using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class DialogTextDisplay : MonoBehaviour
{
    [SerializeField] private float _typeSpeed = 5;

    private Text _text;

    private void Awake()
    {
        _text = GetComponent<Text>();
    }

    public void PrintText(string message)
    {
        StartCoroutine(Printing(message));
    }

    private IEnumerator Printing(string message)
    {
        var tween = _text.DOText(message, 1 / _typeSpeed);

        while (_text.text != message)
        {
            if (Input.anyKeyDown)
            {
                tween.Kill();
                break;
            }

            yield return null;
        }

        _text.text = message;
    }
}
