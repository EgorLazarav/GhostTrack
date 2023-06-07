using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public enum LevelStates
{
    InProgress,
    Defeat,
    Victory
}

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Player _player;

    private int _enemiesLeft;

    public static event UnityAction<LevelStates> GameOver;

    private void Start()
    {
        _enemiesLeft = FindObjectsOfType<Enemy>().Length;
    }

    private void OnEnable()
    {
        Enemy.Died += OnEnemyDied;
        _player.Died += OnPlayerDied;
    }

    private void OnDisable()
    {
        Enemy.Died -= OnEnemyDied;
        _player.Died -= OnPlayerDied;
    }

    private void OnEnemyDied(Enemy enemy)
    {
        _enemiesLeft--;

        if (_enemiesLeft == 0)
            GameOver?.Invoke(LevelStates.Victory);
    }

    private void OnPlayerDied()
    {
        GameOver?.Invoke(LevelStates.Defeat);
        PlayerInput.RestartKeyPressed += OnRestartKeyPressed;
    }

    private void OnRestartKeyPressed()
    {
        PlayerInput.RestartKeyPressed -= OnRestartKeyPressed;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
