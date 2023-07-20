using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum Keys
{
    MoveUpKey,
    MoveDownKey,
    MoveLeftKey,
    MoveRightKey,
    ShootKey,
    PunchKey,
    DropWeaponKey,
    PickUpWeaponKey,
    LookKey
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
        KeysMap.Add(Keys.MoveUpKey, KeyCode.W);
        KeysMap.Add(Keys.MoveLeftKey, KeyCode.A);
        KeysMap.Add(Keys.MoveDownKey, KeyCode.S);
        KeysMap.Add(Keys.MoveRightKey, KeyCode.D);

        KeysMap.Add(Keys.ShootKey, KeyCode.Mouse0);
        KeysMap.Add(Keys.PunchKey, KeyCode.Mouse1);
        KeysMap.Add(Keys.PickUpWeaponKey, KeyCode.E);
        KeysMap.Add(Keys.DropWeaponKey, KeyCode.Q);

        KeysMap.Add(Keys.LookKey, KeyCode.LeftShift);
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
        if (Input.GetKeyDown(KeysMap[Keys.PunchKey]))
        {
            PuncKeyPressed?.Invoke();
        }

        if (Input.GetKey(KeysMap[Keys.ShootKey]))
        {
            ShootKeyPressing?.Invoke();
        }

        if (Input.GetKeyDown(KeysMap[Keys.DropWeaponKey]))
        {
            DropWeaponKeyPressed?.Invoke();
        }

        if (Input.GetKeyDown(KeysMap[Keys.PickUpWeaponKey]))
        {
            PickUpWeaponKeyPressed?.Invoke();
        }
    }

    private void CheckLookKeyPressing()
    {
        if (Input.GetKeyDown(KeysMap[Keys.LookKey]))
        {
            LookKeyPressed?.Invoke(true);
            _isLookKeyPressing = true;
        }

        if (Input.GetKeyUp(KeysMap[Keys.LookKey]))
        {
            LookKeyPressed?.Invoke(false);
            _isLookKeyPressing = false;
        }
    }

    private void CheckMoveKeysPressing()
    {
        Vector2 moveVector = Vector2.zero;

        if (Input.GetKey(KeysMap[Keys.MoveUpKey]))
        {
            moveVector += Vector2.up;
        }

        if (Input.GetKey(KeysMap[Keys.MoveLeftKey]))
        {
            moveVector += Vector2.left;
        }

        if (Input.GetKey(KeysMap[Keys.MoveDownKey]))
        {
            moveVector += Vector2.down;
        }

        if (Input.GetKey(KeysMap[Keys.MoveRightKey]))
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

        foreach (var k in KeysMap)
        {
            if (k.Value == keyCode)
            {
                KeysMap[k.Key] = KeyCode.None;
            }
        }
    }
}