using System;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    [Header("Combat")]
    [SerializeField] private PlayerCombat _combat;
    [SerializeField] private Weapon _startWeapon;
    [SerializeField] private Transform _weaponPoint;
    [SerializeField] private Transform _punchPoint;
    [SerializeField] private Health _health;
    [SerializeField] private float _handsLength = 0.5f;
    [SerializeField] private LayerMask _enemyMask;
    [SerializeField] private ParticleSystem _punchVFX;

    [Header("Movement")]
    [SerializeField] private PlayerMovement _movement;
    [SerializeField] private float _speed = 5;

    public Weapon StartWeapon => _startWeapon;

    public static event UnityAction Died;

    public void Init()
    {
        _combat.Init(_startWeapon, _weaponPoint, _handsLength, _punchPoint, _enemyMask, _punchVFX);
        _movement.Init(_speed);
    }

    private void OnEnable()
    {
        PlayerInput.DropWeaponKeyPressed += OnDropWeaponKeyPressed;
        PlayerInput.MoveKeyPressing += OnMoveKeyPressing;
        PlayerInput.PickUpWeaponKeyPressed += OnPickUpWeaponKeyPressed;
        PlayerInput.PuncKeyPressed += OnPuncKeyPressed;
        PlayerInput.ShootKeyPressing += OnShootKeyPressing;

        _health.Over += OnHealthOver;
    }

    private void OnDisable()
    {
        PlayerInput.DropWeaponKeyPressed -= OnDropWeaponKeyPressed;
        PlayerInput.MoveKeyPressing -= OnMoveKeyPressing;
        PlayerInput.PickUpWeaponKeyPressed -= OnPickUpWeaponKeyPressed;
        PlayerInput.PuncKeyPressed -= OnPuncKeyPressed;
        PlayerInput.ShootKeyPressing -= OnShootKeyPressing;

        _health.Over -= OnHealthOver;
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

    private void OnHealthOver()
    {
        Died?.Invoke();
        gameObject.SetActive(false);
    }
}
