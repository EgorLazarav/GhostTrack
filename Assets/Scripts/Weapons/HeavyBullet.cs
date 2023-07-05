using UnityEngine;

public class HeavyBullet : Bullet
{
    [SerializeField] private float _damageReducePerHit = 2;
    [SerializeField] private int _maxHits = 2;

    private float _currentHits = 0;

    public override void Init(Vector3 position, Quaternion rotation, float shotPower, int damagePercent)
    {
        base.Init(position, rotation, shotPower, damagePercent);

        _currentHits = 0;
    }

    protected override void DealDamage(Health health)
    {
        _currentHits++;

        if (_currentHits >= _maxHits)
            base.DealDamage(health);
        else
            health.ApplyDamage(DamagePercent);

        DamagePercent /= _damageReducePerHit;
    }
}