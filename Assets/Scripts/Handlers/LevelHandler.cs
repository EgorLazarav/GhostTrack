using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelHandler : MonoBehaviour
{
    [SerializeField] private LevelInfoDisplay _levelInfoDisplay;

    private int _enemiesCountOnLevel;

    public void Init(int enemiesCountOnLevel)
    {
        _enemiesCountOnLevel = enemiesCountOnLevel;
    }

    private void OnEnable()
    {
        PlayerController.Died += OnPlayerDied;
        EnemyController.Died += OnEnemyDied;
    }

    private void OnDisable()
    {
        PlayerController.Died -= OnPlayerDied;
        EnemyController.Died -= OnEnemyDied;
    }

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

    private void OnPlayerDied()
    {
        _levelInfoDisplay.Show("'R' TO RESTART");

        StartCoroutine(WaitingForRestart());
    }

    private void OnEnemyDied(EnemyController enemy)
    {
        _enemiesCountOnLevel--;

        if (_enemiesCountOnLevel == 0)
        {
            Car.Collider.isTrigger = true;
            _levelInfoDisplay.Show("GO TO CAR");
        }
    }

    private IEnumerator WaitingForRestart()
    {
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.R))
                SceneLoader.Instance.ReloadLevel();

            yield return null;
        }
    }
}
