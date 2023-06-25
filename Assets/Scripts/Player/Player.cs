using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Weapon _startWeapon;

    public Weapon StartWeapon => _startWeapon;

    public void Init()
    {

    }
}