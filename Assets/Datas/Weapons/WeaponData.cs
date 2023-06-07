using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create New Weapon Data", fileName = "New Weapon Data", order = 54)]
public class WeaponData : ScriptableObject
{
    [SerializeField] private float _fireRate;
    [SerializeField] private float _fireRange;
    [SerializeField] private float _shotPower;
    [SerializeField] private int _maxBullets;
    [SerializeField] private AudioClip _shootSFX;
    [SerializeField] private Sprite _sprite;
    [SerializeField] private Clip _clip;
    [SerializeField] private Bullet _bullet;
    [SerializeField][Range(1, 4)] private int _bulletDamageReduceCoeff = 1;

    public float FireRate => _fireRate;
    public float FireRange => _fireRange;
    public float ShotPower => _shotPower;
    public int MaxBullets => _maxBullets;
    public AudioClip ShootSFX => _shootSFX;
    public Sprite Sprite => _sprite;
    public Clip Clip => _clip;
    public Bullet Bullet => _bullet;
    public int BulletDamageReduceCoeff => _bulletDamageReduceCoeff;
}
