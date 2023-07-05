using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonBackToMainMenu : ButtonClickHandler
{
    protected override void OnButtonClicked()
    {
        if (SceneManager.GetActiveScene().name == SceneNames.MainMenu.ToString())
            SettingsModalWindow.Instance.Close();
        else
            ConfirmModalWindow.Instance.Show("����� � ������� ����? �������� ������ ����� �������", SceneLoader.Instance.LoadMainMenu);
    }
}
