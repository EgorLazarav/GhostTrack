using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class DialogTextDisplay : MonoBehaviour
{
    [SerializeField] private float _typeSpeed = 5;

    private Text _text;

    public event UnityAction DialogOver;

    private void Awake()
    {
        _text = GetComponent<Text>();
    }

    public void PrintText(string[] messages)
    {
        StartCoroutine(Printing(messages));
    }

    private IEnumerator Printing(string[] messages)
    {
        foreach (var message in messages)
        {
            _text.text = "";

            yield return new WaitForEndOfFrame();

            int i = 0;

            while (_text.text != message)
            {
                _text.text += message[i];
                i++;

                if (Input.anyKeyDown)
                    break;

                yield return new WaitForSecondsRealtime(Time.unscaledDeltaTime / _typeSpeed);
            }

            _text.text = message;

            yield return new WaitForEndOfFrame();

            while (!Input.anyKeyDown)
                yield return null;
        }

        DialogOver?.Invoke();
    }
}
