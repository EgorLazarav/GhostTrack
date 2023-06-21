using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Weapon : ObjectPool<Bullet>
{
    [SerializeField] private WeaponData _data;
    [SerializeField] private Transform _shootPoint;

    private Coroutine _internalReloadingCoroutine;
    private Coroutine _reloadingCoroutine;
    private WaitForSeconds _shotsDelay;

    private int _currentBulletsInClip;
    private int _bulletsLeft;

    public event UnityAction<int, int> BulletsChanged;

    protected virtual void Awake()
    {
        _shotsDelay = new WaitForSeconds(_data.TimeBetweenShots);

        _bulletsLeft = _data.BulletsLeft;
        _currentBulletsInClip = _bulletsLeft < _data.MaxBulletsInClip? _bulletsLeft : _data.MaxBulletsInClip;
        _bulletsLeft -= _currentBulletsInClip;

        Init(_data.Bullet);
    }

    private IEnumerator InternalReloading()
    {
        yield return _shotsDelay;
        _internalReloadingCoroutine = null;
    }

    private IEnumerator Reloading(float reloadTimeReduceCoef)
    {
        yield return new WaitForSeconds(_data.BaseReloadTime / reloadTimeReduceCoef);

        _currentBulletsInClip = _bulletsLeft < _data.MaxBulletsInClip ? _bulletsLeft : _data.MaxBulletsInClip;
        _bulletsLeft -= _currentBulletsInClip;
        yield return new WaitForEndOfFrame();
        BulletsChanged?.Invoke(_currentBulletsInClip, _bulletsLeft);

        _reloadingCoroutine = null;
    }

    public virtual void TryShoot()
    {
        if (_currentBulletsInClip <= 0)
            return;

        if (_internalReloadingCoroutine != null)
            return;

        if (_reloadingCoroutine != null)
            return;

        _internalReloadingCoroutine = StartCoroutine(InternalReloading());
        _currentBulletsInClip--;
        BulletsChanged?.Invoke(_currentBulletsInClip, _bulletsLeft);

        var bullet = GetItem();
        bullet.Init(_shootPoint.transform.position, _shootPoint.transform.rotation, _data.ShotPower);
    }

    public virtual void TryReload(float reloadTimeReduceCoef)
    {
        if (_reloadingCoroutine != null)
            return;

        _reloadingCoroutine = StartCoroutine(Reloading(reloadTimeReduceCoef));
    }

    public virtual void TryStopReloading()
    {
        if (_reloadingCoroutine == null)
            return;

        StopCoroutine(_reloadingCoroutine);
        _reloadingCoroutine = null;
    }

    public void Setup(Transform newParent)
    {
        transform.parent = newParent;
        transform.rotation = newParent.rotation;
        transform.position = newParent.position;
        BulletsChanged?.Invoke(_currentBulletsInClip, _bulletsLeft);
    }

    public void Drop()
    {
        TryStopReloading();
        transform.parent = null;
    }
}
