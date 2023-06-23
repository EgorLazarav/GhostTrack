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

    public abstract void TryShoot();
}
