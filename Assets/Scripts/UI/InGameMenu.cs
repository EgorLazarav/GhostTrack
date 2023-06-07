using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameMenu : MonoBehaviour
{
    [SerializeField] private GameObject _blurLayer;

    [SerializeField] private Button _continueButton;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _exitButton;

    private bool _state = false;

    private void OnEnable()
    {
        PlayerInput.OpenInGameMenuKeyPressed += OnOpenMenuKeyPressed;

        _continueButton.onClick.AddListener(OnContinueButtonClicked);
        _settingsButton.onClick.AddListener(OnSettingsButtonClicked);
        _exitButton.onClick.AddListener(OnExitButtonClicked);
    }

    private void OnDisable()
    {
        PlayerInput.OpenInGameMenuKeyPressed -= OnOpenMenuKeyPressed;

        _continueButton.onClick.RemoveListener(OnContinueButtonClicked);
        _settingsButton.onClick.RemoveListener(OnSettingsButtonClicked);
        _exitButton.onClick.RemoveListener(OnExitButtonClicked);
    }

    private void OnOpenMenuKeyPressed()
    {
        if (_state == false)
        {
            _blurLayer.SetActive(true);
            Time.timeScale = 0;
            _state = true;
        }
        else
        {
            _blurLayer.SetActive(false);
            Time.timeScale = 1;
            _state = false;
        }
    }

    private void OnContinueButtonClicked()
    {
        _blurLayer.SetActive(false);
        Time.timeScale = 1;
        _state = false;
    }

    private void OnSettingsButtonClicked()
    {
        print("а их нет");
    }

    private void OnExitButtonClicked()
    {
        SceneLoader.LoadMainMenuScene();
    }
}
