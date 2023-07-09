using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create New Weapon Data", order = 54, fileName = "New Weapon Data")]
public class WeaponData : ScriptableObject
{
    [Header("Bullets")]
    [SerializeField] private Bullet _bullet;
    [SerializeField][Min(1)] private int _bulletsCount = 30;

    [Header("Shot")]
    [SerializeField][Range(0, 100)] private int _damagePercent = 100;
    [SerializeField][Min(0.1f)] private float _shotPower = 10;
    [SerializeField][Min(0.01f)] private float _timeBetweenShots = 1;
    [SerializeField] private AudioClip _shotSFX;
    [SerializeField] private AudioClip _emptyClipSFX;
    [SerializeField] private AudioClip _pickUpSFX;
    [SerializeField] private bool _isShotSilenced = false;

    public Bullet Bullet => _bullet;
    public int BulletsCount => _bulletsCount;

    public int DamagePercent => _damagePercent;
    public float ShotPower => _shotPower;
    public float TimeBetweenShots => _timeBetweenShots;
    public AudioClip ShotSFX => _shotSFX;
    public AudioClip EmptyClipSFX => _emptyClipSFX;
    public AudioClip PickUpSFX => _pickUpSFX;
    public bool IsShotSilenced => _isShotSilenced;  
}
