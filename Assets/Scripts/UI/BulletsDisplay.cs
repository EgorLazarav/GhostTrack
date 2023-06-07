using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletsDisplay : MonoBehaviour
{
    [SerializeField] private Text _bulletsDisplay;
    [SerializeField] private PlayerCombat _playerCombat;

    private void OnEnable()
    {
        _playerCombat.BulletsChanged += OnBulletsChanged;
    }

    private void OnDisable()
    {
        _playerCombat.BulletsChanged -= OnBulletsChanged;
    }

    private void OnBulletsChanged(int newAmount)
    {
        if (newAmount > 0)
            _bulletsDisplay.text = newAmount.ToString() + "rnds";
        else
            _bulletsDisplay.text = "no ammo";
    }
}
