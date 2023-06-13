using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private MainMenu _mainMenu;
    [SerializeField] private ConfirmWindow _confirmWindow;

    private void OnEnable()
    {
        _mainMenu.ContinueButtonClicked += OnContinueButtonClicked;
        _mainMenu.NewGameButtonClicked += OnNewGameButtonClicked;
        _mainMenu.SettingsButtonClicked += OnSettingsButtonClicked;
        _mainMenu.ExitButtonClicked += OnExitButtonClicked;
    }

    private void OnDisable()
    {
        _mainMenu.ContinueButtonClicked -= OnContinueButtonClicked;
        _mainMenu.NewGameButtonClicked -= OnNewGameButtonClicked;
        _mainMenu.SettingsButtonClicked -= OnSettingsButtonClicked;
        _mainMenu.ExitButtonClicked -= OnExitButtonClicked;
    }

    private void OnContinueButtonClicked()
    {

    }

    private void OnNewGameButtonClicked()
    {
        _confirmWindow.Show("Вы действительно хотите начать новую игру? Весь прогресс будет сброшен.");
        _confirmWindow.ActionConfirmed += OnNewGameConfirmed;
    }

    private void OnSettingsButtonClicked()
    {

    }

    private void OnExitButtonClicked()
    {
        _confirmWindow.Show("Вы действительно хотите выйти?");
        _confirmWindow.ActionConfirmed += OnExitConfirmed;
    }

    private void OnNewGameConfirmed()
    {
        _confirmWindow.ActionConfirmed -= OnNewGameConfirmed;
        print("starting new game confirmed");
    }

    private void OnExitConfirmed()
    {
        _confirmWindow.ActionConfirmed -= OnExitConfirmed;
        print("exit confirmed");
        Application.Quit();
    }
}
