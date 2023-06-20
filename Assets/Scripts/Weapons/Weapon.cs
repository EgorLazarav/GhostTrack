using UnityEngine;

public abstract class Weapon : ObjectPool<Bullet>
{
    [SerializeField] private Bullet _bullet;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private float _shotPower;

    protected Transform ShootPoint => _shootPoint;
    protected float ShotPower => _shotPower;

    private void Awake()
    {
        Init(_bullet);
    }

    public abstract void TryShoot();
}
