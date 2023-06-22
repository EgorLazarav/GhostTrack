using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Weapon : ObjectPool<Bullet>
{
    [SerializeField] private WeaponData _data;
    [SerializeField] private Transform _shootPoint;

    private Coroutine _internalReloadingCoroutine;
    private WaitForSeconds _shotsDelay;

    protected Coroutine ReloadingCoroutine;
    protected int CurrentBulletsInClip;
    protected int BulletsLeft;

    protected WeaponData Data => _data;
    protected Transform ShootPoint => _shootPoint;

    public event UnityAction<int, int> BulletsChanged;

    protected virtual void Awake()
    {
        _shotsDelay = new WaitForSeconds(_data.TimeBetweenShots);

        BulletsLeft = _data.BulletsLeft;
        CurrentBulletsInClip = BulletsLeft < _data.MaxBulletsInClip? BulletsLeft : _data.MaxBulletsInClip;
        BulletsLeft -= CurrentBulletsInClip;

        Init(_data.Bullet);
    }

    private IEnumerator InternalReloading()
    {
        yield return _shotsDelay;
        _internalReloadingCoroutine = null;
    }

    protected virtual IEnumerator Reloading(float reloadTimeReduceCoef)
    {
        yield return new WaitForSeconds(_data.BaseReloadTime / reloadTimeReduceCoef);

        CurrentBulletsInClip = BulletsLeft < _data.MaxBulletsInClip ? BulletsLeft : _data.MaxBulletsInClip;
        BulletsLeft -= CurrentBulletsInClip;

        yield return new WaitForEndOfFrame();
        OnBulletsChanged();
        ReloadingCoroutine = null;
    }

    protected void OnBulletsChanged()
    {
        BulletsChanged?.Invoke(CurrentBulletsInClip, BulletsLeft);
    }

    public virtual void TryShoot()
    {
        if (CurrentBulletsInClip <= 0)
            return;

        if (_internalReloadingCoroutine != null)
            return;

        if (ReloadingCoroutine != null)
            return;

        CreateBullet(_shootPoint.transform.rotation, _data.ShotPower);
        CurrentBulletsInClip--;
        OnBulletsChanged();
        _internalReloadingCoroutine = StartCoroutine(InternalReloading());
    }

    protected virtual void CreateBullet(Quaternion rotation, float shotPower, float bulletDamage = 1)
    {
        var bullet = GetItem();
        bullet.Init(_shootPoint.transform.position, rotation, shotPower, bulletDamage);
    }

    public virtual void TryReload(float reloadTimeReduceCoef)
    {
        if (ReloadingCoroutine != null)
            return;

        ReloadingCoroutine = StartCoroutine(Reloading(reloadTimeReduceCoef));
    }

    public virtual void TryStopReloading()
    {
        if (ReloadingCoroutine == null)
            return;

        StopCoroutine(ReloadingCoroutine);
        ReloadingCoroutine = null;
    }

    public virtual void PickUp(Transform newParent)
    {
        transform.parent = newParent;
        transform.rotation = newParent.rotation;
        transform.position = newParent.position;
        OnBulletsChanged();
    }

    public virtual void Drop()
    {
        TryStopReloading();
        transform.parent = null;
    }
}
