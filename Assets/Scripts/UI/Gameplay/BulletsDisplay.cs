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

        if (startWeaponBulletsCount == 0)
            _text.text = "NO AMMO";
        else
            _text.text = "||| " + startWeaponBulletsCount;

        PlayerCombat.BulletsChanged += OnPlayerBulletsChanged;
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
