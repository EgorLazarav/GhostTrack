using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : Bullet
{
    [SerializeField] private int _durability = 3;
    [SerializeField] private float _explosionTimer = 3;
    [SerializeField] private float _explosionRange = 2;

    private float _velocityReduceCoeff = 1.2f;
    private int _currentDurability;
    private Coroutine _launchingCoroutine;

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out Health health))
            Explode();

        Rigidbody.velocity /= _velocityReduceCoeff;
        _currentDurability--;

        if (_currentDurability <= 0)
            Explode();
    }

    public override void Init(Vector3 position, Quaternion rotation, float shotPower)
    {
        base.Init(position, rotation, shotPower);

        if (_launchingCoroutine != null)
            StopCoroutine(_launchingCoroutine);

        _launchingCoroutine = StartCoroutine(Launching());
        _currentDurability = _durability;
    }

    private IEnumerator Launching()
    {
        float timer = _explosionTimer;

        while (timer > 0)
        {
            timer -= Time.deltaTime;
            Rigidbody.velocity /= (1 + Time.deltaTime * _velocityReduceCoeff);
            yield return null;
        }

        Explode();
        _launchingCoroutine = null;
    }

    private void Explode()
    {
        var objects = Physics2D.OverlapCircleAll(transform.position, _explosionRange);

        foreach (var obj in objects)
        {
            if (obj.TryGetComponent(out Health health))
            {
                health.ApplyDamage();
            }
        }

        gameObject.SetActive(false);
    }
}
