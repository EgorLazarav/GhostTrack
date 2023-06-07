using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private WeaponData _data;
    [SerializeField] private Transform _shootPoint;
    
    private BoxCollider2D _collider;
    private Coroutine _internalReloadingCoroutine;
    private WaitForSeconds _fireRateDelay;

    public Transform ShootPoint => _shootPoint;
    public WeaponData Data => _data;
    public Clip Clip { get; private set; }

    private const int SecondsInMinute = 60;

    private void Awake()
    {
        Clip = Instantiate(Data.Clip, transform);
        Clip.Init(Data.Bullet, Data.MaxBullets);

        _collider = gameObject.AddComponent<BoxCollider2D>();
        _collider.isTrigger = false;

        var spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = Data.Sprite;
        spriteRenderer.sortingOrder = 5;

        _collider.offset = new Vector2(0, 0);
        _collider.size = new Vector3(spriteRenderer.bounds.size.x / transform.lossyScale.x,
        spriteRenderer.bounds.size.y / transform.lossyScale.y,
        spriteRenderer.bounds.size.z / transform.lossyScale.z) + new Vector3(0.1f, 0.1f, 0.1f);

        _fireRateDelay = new WaitForSeconds(SecondsInMinute / Data.FireRate);
        gameObject.name = Data.name;
    }

    public bool TryShoot(Vector2 lookDirection)
    {
        if (_internalReloadingCoroutine != null || Clip.TryGetBullet(out Bullet bullet) == false)
            return false;

        float bulletRotationZ = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        bullet.Init(lookDirection, _shootPoint.position, bulletRotationZ, Data.FireRange, Data.ShotPower, Data.BulletDamageReduceCoeff);

        _internalReloadingCoroutine = StartCoroutine(InternalReloading());
        AudioSource.PlayClipAtPoint(Data.ShootSFX, _shootPoint.position);

        return true;
    }

    private IEnumerator InternalReloading()
    {
        yield return _fireRateDelay;
        _internalReloadingCoroutine = null;
    }

    public void Drop()
    {
        transform.parent = null;
        _collider.isTrigger = true;
        transform.eulerAngles = Vector3.zero;
    }

    public void OnPickUp(Transform weaponPoint)
    {
        _collider.isTrigger = false;
        transform.parent = weaponPoint;
        transform.position = weaponPoint.position;
        transform.eulerAngles = weaponPoint.eulerAngles;
    }
}
