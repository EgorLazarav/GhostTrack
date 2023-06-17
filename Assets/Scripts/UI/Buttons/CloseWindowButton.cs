using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class CloseWindowButton : MonoBehaviour
{
    [SerializeField] private GameObject _closableWindow;

    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnCloseButtonClicked);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnCloseButtonClicked);
    }

    private void OnCloseButtonClicked()
    {
        _closableWindow.SetActive(false);
    }
}
