using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class BulletsDisplay : MonoBehaviour
{
    private Text _text;

    public void Init(int startWeaponBulletsCount)
    {
        _text = GetComponent<Text>();

        SetText(startWeaponBulletsCount);

        PlayerCombat.BulletsChanged += OnPlayerBulletsChanged;
    }

    private void SetText(int bulletsCount)
    {
        if (bulletsCount == 0)
            _text.text = "NO AMMO";
        else
            _text.text = "||| " + bulletsCount;
    }

    private void OnDestroy()
    {
        PlayerCombat.BulletsChanged -= OnPlayerBulletsChanged;
    }

    private void OnPlayerBulletsChanged(int amount)
    {
        if (amount == 0)
            _text.text = "NO AMMO";
        else
            _text.text = "||| " + amount;
    }
}
