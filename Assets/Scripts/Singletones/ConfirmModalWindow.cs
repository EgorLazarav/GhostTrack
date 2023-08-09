using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class ConfirmModalWindow : MonoBehaviour
{
    [SerializeField] private Image _blurPanel;
    [SerializeField] private TMP_Text _headerText;
    [SerializeField] private Button _acceptButton;
    [SerializeField] private Button _declineButton;

    public static ConfirmModalWindow Instance { get; private set; } = null;

    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        _acceptButton.onClick.AddListener(OnButtonClicked);
        _declineButton.onClick.AddListener(OnButtonClicked);
    }

    private void OnDisable()
    {
        _acceptButton.onClick.RemoveListener(OnButtonClicked);
        _declineButton.onClick.RemoveListener(OnButtonClicked);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && _blurPanel.gameObject.activeSelf)
        {
            _acceptButton.onClick.RemoveAllListeners();
            _acceptButton.onClick.AddListener(OnButtonClicked);
            _blurPanel.gameObject.SetActive(false);
        }
    }

    private void OnButtonClicked()
    {
        _acceptButton.onClick.RemoveAllListeners();
        _acceptButton.onClick.AddListener(OnButtonClicked);
        _blurPanel.gameObject.SetActive(false);
    }

    public void Show(string header, UnityAction action)
    {
        _blurPanel.gameObject.SetActive(true);
        _headerText.text = header;
        _acceptButton.onClick.AddListener(action);
    }
}
