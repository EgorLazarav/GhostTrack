using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class PlayerCombat : MonoBehaviour
{
    private Transform _weaponPoint;
    private Transform _punchPoint;
    private float _handsLength = 0.5f;
    private Weapon _currentWeapon;

    public static event UnityAction<int> BulletsChanged;
    public static event UnityAction Shooted;

    public void Init(Weapon startWeapon, Transform weaponPoint, float handsLength, Transform punchPoint)
    {
        _weaponPoint = weaponPoint;
        _punchPoint = punchPoint;
        _handsLength = handsLength;

        var weapon = Instantiate(startWeapon);
        EquipWeapon(weapon);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            TryShoot();
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            TryPunch();
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

    private void TryPunch()
    {
        print("Punch");

        var hit = Physics2D.Raycast(_punchPoint.position, Vector2.right, _handsLength);

        if (!hit)
            return;

        if (hit.collider.TryGetComponent(out Health health))
            health.ApplyDamage();
    }

    private bool TryShoot()
    {
        if (_currentWeapon == null)
            return false;

        if (_currentWeapon.TryShoot())
        {
            Shooted?.Invoke();
            BulletsChanged?.Invoke(_currentWeapon.CurrentBulletsCount);
        }
        else
        {
            return false;
        }

        return true;
    }

    private bool TryDropWeapon()
    {
        if (_currentWeapon == null)
            return false;

        _currentWeapon.Throw();
        _currentWeapon = null;

        BulletsChanged?.Invoke(0);

        return true;
    }

    private bool TryEquipClosestWeapon()
    {
        var closestObjects = Physics2D.OverlapCircleAll(transform.position, _handsLength);
        var closestWeapon = closestObjects.FirstOrDefault(obj => obj.transform.parent == null && obj.TryGetComponent(out Weapon weapon));

        if (closestWeapon != null)
            EquipWeapon(closestWeapon.GetComponent<Weapon>());

        return closestWeapon != null;
    }

    private void EquipWeapon(Weapon newWeapon)
    {
        TryDropWeapon();

        _currentWeapon = newWeapon;
        _currentWeapon.PickUp(_weaponPoint);

        BulletsChanged?.Invoke(_currentWeapon.CurrentBulletsCount);
    }
}
