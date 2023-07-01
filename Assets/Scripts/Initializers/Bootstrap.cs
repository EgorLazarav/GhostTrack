using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private Player _player;
    [SerializeField] private PlayerCombat _playerCombat;

    [Header("UI")]
    [SerializeField] private BulletsDisplay _bulletsDisplay;

    [Header("Camera")]
    [SerializeField] private MainCameraController _mainCameraController;

    private void Awake()
    {
        _player.Init();
        _playerCombat.Init(_player.StartWeapon);

        _mainCameraController.Init(_player.transform);

        _bulletsDisplay.Init(_player.StartWeapon.Data.BulletsCount);

        var enemies = FindObjectsOfType<EnemyController>();
        enemies.ToList().ForEach(e => e.Init(_player));
    }
}
