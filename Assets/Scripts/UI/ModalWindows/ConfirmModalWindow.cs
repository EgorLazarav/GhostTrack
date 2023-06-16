using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ConfirmModalWindow : MonoBehaviour, IModalWindow
{
    [SerializeField] private Image _blurPanel;
    [SerializeField] private Text _headerText;
    [SerializeField] private Button _acceptButton;
    [SerializeField] private Button _declineButton;

    public event UnityAction<bool> ActionConfirmed;

    private void OnEnable()
    {
        _acceptButton.onClick.AddListener(OnAcceptButtonClicked);
        _declineButton.onClick.AddListener(OnDeclineButtonClicked);
    }

    private void OnDisable()
    {
        _acceptButton.onClick.RemoveListener(OnAcceptButtonClicked);
        _declineButton.onClick.RemoveListener(OnDeclineButtonClicked);
    }

    private void OnAcceptButtonClicked()
    {
        ActionConfirmed?.Invoke(true);
        Close();
    }

    private void OnDeclineButtonClicked()
    {
        ActionConfirmed?.Invoke(false);
        Close();
    }

    public void Show()
    {
        _blurPanel.gameObject.SetActive(true);
    }

    public void Close()
    {
        _blurPanel.gameObject.SetActive(true);
    }
}
