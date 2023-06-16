using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ConfirmModalWindow : MonoBehaviour
{
    [SerializeField] private Image _blurPanel;
    [SerializeField] private Text _headerText;
    [SerializeField] private Button _acceptButton;
    [SerializeField] private Button _declineButton;

    public static ConfirmModalWindow Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance == this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        _blurPanel.gameObject.SetActive(false);
    }

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
        _acceptButton.onClick.RemoveAllListeners();
        _acceptButton.onClick.AddListener(OnAcceptButtonClicked);
        _blurPanel.gameObject.SetActive(false);
    }

    private void OnDeclineButtonClicked()
    {
        _acceptButton.onClick.RemoveAllListeners();
        _acceptButton.onClick.AddListener(OnAcceptButtonClicked);
        _blurPanel.gameObject.SetActive(false);
    }

    public void Show(string header, UnityAction action)
    {
        _blurPanel.gameObject.SetActive(true);
        _headerText.text = header;
        _acceptButton.onClick.AddListener(action);
    }
}
