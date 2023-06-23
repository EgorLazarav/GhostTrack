using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleWeapon : Weapon
{
    public override void TryShoot()
    {
        if (InternalReloadingCoroutine != null)
            return;

        if (CurrentBulletsCount <= 0)
            return;

        CurrentBulletsCount--;
        Shoot(ShootPoint.rotation);

        InternalReloadingCoroutine = StartCoroutine(InternalReloading());
    }
}
