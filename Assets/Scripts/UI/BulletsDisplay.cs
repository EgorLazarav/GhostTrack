using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class BulletsDisplay : MonoBehaviour
{
    private Text _text;
    private Weapon _currentWeapon;

    private void Awake()
    {
        _text = GetComponent<Text>();
    }

    private void OnEnable()
    {
        PlayerCombat.WeaponChanged += OnPlayerWeaponChanged;
    }

    private void OnDisable()
    {
        PlayerCombat.WeaponChanged -= OnPlayerWeaponChanged;

        if (_currentWeapon != null)
            _currentWeapon.BulletsChanged -= OnPlayerCurrentWeaponBulletsChanged;
    }

    private void OnPlayerWeaponChanged(Weapon newWeapon)
    {
        if (_currentWeapon != null)
            _currentWeapon.BulletsChanged -= OnPlayerCurrentWeaponBulletsChanged;

        if (newWeapon == null)
        {
            _text.text = "NO WEAPON";
        }
        else
        {
            _currentWeapon = newWeapon;
            _currentWeapon.BulletsChanged += OnPlayerCurrentWeaponBulletsChanged;
            OnPlayerCurrentWeaponBulletsChanged(_currentWeapon.CurrentBulletsInClip, _currentWeapon.BulletsLeft);
        }
    }

    private void OnPlayerCurrentWeaponBulletsChanged(int currentBulletsInClip, int bulletsLeft)
    {
        _text.text = $"{currentBulletsInClip}/{bulletsLeft}";
    }
}
