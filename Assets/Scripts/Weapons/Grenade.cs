using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : Bullet
{
    [SerializeField] private int _durability = 3;
    [SerializeField] private float _explosionTimer = 3;

    private float _velocityReduceCoeff = 1.5f;

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody.velocity /= _velocityReduceCoeff;
        _durability--;

        if (_durability <= 0)
            gameObject.SetActive(false);
    }

    public override void Init(Vector3 position, Quaternion rotation, float shotPower)
    {
        base.Init(position, rotation, shotPower);
        StartCoroutine(Explosioning());
    }

    private IEnumerator Explosioning()
    {
        yield return new WaitForSeconds(_explosionTimer);
        gameObject.SetActive(false);
    }
}
