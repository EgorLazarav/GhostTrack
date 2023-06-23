using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private PlayerCombat _playerCombat;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private PlayerRotator _playerRotator;

    /*
    [Header("UI")]
    [SerializeField] private BulletsDisplay _bulletsDisplay;
    */

    [Header("Camera")]
    [SerializeField] private MainCameraController _mainCameraController;

    private void Awake()
    {
        _mainCameraController.Init();

        // _bulletsDisplay.Init();

        _playerCombat.Init();
        _playerMovement.Init();
        _playerRotator.Init();
    }
}
