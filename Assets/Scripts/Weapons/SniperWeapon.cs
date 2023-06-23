using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperWeapon : SimpleWeapon
{
    [SerializeField] private float _bonusVisionRange = 2;
    [SerializeField] private int _bonusDamage = 2;

    protected override void Shoot(Quaternion rotation, float shotPower, int damagePercent)
    {
        damagePercent *= _bonusDamage;
        base.Shoot(rotation, shotPower, damagePercent);
    }

    public override void PickUp(Transform newParent)
    {
        base.PickUp(newParent);

        if (IsPlayerOwner())
            Camera.main.orthographicSize += _bonusVisionRange;
    }

    public override void Throw()
    {
        if (IsPlayerOwner())
            Camera.main.orthographicSize -= _bonusVisionRange;

        base.Throw();
    }

    private bool IsPlayerOwner()
    {
        Transform currentParent = transform.parent;

        while (currentParent != null)
        {
            if (currentParent.TryGetComponent(out Player player))
                return true;

            currentParent = currentParent.parent;
        }

        return false;
    }
}
