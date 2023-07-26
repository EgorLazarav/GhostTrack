using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Collider2D))]
public class PlayerCombat : MonoBehaviour
{
    private Transform _weaponPoint;
    private Transform _punchPoint;
    private float _handsLength;
    private float _shiftDuration;
    private float _shiftReloadTime;
    private Weapon _currentWeapon;
    private LayerMask _enemyMask;
    private LayerMask _weaponMask;
    private SpriteRenderer _spriteRenderer;
    private WaitForSeconds _shiftDelay;
    private WaitForSeconds _shiftCooldown;
    private Coroutine _shiftingCoroutine;
    private Collider2D _collider;
    private AudioClip _punchSFX;

    public Weapon CurrentWeapon => _currentWeapon;

    public static event UnityAction<int> BulletsChanged;
    public static event UnityAction Attacked;

    public void Init(Weapon startWeapon, Transform weaponPoint, float handsLength, Transform punchPoint, LayerMask enemyMask,LayerMask weaponMask, float shiftDuration, float shiftReloadTime, AudioClip punchSFX)
    {
        _weaponPoint = weaponPoint;
        _punchPoint = punchPoint;
        _handsLength = handsLength;
        _enemyMask = enemyMask;
        _weaponMask = weaponMask;
        _shiftDuration = shiftDuration;
        _shiftReloadTime = shiftReloadTime;
        _punchSFX = punchSFX;

        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<Collider2D>();

        _shiftDelay = new WaitForSeconds(_shiftDuration);
        _shiftCooldown = new WaitForSeconds(_shiftReloadTime);

        var weapon = Instantiate(startWeapon);
        EquipWeapon(weapon);
    }

    public void TryPunch()
    {
        if (_shiftingCoroutine != null)
            return;

        var hit = Physics2D.OverlapCircle(_punchPoint.position, _handsLength, _enemyMask);
        AudioManager.Instance.PlaySound(_punchSFX);

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

    public void EquipWeapon(Weapon newWeapon)
    {
        TryDropWeapon();

        _currentWeapon = newWeapon;
        _currentWeapon.PickUp(_weaponPoint);

        BulletsChanged?.Invoke(newWeapon.CurrentBulletsCount);
    }

    public void TryShift()
    {
        if (_shiftingCoroutine != null)
            return;

        _shiftingCoroutine = StartCoroutine(Shifting());
    }

    private IEnumerator Shifting()
    {
        float baseAlpha = _spriteRenderer.color.a;
        float shiftAlpha = 0.5f;

        _spriteRenderer.color = _spriteRenderer.color.SetAlpha(shiftAlpha);
        _collider.enabled = false;
        TryDropWeapon();

        yield return _shiftDelay;

        _spriteRenderer.color = _spriteRenderer.color.SetAlpha(baseAlpha);
        _collider.enabled = true;

        yield return _shiftCooldown;

        _shiftingCoroutine = null;
    }
}
