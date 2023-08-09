using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public enum Keys
{
    MoveUp,
    MoveDown,
    MoveLeft,
    MoveRight,
    Shoot,
    Punch,
    DropWeapon,
    PickUpWeapon,
    Look,
    Shift,
    Skip
}

public class PlayerInput : MonoBehaviour
{
    public static event UnityAction<Vector2> MoveKeyPressing;
    public static event UnityAction ShootKeyPressing;
    public static event UnityAction PuncKeyPressed;
    public static event UnityAction DropWeaponKeyPressed;
    public static event UnityAction PickUpWeaponKeyPressed;
    public static event UnityAction<bool> LookKeyPressed;
    public static event UnityAction ShiftKeyPressed;
    public static event UnityAction SkipKeyPressed;

    public static PlayerInput Instance { get; private set; }
    public Dictionary<Keys, KeyCode> KeysMap { get; private set; } = new Dictionary<Keys, KeyCode>();

    private bool _isLookKeyPressing = false;

    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        InitKeys();
    }

    private void InitKeys()
    {
        KeysMap.Add(Keys.MoveUp, KeyCode.W);
        KeysMap.Add(Keys.MoveLeft, KeyCode.A);
        KeysMap.Add(Keys.MoveDown, KeyCode.S);
        KeysMap.Add(Keys.MoveRight, KeyCode.D);

        KeysMap.Add(Keys.Shoot, KeyCode.Mouse0);
        KeysMap.Add(Keys.Punch, KeyCode.Mouse1);
        KeysMap.Add(Keys.PickUpWeapon, KeyCode.E);
        KeysMap.Add(Keys.DropWeapon, KeyCode.Q);

        KeysMap.Add(Keys.Look, KeyCode.V);
        KeysMap.Add(Keys.Shift, KeyCode.LeftShift);
        KeysMap.Add(Keys.Skip, KeyCode.Space);
    }

    private void Update()
    {
        CheckSkipKeyPressed();

        if (Time.timeScale == 0)
            return;

        CheckLookKeyPressing();

        if (_isLookKeyPressing)
            return;

        CheckCombatKeysPressing();
        CheckMoveKeysPressing();
    }

    private void CheckSkipKeyPressed()
    {
        if (Input.GetKeyDown(KeysMap[Keys.Skip]))
        {
            SkipKeyPressed?.Invoke();
        }
    }

    private void CheckCombatKeysPressing()
    {
        if (Input.GetKeyDown(KeysMap[Keys.Punch]))
        {
            PuncKeyPressed?.Invoke();
        }

        if (Input.GetKey(KeysMap[Keys.Shoot]))
        {
            ShootKeyPressing?.Invoke();
        }

        if (Input.GetKeyDown(KeysMap[Keys.DropWeapon]))
        {
            DropWeaponKeyPressed?.Invoke();
        }

        if (Input.GetKeyDown(KeysMap[Keys.PickUpWeapon]))
        {
            PickUpWeaponKeyPressed?.Invoke();
        }

        if (Input.GetKeyDown(KeysMap[Keys.Shift]))
        {
            ShiftKeyPressed?.Invoke();
        }
    }

    private void CheckLookKeyPressing()
    {
        if (Input.GetKeyDown(KeysMap[Keys.Look]))
        {
            LookKeyPressed?.Invoke(true);
            _isLookKeyPressing = true;
        }

        if (Input.GetKeyUp(KeysMap[Keys.Look]))
        {
            LookKeyPressed?.Invoke(false);
            _isLookKeyPressing = false;
        }
    }

    private void CheckMoveKeysPressing()
    {
        Vector2 moveVector = Vector2.zero;

        if (Input.GetKey(KeysMap[Keys.MoveUp]))
        {
            moveVector += Vector2.up;
        }

        if (Input.GetKey(KeysMap[Keys.MoveLeft]))
        {
            moveVector += Vector2.left;
        }

        if (Input.GetKey(KeysMap[Keys.MoveDown]))
        {
            moveVector += Vector2.down;
        }

        if (Input.GetKey(KeysMap[Keys.MoveRight]))
        {
            moveVector += Vector2.right;
        }

        MoveKeyPressing?.Invoke(moveVector);
    }

    public void BindKey(Keys key, KeyCode keyCode)
    {
        KeysMap[key] = keyCode;
    }
}