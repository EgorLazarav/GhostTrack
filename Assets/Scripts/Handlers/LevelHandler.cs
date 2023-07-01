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
        Car.PlayerEntered += OnPlayerEnteredCar;
    }

    private void OnDisable()
    {
        PlayerController.Died -= OnPlayerDied;
        EnemyController.Died -= OnEnemyDied;
        Car.PlayerEntered += OnPlayerEnteredCar;
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

    private void OnPlayerEnteredCar()
    {
        SceneLoader.Instance.ReloadLevel();
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
