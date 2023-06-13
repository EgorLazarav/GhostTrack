using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button _continueButton;
    [SerializeField] private Button _newGameButton;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _exitButton;

    public event UnityAction ContinueButtonClicked;
    public event UnityAction NewGameButtonClicked;
    public event UnityAction SettingsButtonClicked;
    public event UnityAction ExitButtonClicked;

    private void OnEnable()
    {
        _continueButton.onClick.AddListener(OnContinueButtonClicked);
        _newGameButton.onClick.AddListener(OnNewGameButtonClicked);
        _settingsButton.onClick.AddListener(OnSettingsButtonClicked);
        _exitButton.onClick.AddListener(OnExitButtonClicked);
    }

    private void OnDisable()
    {
        _continueButton.onClick.RemoveListener(OnContinueButtonClicked);
        _newGameButton.onClick.RemoveListener(OnNewGameButtonClicked);
        _settingsButton.onClick.RemoveListener(OnSettingsButtonClicked);
        _exitButton.onClick.RemoveListener(OnExitButtonClicked);
    }

    private void OnContinueButtonClicked()
    {
        print("continue button clicked");
        ContinueButtonClicked?.Invoke();
    }

    private void OnNewGameButtonClicked()
    {
        print("new game button clicked");
        NewGameButtonClicked?.Invoke();
    }

    private void OnSettingsButtonClicked()
    {
        print("settings button clicked");
        SettingsButtonClicked?.Invoke();
    }

    private void OnExitButtonClicked()
    {
        print("exit button clicked");
        ExitButtonClicked?.Invoke();
    }
}