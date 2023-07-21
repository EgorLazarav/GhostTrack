using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyBindDisplay : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Text _keyText;
    [SerializeField] private Text _actionText;

    public KeyCode CurrentKeyBind { get; private set; }
    public Keys BindingKey { get; private set; }

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
        CurrentKeyBind = key;
        BindingKey = action;

        _keyText.text = key.ToString();
        _actionText.text = action.ToString();
    }

    private void OnButtonClicked()
    {
        StartCoroutine(CheckingInput());
    }

    private IEnumerator CheckingInput()
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
                        CurrentKeyBind = key;
                        break;
                    }
                }
            }

            yield return null;
        }

        _keyText.text = CurrentKeyBind.ToString();
    }
}
