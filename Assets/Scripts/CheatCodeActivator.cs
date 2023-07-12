using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatCodeActivator : MonoBehaviour
{
    public static bool IsInvulnerable { get; private set; } = false;
    public static bool IsInfinityBullets { get; private set; } = false;
    public static bool IsPlayerInvisible { get; private set; } = false;

    private const string GOD = nameof(GOD); // player is invulnerable
    private const string INF = nameof(INF); // infinity ammo
    private const string INV = nameof(INV); // invisible (enemies ignores player)
    private const string IMD = nameof(IMD); // invaders must die (kill all enemies)

    private const string EW1 = nameof(EW1); // player get's Uzi
    private const string EW2 = nameof(EW2); // player get's AK47
    private const string EW3 = nameof(EW3); // player get's M249
    private const string EW4 = nameof(EW4); // player get's grenadelauncher
    private const string EW5 = nameof(EW5); // player get's AWM
    private const string EW6 = nameof(EW6); // player get's Shotgun

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
