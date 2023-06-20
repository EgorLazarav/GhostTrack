using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create New Weapon Data", order = 54, fileName = "New Weapon Data")]
public class WeaponData : ScriptableObject
{
    [Header("Bullets")]
    [SerializeField] private Bullet _bullet;
    [SerializeField] private int _maxBulletsInClip;
    [SerializeField] private int _maxClips;

    [Header("Shot")]
    [SerializeField] private float _baseReloadTime = 1;
    [SerializeField] private float _shotPower;
    [SerializeField] private float _timeBetweenShots;

    public Bullet Bullet => _bullet;
    public int MaxBulletsInClip => _maxBulletsInClip;
    public int BulletsLeft => _maxClips * _maxBulletsInClip;

    public float BaseReloadTime => _baseReloadTime;
    public float ShotPower => _shotPower;
    public float TimeBetweenShots => _timeBetweenShots;

    private void OnValidate()
    {
        if (_maxBulletsInClip < 0)
            _maxBulletsInClip = 0;

        if (_maxClips < 0)
            _maxClips = 0;

        if (_baseReloadTime < 0)
            _baseReloadTime = 0;

        if (_shotPower < 0)
            _shotPower = 0;

        if (_timeBetweenShots < 0)
            _timeBetweenShots = 0;
    }
}
