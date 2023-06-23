using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleWeapon : Weapon
{
    public override bool TryShoot()
    {
        if (base.TryShoot())
        {
            Shoot(ShootPoint.rotation, Data.ShotPower, Data.DamagePercent);
            return true;
        }

        return false;
    }
}
