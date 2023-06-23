using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunWeapon : Weapon
{
    [SerializeField] private float _angleSpread = 5;

    public override void TryShoot()
    {
        if (InternalReloadingCoroutine != null)
            return;

        if (CurrentBulletsCount <= 0)
            return;

        CurrentBulletsCount--;

        for (int i = -1; i <= 1; i++)
        {
            Quaternion rotation = Quaternion.Euler(ShootPoint.eulerAngles + new Vector3(0, 0, _angleSpread * i));
            Shoot(rotation);
        }

        InternalReloadingCoroutine = StartCoroutine(InternalReloading());
    }
}
