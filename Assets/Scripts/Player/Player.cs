using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private Weapon _startWeapon;
    [SerializeField] private PlayerCombat _playerCombat;

    public Weapon StartWeapon => _startWeapon;

    public static event UnityAction Died;

    public void Init()
    {
        _playerCombat.Init(_startWeapon);
    }

    private void OnDisable()
    {
        Died?.Invoke();
    }
}