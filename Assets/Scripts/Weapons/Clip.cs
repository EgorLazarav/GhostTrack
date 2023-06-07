using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Clip : MonoBehaviour
{
    private Bullet _bullet;
    private List<Bullet> _bullets = new List<Bullet>();

    public int BulletsLeft { get; private set; }

    public void Init(Bullet bullet, int maxBullets)
    {
        _bullet = bullet;
        BulletsLeft = maxBullets;
    }

    public bool TryGetBullet(out Bullet bullet)
    {
        if (BulletsLeft <= 0)
        {
            bullet = null;
            return false;
        }

        bullet = _bullets.FirstOrDefault(b => b.gameObject.activeSelf == false);

        if (bullet == null)
        {
            bullet = Instantiate(_bullet);
            _bullets.Add(bullet);
        }

        bullet.gameObject.SetActive(true);
        BulletsLeft--;

        return true;
    }
}
