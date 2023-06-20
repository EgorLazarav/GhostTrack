using System.Collections;
using UnityEngine;

public class Weapon : ObjectPool<Bullet>
{
    [SerializeField] private Bullet _bullet;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private float _shotPower;
    [SerializeField] private float _timeBetweenShots;
    [SerializeField] private int _bulletsLeft;
    [SerializeField] private int _maxBulletsInClip;
    [SerializeField] private float _baseReloadTime = 1;

    private Coroutine _internalReloadingCoroutine;
    private Coroutine _reloadingCoroutine;
    private WaitForSeconds _shotsDelay;

    private int _currentBulletsInClip;

    protected virtual void Awake()
    {
        _shotsDelay = new WaitForSeconds(_timeBetweenShots);

        _currentBulletsInClip = _bulletsLeft < _maxBulletsInClip? _bulletsLeft : _maxBulletsInClip;
        _bulletsLeft -= _currentBulletsInClip;

        Init(_bullet);
    }

    private IEnumerator InternalReloading()
    {
        yield return _shotsDelay;
        _internalReloadingCoroutine = null;
    }

    private IEnumerator Reloading(float reloadTimeReduceCoef)
    {
        yield return new WaitForSeconds(_baseReloadTime / reloadTimeReduceCoef);

        _currentBulletsInClip = _bulletsLeft < _maxBulletsInClip ? _bulletsLeft : _maxBulletsInClip;
        _bulletsLeft -= _currentBulletsInClip;
        print(_currentBulletsInClip);

        _reloadingCoroutine = null;
    }

    public virtual void TryShoot()
    {
        if (_currentBulletsInClip <= 0)
            return;

        if (_reloadingCoroutine != null)
            return;

        if (_internalReloadingCoroutine != null)
            return;

        _internalReloadingCoroutine = StartCoroutine(InternalReloading());
        _currentBulletsInClip--;
        print(_currentBulletsInClip);

        var bullet = GetItem();
        bullet.Init(_shootPoint.transform.position, _shootPoint.transform.rotation, _shotPower);
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
}
