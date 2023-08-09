using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class KeyBindDisplay : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Text _keyText;
    [SerializeField] private Text _actionText;

    private KeyCode _keyCode;
    private Keys _action;

    public static event UnityAction<Keys, KeyCode> KeyBinded;

    private void OnEnable()
    {
        _button.onClick.AddListener(OnButtonClicked);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnButtonClicked);
    }

    public void Init(KeyCode key, Keys action)
    {
        _keyCode = key;
        _action = action;

        _keyText.text = key.ToString();

        var actionName = string.Join(" ", Regex.Split(action.ToString(), "(?=\\p{Lu})"));
        _actionText.text = actionName;
    }

    private void OnButtonClicked()
    {
        StartCoroutine(BindingKeyOnInput());
    }

    private IEnumerator BindingKeyOnInput()
    {
        bool isKeyPressed = false;
        _keyText.text = "";

        while (!isKeyPressed)
        {
            if (Input.anyKeyDown)
            {
                foreach (KeyCode key in Enum.GetValues(typeof(KeyCode)))
                {
                    if (Input.GetKeyDown(key))
                    {
                        isKeyPressed = true;
                        _keyCode = key;
                        break;
                    }
                }
            }

            yield return null;
        }

        _keyText.text = _keyCode.ToString();
        KeyBinded?.Invoke(_action, _keyCode);
    }
}
