using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelCompleteHandler : MonoBehaviour
{
    private int _enemiesCountOnLevel;

    public event UnityAction LevelCompleted;

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

    private void OnPlayerDied()
    {
        StartCoroutine(WaitingForRestart());
    }

    private void OnEnemyDied(EnemyController enemy)
    {
        _enemiesCountOnLevel--;

        if (_enemiesCountOnLevel == 0)
            LevelCompleted?.Invoke();
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
