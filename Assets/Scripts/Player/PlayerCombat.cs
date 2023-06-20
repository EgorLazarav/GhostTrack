using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private Weapon _weapon;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            TryShoot();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            TryEquipWeapon();
        }
    }

    private void TryEquipWeapon()
    {
        var objects = Physics2D.OverlapCircleAll(transform.position, 1);
        var closestWeapon = objects.FirstOrDefault(obj => obj.transform.parent == null && obj.TryGetComponent(out Weapon weapon));

        if (closestWeapon == null)
            return;

        if (_weapon != null)
        {

        }
    }

    private void TryShoot()
    {
        if (_weapon == null)
            return;

        _weapon.TryShoot();
    }
}
