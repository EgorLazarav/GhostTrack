using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private Player _player;

    [Header("UI")]
    [SerializeField] private BulletsDisplay _bulletsDisplay;

    [Header("Camera")]
    [SerializeField] private MainCameraController _mainCameraController;

    [Header("Enemies")]
    [SerializeField] private EnemyController[] _enemies;

    private void Awake()
    {
        _player.Init();

        _mainCameraController.Init(_player.transform);

        _bulletsDisplay.Init(_player.StartWeapon.Data.BulletsCount);

        _enemies.ToList().ForEach(e => e.Init(_player));
    }
}
