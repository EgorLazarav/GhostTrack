using System.Collections;
using System.Collections.Generic;
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
    }

    private void TryShoot()
    {
        if (_weapon == null)
            return;

        _weapon.TryShoot();
    }
}
