using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyBullet : Bullet
{
    [SerializeField] private int _penetrationDurability = 3;
    [SerializeField] private float _damageReduceCoeff = 2;

    private float _currentDamage;
    private int _currentPenetrationDurability;

    public override void Init(Vector3 position, Quaternion rotation, float shotPower, float damage = 1)
    {
        base.Init(position, rotation, shotPower);
        _currentPenetrationDurability = _penetrationDurability;
        _currentDamage = BaseDamage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Health health))
            health.ApplyDamage(_currentDamage);

        _currentPenetrationDurability--;
        _currentDamage /= _damageReduceCoeff;

        if (_currentPenetrationDurability <= 0)
            gameObject.SetActive(false);
    }
}
