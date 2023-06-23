using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunWeapon : Weapon
{
    [SerializeField] private float _angleSpread = 5;

    public override bool TryShoot()
    {
        if (base.TryShoot())
        {
            for (int i = -1; i <= 1; i++)
            {
                Quaternion rotation = Quaternion.Euler(ShootPoint.eulerAngles + new Vector3(0, 0, _angleSpread * i));
                Shoot(rotation, Data.ShotPower, Data.DamagePercent / 3);
            }

            return true;
        }

       return false;
    }
}
