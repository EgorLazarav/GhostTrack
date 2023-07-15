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

        if (_letters.Length >= 3)
        {
            if (GetStringEnding(_letters, 3) == GOD)
            {
                ActivateCheatCode(ref _isPlayerInvulnerable, "Invulnerable");
            }
            else if (GetStringEnding(_letters, 3) == INF)
            {
                ActivateCheatCode(ref _isPlayerHasInfinityBullets, "Infinity bullets");
            }
            else if (GetStringEnding(_letters, 3) == INV)
            {
                ActivateCheatCode(ref _isPlayerInvisible, "Invisible");
            }
        }
    }

    private string GetStringEnding(string str, int charCount)
    {
        if (str.Length < charCount)
            throw new IndexOutOfRangeException();

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
            foreach (KeyCode key in Enum.GetValues(typeof(KeyCode)))
                if (Input.GetKeyDown(key))
                    _letters += key.ToString();
    }
}
