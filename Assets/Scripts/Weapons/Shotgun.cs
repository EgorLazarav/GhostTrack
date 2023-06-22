using System.Collections;
using UnityEngine;

public class Shotgun : Weapon
{
    [SerializeField] private float _angleSpread = 15;
    [SerializeField] private float _oneBulletReloadSpeed = 2;

    public override void TryShoot()
    {
        TryStopReloading();
        OnBulletsChanged();
        base.TryShoot();
    }

    protected override void CreateBullet(Quaternion rotation, float shotPower, float bulletDamage = 1)
    {
        for (int i = -1; i <= 1; i++)
        {
            base.CreateBullet(Quaternion.Euler(0, 0, ShootPoint.transform.eulerAngles.z + _angleSpread * i), shotPower, 0.35f);
        }
    }

    protected override IEnumerator Reloading(float reloadTimeReduceCoef)
    {
        ReloadingCoroutine = null;

        while (BulletsLeft > 0 && CurrentBulletsInClip < Data.MaxBulletsInClip)
        {
            yield return new WaitForSeconds(_oneBulletReloadSpeed / reloadTimeReduceCoef);
            CurrentBulletsInClip++;
            BulletsLeft--;
            yield return new WaitForEndOfFrame();
            OnBulletsChanged();
        }
    }
}
