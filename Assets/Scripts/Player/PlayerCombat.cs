using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private Weapon _currentWeapon;
    [SerializeField] private Transform _weaponPoint;
    [SerializeField] private float _pickUpWeaponRange = 0.5f;

    public static event UnityAction<Weapon> WeaponChanged;

    public void Init()
    {
        if (_currentWeapon == null)
            return;

        var weapon = Instantiate(_currentWeapon);
        EquipWeapon(weapon);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            TryShoot();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            TryDropWeapon();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            TryEquipClosestWeapon();
        }
    }

    private void TryShoot()
    {
        if (_currentWeapon == null)
            return;

        _currentWeapon.TryShoot();
    }

    private void TryDropWeapon()
    {
        if (_currentWeapon == null)
            return;

        _currentWeapon.Throw();
        _currentWeapon = null;

        WeaponChanged?.Invoke(_currentWeapon);
    }

    private void TryEquipClosestWeapon()
    {
        var closestObjects = Physics2D.OverlapCircleAll(transform.position, _pickUpWeaponRange);
        var closestWeapon = closestObjects.FirstOrDefault(obj => obj.transform.parent == null && obj.TryGetComponent(out Weapon weapon));

        if (closestWeapon != null)
            EquipWeapon(closestWeapon.GetComponent<Weapon>());
    }

    private void EquipWeapon(Weapon newWeapon)
    {
        TryDropWeapon();

        _currentWeapon = newWeapon;
        _currentWeapon.PickUp(_weaponPoint);

        WeaponChanged?.Invoke(newWeapon);
    }
}
