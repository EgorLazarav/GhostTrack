using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button _continueButton;
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _exitButton;

    private void OnEnable()
    {
        _startButton.onClick.AddListener(OnStartButtonClicked);
        _continueButton.onClick.AddListener(OnContinueButtonClicked);
        _settingsButton.onClick.AddListener(OnSettingsButtonClicked);
        _exitButton.onClick.AddListener(OnExitButtonClicked);
    }

    private void OnDisable()
    {
        _startButton.onClick.RemoveListener(OnStartButtonClicked);
        _continueButton.onClick.RemoveListener(OnContinueButtonClicked);
        _settingsButton.onClick.RemoveListener(OnSettingsButtonClicked);
        _exitButton.onClick.RemoveListener(OnExitButtonClicked);
    }

    private void Start()
    {
        _continueButton.interactable = PlayerPrefs.HasKey(Constants.LastSavedSceneIndex) && PlayerPrefs.GetInt(Constants.LastSavedSceneIndex) != 0;
        // _continueButton.interactable = false;

        if (_continueButton.interactable == false)
        {
            _continueButton.TryGetComponent(out ButtonAnimator buttonAnimator);
            buttonAnimator.enabled = false;
            buttonAnimator.Text.color = new Color(0, 0, 0, 0.5f);
        }
    }

    private void OnStartButtonClicked()
    {
        SceneLoader.LoadNewGameScene();
    }

    private void OnContinueButtonClicked()
    {
        Time.timeScale = 1;
        SceneLoader.LoadLastSavedScene();
    }

    private void OnSettingsButtonClicked()
    {
        print("settings");
    }

    private void OnExitButtonClicked()
    {
        print("exit");
        Application.Quit();
    }
}
