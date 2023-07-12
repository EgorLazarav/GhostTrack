using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CheatCodeActivator : MonoBehaviour
{
    [SerializeField] private Weapon[] _weapons;
    [SerializeField] private PlayerCombat _playerCombat;

    public static bool IsInvulnerable { get; private set; } = false;
    public static bool IsInfinityBullets { get; private set; } = false;
    public static bool IsPlayerInvisible { get; private set; } = false;

    private const string GOD = nameof(GOD); // player is invulnerable
    private const string INF = nameof(INF); // infinity ammo
    private const string INV = nameof(INV); // invisible (enemies ignores player)
    private const string IMD = nameof(IMD); // invaders must die (kill all enemies)

    private const string EW = nameof(EW); // player get's 1: Uzi, 2 : AK47, 3 : M249, 4 : Grenadelauncher, 5: AWM, 6: Nova

    private string _letters = "";

    private void Update()
    {
        CheckInput();

        if (_letters.Length >= 3)
        {
            if (_letters.Substring(_letters.Length - 3, 3) == GOD)
            {
                _letters = "";
                IsInvulnerable = !IsInvulnerable;
                string state = IsInvulnerable ? "" : "DE";
                print($"GOD CHEAT {state}ACTIVATED");
            }
            else if (_letters.Substring(_letters.Length - 3, 3) == INF)
            {
                _letters = "";
                IsInfinityBullets = !IsInfinityBullets;
                string state = IsInfinityBullets ? "" : "DE";
                print($"INFINITY AMMO CHEAT {state}ACTIVATED");
            }
            else if (_letters.Substring(_letters.Length - 3, 3) == INV)
            {
                _letters = "";
                IsPlayerInvisible = !IsPlayerInvisible;
                string state = IsPlayerInvisible ? "" : "DE";
                print($"INVISIBLE CHEAT {state}ACTIVATED");
            }

            /*
            else if (_letters.Substring(_letters.Length - 3, 2) == "EW")
            {
                for (int i = 1; i <= _weapons.Length; i++)
                {
                    if (_letters[_letters.Length - 1] == Convert.ToChar(i))
                    {
                        print("+");

                        var weapon = Instantiate(_weapons[i - 1]);
                        _playerCombat.EquipWeapon(weapon);
                        _letters = "";
                        print(weapon.name + " CREATED");
                    }
                }
            }
            */
        }
    }

    private void CheckInput()
    {
        if (Input.anyKeyDown)
        {
            foreach (KeyCode key in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(key))
                {
                    _letters += key.ToString();
                }
            }
        }
    }
}
