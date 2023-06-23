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

        var bullet = GetItem();
        bullet.Init(ShootPoint.position, ShootPoint.rotation, Data.ShotPower, Data.DamagePercent);
        CurrentBulletsCount--;

        InternalReloadingCoroutine = StartCoroutine(InternalReloading());
    }

    private IEnumerator InternalReloading()
    {
        yield return InternalReloadingDelay;
        InternalReloadingCoroutine = null;
    }
}
