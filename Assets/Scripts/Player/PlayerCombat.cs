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
    public static event UnityAction Shooted;

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
        AudioManager.Instance.PlayPunchSFX();

        var hit = Physics2D.OverlapCircle(_punchPoint.position, _handsLength, _enemyMask);

        if (!hit)
            return;

        if (hit.TryGetComponent(out Health health))
            health.ApplyDamage(ignoreArmor: true);
    }

    public bool TryShoot()
    {
        if (_currentWeapon == null)
            return false;

        if (_currentWeapon.TryShoot())
        {
            Shooted?.Invoke();
            BulletsChanged?.Invoke(_currentWeapon.CurrentBulletsCount);

            return true;
        }
        else
        {
            if (_currentWeapon.CurrentBulletsCount <= 0)
                AudioManager.Instance.TryPlayEmptyClipSFX();

            return false;
        }
    }

    public bool TryDropWeapon()
    {
        if (_currentWeapon == null)
            return false;

        _currentWeapon.Throw();
        _currentWeapon = null;

        BulletsChanged?.Invoke(0);
        AudioManager.Instance.PlayDropWeaponSFX();

        return true;
    }

    public bool TryPickUpClosestWeapon()
    {
        var closestObjects = Physics2D.OverlapCircleAll(transform.position, _handsLength, _weaponMask);
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
        AudioManager.Instance.PlayPickUpWeaponSFX();

        BulletsChanged?.Invoke(_currentWeapon.CurrentBulletsCount);
    }
}
