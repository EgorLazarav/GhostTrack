using System.Collections.Generic;
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
    Look
}

public class PlayerInput : MonoBehaviour
{
    public static event UnityAction<Vector2> MoveKeyPressing;
    public static event UnityAction ShootKeyPressing;
    public static event UnityAction PuncKeyPressed;
    public static event UnityAction DropWeaponKeyPressed;
    public static event UnityAction PickUpWeaponKeyPressed;
    public static event UnityAction<bool> LookKeyPressed;

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

        KeysMap.Add(Keys.Look, KeyCode.LeftShift);
    }

    private void Update()
    {
        CheckLookKeyPressing();

        if (_isLookKeyPressing)
            return;

        CheckCombatKeysPressing();
        CheckMoveKeysPressing();
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

    public void TryBindKey(Keys key, KeyCode keyCode)
    {
        if (!KeysMap.ContainsKey(key))
            return;

        KeysMap[key] = keyCode;
    }
}