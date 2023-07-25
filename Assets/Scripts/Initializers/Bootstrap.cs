using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private PlayerController _player;

    [Header("Handlers")]
    [SerializeField] private LevelCompleteHandler _levelHandler;
    [SerializeField] private PlayerScoreHandler _playerScoreHandler;

    [Header("Camera")]
    [SerializeField] private MainCameraController _mainCameraController;

    [Header("UI")]
    [SerializeField] private LevelInfoDisplay _levelInfoDisplay;

    [Header("Enemies")]
    [SerializeField] private EnemyController[] _enemies;

    private void Awake()
    {
        _player.Init();

        _levelHandler.Init(_enemies.Length);
        _playerScoreHandler.Init(_enemies.Length);

        _mainCameraController.Init(_player.transform, _player.ViewRange);

        _levelInfoDisplay.Init(_player.StartWeapon.Data.BulletsCount);

        _enemies.ToList().ForEach(e => e.Init(_player));
    }
}
