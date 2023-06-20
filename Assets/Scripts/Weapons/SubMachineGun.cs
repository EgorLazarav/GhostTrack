using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubMachineGun : Weapon
{
    private Coroutine _internalReloadingCoroutine;

    public override void TryShoot()
    {
        if (_internalReloadingCoroutine != null)
            return;

        _internalReloadingCoroutine = StartCoroutine(InternalReloading());

        var bullet = GetItem();
        bullet.Init(ShootPoint.transform.position, ShootPoint.transform.rotation, ShotPower);
    }

    private IEnumerator InternalReloading()
    {
        yield return new WaitForSeconds(0.1f);
        _internalReloadingCoroutine = null;
    }
}
