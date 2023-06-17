using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public abstract class ButtonClickHandler : MonoBehaviour
{
    protected Button Button;

    private void Awake()
    {
        Button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        Button.onClick.AddListener(OnButtonClicked);   
    }

    private void OnDisable()
    {
        Button.onClick.RemoveListener(OnButtonClicked);
    }

    protected abstract void OnButtonClicked();
}
