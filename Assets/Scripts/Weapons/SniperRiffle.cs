using UnityEngine;

public class SniperRiffle : Weapon
{
    [SerializeField] private float _bonusZoomOutRange = 2;

    public override void PickUp(Transform newParent)
    {
        base.PickUp(newParent);
        Camera.main.orthographicSize += _bonusZoomOutRange;
    }

    public override void Drop()
    {
        base.Drop();
        Camera.main.orthographicSize -= _bonusZoomOutRange;
    }
}