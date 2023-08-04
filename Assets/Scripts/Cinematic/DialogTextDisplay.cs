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

                yield return new WaitForEndOfFrame();

                if (Input.GetKeyDown(KeyCode.Mouse0))
                    break;
            }

            _text.text = message;

            yield return new WaitForEndOfFrame();

            while (!Input.GetKeyDown(KeyCode.Mouse0))
                yield return null;
        }

        DialogOver?.Invoke();
    }
}
