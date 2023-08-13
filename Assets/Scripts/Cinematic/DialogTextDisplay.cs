using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class DialogTextDisplay : MonoBehaviour
{
    [SerializeField] private float _timeToShowMessage = 2;
    [SerializeField] private TMP_Text _text;

    private Coroutine _coroutine;
    private float _currentMessageTimer;

    public event UnityAction DialogOver;

    private void HandleEndOfDialog()
    {
        DialogOver?.Invoke();
        StopCoroutine(_coroutine);
        _text.text = "";
        _coroutine = null;
    }

    public void StartDialog(string[] messages)
    {
        _coroutine = StartCoroutine(Printing(messages));
    }

    private IEnumerator Printing(string[] messages)
    {
        float delay = 1f;

        yield return new WaitForSecondsRealtime(delay);

        foreach (var message in messages)
        {
            yield return new WaitForEndOfFrame();

            _text.text = "";
            int i = 0;

            while (_text.text != message)
            {
                _text.text += message[i];
                i++;

                yield return null;
            }

            _text.text = message;
            _currentMessageTimer = _timeToShowMessage;

            while (_currentMessageTimer > 0)
            {
                _currentMessageTimer -= Time.unscaledDeltaTime;

                yield return null;
            }
        }

        HandleEndOfDialog();
    }
}
