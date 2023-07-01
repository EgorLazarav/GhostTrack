using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private Weapon _startWeapon;

    public Weapon StartWeapon => _startWeapon;

    public static event UnityAction Died;

    public void Init()
    {

    }

    private void OnDisable()
    {
        Died?.Invoke();
    }
}