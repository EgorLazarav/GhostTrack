using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    [Header("Sound")]
    [SerializeField] private AudioClip _punchSFX;

    [Header("Combat")]
    [SerializeField] private PlayerCombat _combat;
    [SerializeField] private Weapon _startWeapon;
    [SerializeField] private Transform _weaponPoint;
    [SerializeField] private Transform _punchPoint;
    [SerializeField] private Health _health;
    [SerializeField] private float _handsLength = 0.5f;
    [SerializeField] private LayerMask _enemyMask;
    [SerializeField] private LayerMask _weaponMask;

    [Header("Movement")]
    [SerializeField] private PlayerMovement _movement;
    [SerializeField] private float _speed = 5;
    [SerializeField] private float _noWeaponBonusSpeed = 1.5f;

    [Header("Optional")]
    [SerializeField] private float _viewRange = 3;

    [Header("Shift")]
    [SerializeField] private float _shiftDuration = 3;
    [SerializeField] private float _shiftReloadTime = 6;

    public Weapon StartWeapon => _startWeapon;
    public float ViewRange => _viewRange;

    public static event UnityAction Died;

    public void Init()
    {
        _combat.Init(_startWeapon, _weaponPoint, _handsLength, _punchPoint, _enemyMask, _weaponMask, _shiftDuration, _shiftReloadTime, _punchSFX);
        _movement.Init(_speed, _noWeaponBonusSpeed);
    }

    private void OnEnable()
    {
        PlayerInput.DropWeaponKeyPressed += OnDropWeaponKeyPressed;
        PlayerInput.MoveKeyPressing += OnMoveKeyPressing;
        PlayerInput.PickUpWeaponKeyPressed += OnPickUpWeaponKeyPressed;
        PlayerInput.PuncKeyPressed += OnPuncKeyPressed;
        PlayerInput.ShootKeyPressing += OnShootKeyPressing;
        PlayerInput.LookKeyPressed += OnLookKeyPressed;
        PlayerInput.ShiftKeyPressed += OnShiftKeyPressed;

        _health.Over += OnHealthOver;
    }

    private void OnDisable()
    {
        PlayerInput.DropWeaponKeyPressed -= OnDropWeaponKeyPressed;
        PlayerInput.MoveKeyPressing -= OnMoveKeyPressing;
        PlayerInput.PickUpWeaponKeyPressed -= OnPickUpWeaponKeyPressed;
        PlayerInput.PuncKeyPressed -= OnPuncKeyPressed;
        PlayerInput.ShootKeyPressing -= OnShootKeyPressing;
        PlayerInput.LookKeyPressed -= OnLookKeyPressed;
        PlayerInput.ShiftKeyPressed -= OnShiftKeyPressed;

        _health.Over -= OnHealthOver;
    }

    private void OnShiftKeyPressed()
    {
        _combat.TryShift();
    }

    private void OnShootKeyPressing()
    {
        _combat.TryShoot();
    }

    private void OnPuncKeyPressed()
    {
        _combat.TryPunch();
    }

    private void OnPickUpWeaponKeyPressed()
    {
        _combat.TryPickUpClosestWeapon();
    }

    private void OnMoveKeyPressing(Vector2 direction)
    {
        _movement.Move(direction, _combat.CurrentWeapon == null);
    }

    private void OnDropWeaponKeyPressed()
    {
        _combat.TryDropWeapon();
    }

    private void OnLookKeyPressed(bool state)
    {
        _movement.Move(Vector2.zero);
    }

    private void OnHealthOver()
    {
        Died?.Invoke();
        gameObject.SetActive(false);
    }
}
