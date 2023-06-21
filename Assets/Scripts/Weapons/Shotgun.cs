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

    protected override void CreateBullet()
    {
        base.CreateBullet();
        print("pah");

        for (int i = -1; i <= 1; i += 2)
        {
            var bullet = GetItem();
            bullet.Init(ShootPoint.transform.position, Quaternion.Euler(0, 0, ShootPoint.transform.eulerAngles.z + _angleSpread * i), Data.ShotPower);
        }
    }

    protected override IEnumerator Reloading(float reloadTimeReduceCoef)
    {
        ReloadingCoroutine = null;
        reloadTimeReduceCoef *= _oneBulletReloadSpeed;

        while (BulletsLeft > 0 && CurrentBulletsInClip < Data.MaxBulletsInClip)
        {
            CurrentBulletsInClip++;
            BulletsLeft--;
            yield return new WaitForEndOfFrame();
            OnBulletsChanged();
            yield return new WaitForSeconds(Data.BaseReloadTime / reloadTimeReduceCoef);
        }
    }
}
