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
            }
            else
            {
                PlayerInput.Instance.enabled = false;
                Time.timeScale = 0;
                SettingsModalWindow.Instance.Show();
            }
        }
    }

    private void OnDisable()
    {
        PlayerInput.Instance.enabled = true;
    }
}