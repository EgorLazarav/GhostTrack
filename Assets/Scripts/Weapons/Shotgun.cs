using UnityEngine;

public class Shotgun : Weapon
{
    [SerializeField] private float _angleSpread = 15;

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
}
