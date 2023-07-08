using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : Bullet
{
    [SerializeField] private int _durability = 3;
    [SerializeField] private float _explosionTimer = 3;
    [SerializeField] private float _explosionRange = 1.2f;
    [SerializeField] private ParticleSystem _explosion;

    private float _velocityReduceCoeff = 1.2f;
    private int _currentDurability;
    private Coroutine _launchingCoroutine;

    protected override void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        Collider = GetComponent<Collider2D>();

        Collider.isTrigger = false;
        Rigidbody.isKinematic = false;
        Rigidbody.gravityScale = 0;
    }

    public override void Init(Vector3 position, Quaternion rotation, float shotPower, int damagePercent)
    {
        base.Init(position, rotation, shotPower, damagePercent);

        if (_launchingCoroutine != null)
            StopCoroutine(_launchingCoroutine);

        _launchingCoroutine = StartCoroutine(Launching());
        _currentDurability = _durability;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out Health health))
            Explode();

        Rigidbody.velocity /= _velocityReduceCoeff;
        _currentDurability--;
        AudioManager.Instance.PlayGrenadeBounceSFX();

        if (_currentDurability <= 0)
            Explode();
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
                health.ApplyDamage(DamagePercent); // от расстояния цели от центра взрыва урон менять
            }
        }

        var explosion = Instantiate(_explosion, transform.position, Quaternion.identity);
        Destroy(explosion, explosion.main.duration);
        AudioManager.Instance.PlayExplosionSFX();

        gameObject.SetActive(false);
    }
}
