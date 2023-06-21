using UnityEngine;

public class SniperRiffle : Weapon
{
    public override void OnPickUp(Transform newParent)
    {
        base.OnPickUp(newParent);
        Camera.main.orthographicSize *= 2;
    }

    public override void OnDrop()
    {
        base.OnDrop();
        Camera.main.orthographicSize /= 2;
    }
}