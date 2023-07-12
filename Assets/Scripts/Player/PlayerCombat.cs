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
    private LayerMask _enemyMask;
    private LayerMask _weaponMask;

    public Weapon CurrentWeapon => _currentWeapon;

    public static event UnityAction<int> BulletsChanged;
    public static event UnityAction Attacked;

    public void Init(Weapon startWeapon, Transform weaponPoint, float handsLength, Transform punchPoint, LayerMask enemyMask,LayerMask weaponMask)
    {
        _weaponPoint = weaponPoint;
        _punchPoint = punchPoint;
        _handsLength = handsLength;
        _enemyMask = enemyMask;
        _weaponMask = weaponMask;

        var weapon = Instantiate(startWeapon);
        EquipWeapon(weapon);
    }

    public void TryPunch()
    {
        var hit = Physics2D.OverlapCircle(_punchPoint.position, _handsLength, _enemyMask);

        if (!hit)
            return;

        if (hit.TryGetComponent(out Health health))
        {
            health.ApplyDamage(ignoreArmor: true);
            Attacked?.Invoke();
        }
    }

    public void TryShoot()
    {
        if (_currentWeapon == null)
            return;

        if (_currentWeapon.TryShoot())
        {
            if (_currentWeapon.Data.IsShotSilenced == false)
                Attacked?.Invoke();

            BulletsChanged?.Invoke(_currentWeapon.CurrentBulletsCount);
        }
    }

    public void TryDropWeapon()
    {
        if (_currentWeapon == null)
            return;

        _currentWeapon.Throw();
        _currentWeapon = null;

        BulletsChanged?.Invoke(0);
    }

    public void TryPickUpClosestWeapon()
    {
        var closestObjects = Physics2D.OverlapCircleAll(transform.position, _handsLength, _weaponMask);
        var closestWeapon = closestObjects.FirstOrDefault(obj => obj.transform.parent == null && obj.TryGetComponent(out Weapon weapon));

        if (closestWeapon != null)
            EquipWeapon(closestWeapon.GetComponent<Weapon>());
    }

    private void EquipWeapon(Weapon newWeapon)
    {
        TryDropWeapon();

        _currentWeapon = newWeapon;
        _currentWeapon.PickUp(_weaponPoint);

        BulletsChanged?.Invoke(_currentWeapon.CurrentBulletsCount);
    }
}
