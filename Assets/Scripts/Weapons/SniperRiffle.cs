using UnityEngine;

public class SniperRiffle : Weapon
{
    [SerializeField] private float _bonusZoomOutRange = 2;

    public override void PickUp(Transform newParent)
    {
        base.PickUp(newParent);

        if (IsPlayerOwner())
            Camera.main.orthographicSize += _bonusZoomOutRange;
    }

    public override void Drop()
    {
        if (IsPlayerOwner())
            Camera.main.orthographicSize -= _bonusZoomOutRange;

        base.Drop();
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
