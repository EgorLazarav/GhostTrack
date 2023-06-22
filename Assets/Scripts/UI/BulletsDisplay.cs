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

        if (_currentWeapon != null)
            _currentWeapon.BulletsChanged += OnBulletsChanged;
    }

    private void OnDisable()
    {
        PlayerCombat.WeaponChanged -= OnPlayerWeaponChanged;

        if (_currentWeapon != null)
            _currentWeapon.BulletsChanged -= OnBulletsChanged;
    }

    private void OnPlayerWeaponChanged(Weapon newWeapon)
    {
        if (_currentWeapon != null)
            _currentWeapon.BulletsChanged -= OnBulletsChanged;

        _currentWeapon = newWeapon;

        if (newWeapon == null)
            _text.text = "NO WEAPON";
        else
            newWeapon.BulletsChanged += OnBulletsChanged;
    }

    private void OnBulletsChanged(int currentBulletsInClip, int bulletsLeft)
    {
        _text.text = $"{currentBulletsInClip}/{bulletsLeft}";
    }
}
