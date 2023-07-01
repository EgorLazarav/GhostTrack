using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelHandler : MonoBehaviour
{
    [SerializeField] private LevelInfoDisplay _levelInfoDisplay;

    private Player _player;

    private void OnEnable()
    {
        Player.Died += OnPlayerDied;
    }

    private void OnDisable()
    {
        Player.Died -= OnPlayerDied;
    }

    private void OnPlayerDied()
    {
        _levelInfoDisplay.Show("'R' TO RESTART");
    }
}
