using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private Weapon _weapon;
    [SerializeField] private Transform _weaponPoint;
    [SerializeField] private float _pickUpWeaponRange;
    [SerializeField] private float _reloadSpeed = 1;

    public static event UnityAction<Weapon> WeaponChanged;

    private void Awake()
    {
        var weapon = Instantiate(_weapon);
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
            TryEquipWeapon();
        }


        if (Input.GetKeyDown(KeyCode.R))
        {
            TryReloadWeapon();
        }
    }

    private void TryReloadWeapon()
    {
        if (_weapon == null)
            return;

        _weapon.TryReload(_reloadSpeed);
    }

    private void TryEquipWeapon()
    {
        var closestObjects = Physics2D.OverlapCircleAll(transform.position, _pickUpWeaponRange);
        var closestWeapon = closestObjects.FirstOrDefault(obj => obj.transform.parent == null && obj.TryGetComponent(out Weapon weapon));

        if (closestWeapon != null)
            EquipWeapon(closestWeapon.GetComponent<Weapon>());
    }

    private void TryShoot()
    {
        if (_weapon == null)
            return;

        _weapon.TryShoot();
    }

    private void EquipWeapon(Weapon newWeapon)
    {
        TryDropWeapon();

        newWeapon.transform.parent = _weaponPoint;
        newWeapon.transform.rotation = _weaponPoint.rotation;
        newWeapon.transform.position = _weaponPoint.position;

        _weapon = newWeapon;
        WeaponChanged?.Invoke(newWeapon);
    }

    private void TryDropWeapon()
    {
        if (_weapon == null)
            return;

        _weapon.TryStopReloading();
        _weapon.transform.parent = null;
        _weapon = null;
        WeaponChanged?.Invoke(_weapon);
    }
}
