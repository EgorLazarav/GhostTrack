using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CheatCodeActivator : MonoBehaviour
{
    [SerializeField] private Weapon[] _weapons;
    [SerializeField] private PlayerCombat _playerCombat;

    private static bool _isPlayerInvulnerable = false;
    private static bool _isPlayerHasInfinityBullets = false;
    private static bool _isPlayerInvisible = false;

    public static bool IsPlayerInvulnerable => _isPlayerInvulnerable;
    public static bool IsPlayerHasInfinityBullets => _isPlayerHasInfinityBullets;
    public static bool IsPlayerInvisible => _isPlayerInvisible;

    private const string GOD = nameof(GOD); // player is invulnerable
    private const string INF = nameof(INF); // infinity ammo
    private const string INV = nameof(INV); // invisible (enemies ignores player)
    private const string IMD = nameof(IMD); // invaders must die (kill all enemies)

    private const string EW = nameof(EW); // player get's 1: Uzi, 2 : AK47, 3 : M249, 4 : Grenadelauncher, 5: AWM, 6: Nova

    private string _letters = "";

    private void Update()
    {
        CheckInput();
        CheckActivatedCheatCodes();
    }

    private void CheckActivatedWeaponCheatCode()
    {
        if (GetStringEnding(_letters, 3).StartsWith(EW))
        {
            for (int i = 0; i < _weapons.Length; i++)
            {
                if ((i + 1).ToString() == GetStringEnding(_letters, 1))
                {
                    var weapon = Instantiate(_weapons[i]);
                    _playerCombat.EquipWeapon(weapon);
                    _letters = "";
                }
            }
        }
    }

    private void CheckActivatedCheatCodes()
    {
        if (_letters.Length < 3)
            return;

        switch (GetStringEnding(_letters, 3))
        {
            case GOD:
                ActivateCheatCode(ref _isPlayerInvulnerable, "Invulnerable");
                break;

            case INF:
                ActivateCheatCode(ref _isPlayerHasInfinityBullets, "Infinity bullets");
                break;

            case INV:
                ActivateCheatCode(ref _isPlayerInvisible, "Invisible");
                break;

            default:
                CheckActivatedWeaponCheatCode();
                break;
        }
    }

    private string GetStringEnding(string str, int charCount)
    {
        if (str.Length < charCount)
            return str;

        return str.Substring(_letters.Length - charCount, charCount);
    }

    private void ActivateCheatCode(ref bool state, string cheatName)
    {
        _letters = "";
        state = !state;
        string output = state ? "" : "DE";
        print($"{cheatName} CHEAT {output}ACTIVATED");
    }

    private void CheckInput()
    {
        if (Input.anyKeyDown)
        {
            foreach (KeyCode key in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(key))
                {
                    string result = key.ToString();
                    _letters += result[result.Length - 1];
                }
            }
        }
    }
}
