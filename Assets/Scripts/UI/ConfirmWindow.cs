using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ConfirmWindow : MonoBehaviour
{
    [SerializeField] private Image _blurPanel;
    [SerializeField] private Text _headerText;
    [SerializeField] private Button _acceptButton;
    [SerializeField] private Button _declineButton;

    public event UnityAction ActionConfirmed;

    private void OnEnable()
    {
        _acceptButton.onClick.AddListener(OnAcceptButtonClicked);
        _declineButton.onClick.AddListener(() => _blurPanel.gameObject.SetActive(false));
    }

    private void OnDisable()
    {
        _acceptButton.onClick.RemoveListener(OnAcceptButtonClicked);
        _declineButton.onClick.RemoveListener(() => _blurPanel.gameObject.SetActive(false));
    }

    private void OnAcceptButtonClicked()
    {
        ActionConfirmed?.Invoke();
        _blurPanel.gameObject.SetActive(false);
    }

    public void Show(string header)
    {
        _blurPanel.gameObject.SetActive(true);
        _headerText.text = header;
    }
}
