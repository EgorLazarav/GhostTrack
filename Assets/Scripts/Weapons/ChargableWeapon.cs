using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargableWeapon : Weapon
{
    [SerializeField] private float _maxChargeTimer = 2;

    private Coroutine _chargingCoroutine;
    private Coroutine _reloadingCoroutine;

    public override bool TryShoot()
    {
        if (_reloadingCoroutine != null)
            return false;

        if (_chargingCoroutine != null)
            return false;

        if (base.TryShoot())
        {
            _chargingCoroutine = StartCoroutine(Charging(ShootPoint.rotation));
            return true;
        }

        return false;
    }

    private IEnumerator Charging(Quaternion rotation)
    {
        float timer = 1;

        while (timer < _maxChargeTimer + 1)
        {
            timer += Time.deltaTime;
            yield return null;

            if (Input.GetKeyUp(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Q))
                break;
        }


        Shoot(rotation, Data.ShotPower * timer, Data.DamagePercent);

        _reloadingCoroutine = StartCoroutine(Reloading(timer));
        _chargingCoroutine = null;
    }

    private IEnumerator Reloading(float time)
    {
        yield return new WaitForSeconds(time);
        _reloadingCoroutine = null;
    }
}
