using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    [Header("Combat")]
    [SerializeField] private PlayerCombat _combat;
    [SerializeField] private Weapon _startWeapon;
    [SerializeField] private Transform _weaponPoint;
    [SerializeField] private Transform _punchPoint;
    [SerializeField] private Health _health;
    [SerializeField] private float _handsLength = 0.5f;

    [Header("Movement")]
    [SerializeField] private PlayerMovement _movement;
    [SerializeField] private float _speed = 5;

    public Weapon StartWeapon => _startWeapon;

    public static event UnityAction Died;

    public void Init()
    {
        _combat.Init(_startWeapon, _weaponPoint, _handsLength, _punchPoint);
        _movement.Init(_speed);
    }

    private void OnEnable()
    {
        _health.Over += OnHealthOver;
    }

    private void OnDisable()
    {
        _health.Over -= OnHealthOver;
    }

    private void OnHealthOver()
    {
        Died?.Invoke();
        gameObject.SetActive(false);
    }
}