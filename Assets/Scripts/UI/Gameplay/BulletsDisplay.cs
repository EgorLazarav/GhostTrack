using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class BulletsDisplay : MonoBehaviour
{
    private Text _text;

    public void Init()
    {
        _text = GetComponent<Text>();
        _text.text = "NO AMMO";
    }

    private void OnEnable()
    {
        PlayerCombat.BulletsChanged += OnPlayerBulletsChanged;
    }

    private void OnDisable()
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
