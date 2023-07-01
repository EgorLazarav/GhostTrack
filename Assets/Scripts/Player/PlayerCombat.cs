using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private Transform _weaponPoint;
    [SerializeField] private float _handsLength = 0.5f;

    private Weapon _currentWeapon;

    public static event UnityAction<int> BulletsChanged;
    public static event UnityAction Shooted;

    public void Init(Weapon startWeapon)
    {
        var weapon = Instantiate(startWeapon);
        EquipWeapon(weapon);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (_currentWeapon == null)
                Punch();
            else
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

    private void Punch()
    {
        print("Punch");
        var objects = Physics2D.OverlapCircleAll(transform.position, _handsLength);

        foreach (var obj in objects)
        {
            if (obj.TryGetComponent(out Health health) && obj.TryGetComponent(out Player player) == false)
            {
                health.ApplyDamage();
            }
        }
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
