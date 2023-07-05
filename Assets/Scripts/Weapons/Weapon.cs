using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : ObjectPool<Bullet>
{
    [SerializeField] private WeaponData _data;
    [SerializeField] private Transform _shootPoint;

    private Coroutine _internalReloadingCoroutine = null;
    private WaitForSeconds _internalReloadingDelay;
    private int _currentBulletsCount;

    public int CurrentBulletsCount => _currentBulletsCount;
    public WeaponData Data => _data;
    public Transform ShootPoint => _shootPoint;

    private void Start()
    {
        InitPool(_data.Bullet);

        _internalReloadingDelay = new WaitForSeconds(_data.TimeBetweenShots);
        _currentBulletsCount = _data.BulletsCount;
    }

    private IEnumerator InternalReloading()
    {
        yield return _internalReloadingDelay;
        _internalReloadingCoroutine = null;
    }

    public virtual bool TryShoot()
    {
        if (_internalReloadingCoroutine != null)
            return false;

        if (_currentBulletsCount <= 0)
            return false;

        _currentBulletsCount--;
        _internalReloadingCoroutine = StartCoroutine(InternalReloading());

        return true;
    }

    protected virtual void Shoot(Quaternion rotation, float shotPower, int damagePercent)
    {
        var bullet = GetItem();
        bullet.Init(_shootPoint.position, rotation, shotPower, damagePercent);
    }

    public virtual void PickUp(Transform newParent)
    {
        transform.parent = newParent;
        transform.rotation = newParent.rotation;
        transform.position = newParent.position;
        transform.localPosition += new Vector3(0.2f, -0f); // костыль;
    }

    public virtual void Throw()
    {
        transform.parent = null;
    }
}
