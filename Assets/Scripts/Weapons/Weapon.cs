using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : ObjectPool<Bullet>
{
    [SerializeField] protected WeaponData Data;
    [SerializeField] protected Transform ShootPoint;

    protected Coroutine InternalReloadingCoroutine = null;
    protected WaitForSeconds InternalReloadingDelay;

    protected int CurrentBulletsCount;

    private void Start()
    {
        InitPool(Data.Bullet);

        InternalReloadingDelay = new WaitForSeconds(Data.TimeBetweenShots);
        CurrentBulletsCount = Data.BulletsCount;
    }

    protected IEnumerator InternalReloading()
    {
        yield return InternalReloadingDelay;
        InternalReloadingCoroutine = null;
    }

    protected virtual void Shoot(Quaternion rotation)
    {
        var bullet = GetItem();
        bullet.Init(ShootPoint.position, rotation, Data.ShotPower, Data.DamagePercent);
    }

    public abstract void TryShoot();
}
