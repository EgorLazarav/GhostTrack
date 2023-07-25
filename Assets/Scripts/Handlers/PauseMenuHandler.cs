using UnityEngine;

public class PauseMenuHandler : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 0)
            {
                PlayerInput.Instance.enabled = true;
                Time.timeScale = 1;
                SettingsModalWindow.Instance.Close();
                AudioManager.Instance.ContinuePlaybackAll();
            }
            else
            {
                PlayerInput.Instance.enabled = false;
                Time.timeScale = 0;
                SettingsModalWindow.Instance.Show();
                AudioManager.Instance.StopPlaybackAll();
            }
        }
    }

    private void OnDisable()
    {
        PlayerInput.Instance.enabled = true;
    }
}