using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private ConfirmWindow _confirmWindow;

    [Header("Buttons")]
    [SerializeField] private Button _continueButton;
    [SerializeField] private Button _newGameButton;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _exitButton;

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

    private void OnNewGameButtonClicked()
    {
        print("new game");

        _confirmWindow.Show("Вы действительно хотите начать новую игру? Весь прогресс будет сброшен.");
        _confirmWindow.ActionConfirmed += OnNewGameConfirmed;
    }

    private void OnNewGameConfirmed(bool state)
    {
        _confirmWindow.ActionConfirmed -= OnNewGameConfirmed;

        if (state)
        {
            print("starting new game...");
        }
    }

    private void OnContinueButtonClicked()
    {
        print("continue");
    }

    private void OnSettingsButtonClicked()
    {
        print("settings");
    }

    private void OnExitButtonClicked()
    {
        _confirmWindow.Show("Вы действительно хотите выйти?");
        _confirmWindow.ActionConfirmed += OnExitConfirmed;
    }

    private void OnExitConfirmed(bool state)
    {
        _confirmWindow.ActionConfirmed -= OnExitConfirmed;

        if (state)
        {
            print("exit");
            Application.Quit();
        }
    }
}