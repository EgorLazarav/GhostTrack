using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private Transform _weaponPoint;
    [SerializeField] private Weapon _currentWeapon;

    public event UnityAction<int> BulletsChanged;
    public event UnityAction Shooted;

    private void Start()
    {
        if (_currentWeapon != null)
            BulletsChanged?.Invoke(_currentWeapon.Clip.BulletsLeft);
    }

    public void TryShoot(Vector2 direction)
    {
        if (_currentWeapon == null)
            return;

        if (_currentWeapon.TryShoot(direction) == false)
            return;

        BulletsChanged?.Invoke(_currentWeapon.Clip.BulletsLeft);
        Shooted?.Invoke();
    }

    public void TryThrowWeapon()
    {
        if (_currentWeapon == null)
            return;

        _currentWeapon.Drop();
        _currentWeapon = null;

        BulletsChanged?.Invoke(0);
    }

    public void PickUpWeapon(Weapon weapon)
    {
        if (_currentWeapon != null)
            _currentWeapon.Drop();

        _currentWeapon = weapon;
        _currentWeapon.OnPickUp(_weaponPoint);

        BulletsChanged?.Invoke(_currentWeapon.Clip.BulletsLeft);
    }
}