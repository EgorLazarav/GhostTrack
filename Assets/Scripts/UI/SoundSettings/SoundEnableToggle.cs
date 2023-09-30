using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class SoundEnableToggle : MonoBehaviour
{
    private Toggle _toggle;

    private void Awake()
    {
        _toggle = GetComponent<Toggle>();
    }

    private void OnEnable()
    {
        _toggle.onValueChanged.AddListener(OnToggleValueChanged);
    }

    private void OnDisable()
    {
        _toggle.onValueChanged.RemoveListener(OnToggleValueChanged);
    }

    private void OnToggleValueChanged(bool state)
    {
        // AudioListener.volume = Convert.ToInt32(state);
        AudioListener.pause = !state;
    }
}
