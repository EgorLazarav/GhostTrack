using System;
using UnityEngine;

public class GamePauseManager : MonoBehaviour
{
    public static GamePauseManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        PlayerInput.PauseKeyPressed += OnPauseKeyPressed;
    }

    private void OnDisable()
    {
        PlayerInput.PauseKeyPressed -= OnPauseKeyPressed;
    }

    private void OnPauseKeyPressed()
    {
        if (Time.timeScale == 0)
        {
            Unpause();
            SettingsModalWindow.Instance.Close();
        }
        else
        {
            Pause();
            SettingsModalWindow.Instance.Show();
        }
    }

    public void Unpause()
    {
        print("Unpause");
        Time.timeScale = 1;
    }

    public void Pause()
    {
        print("pause");
        Time.timeScale = 0;
    }
}