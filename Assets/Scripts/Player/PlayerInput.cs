using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private KeyCode _moveUpKey = KeyCode.W;
    [SerializeField] private KeyCode _moveDownKey = KeyCode.S;
    [SerializeField] private KeyCode _moveLeftKey = KeyCode.A;
    [SerializeField] private KeyCode _moveRightKey = KeyCode.D;

    [Header("Combat")]
    [SerializeField] private KeyCode _interactKey = KeyCode.E;
    [SerializeField] private KeyCode _shootKey = KeyCode.Mouse0;
    [SerializeField] private KeyCode _throwGunKey = KeyCode.Mouse1;

    [Header("Optional")]
    [SerializeField] private KeyCode _lookKey = KeyCode.LeftShift;
    [SerializeField] private KeyCode _restartKey = KeyCode.R;
    [SerializeField] private KeyCode _openInGameMenuKey = KeyCode.Escape;

    public static event UnityAction<bool> LookKeyPressed;
    public static event UnityAction<Vector2> MoveKeyPressing;
    public static event UnityAction ShootKeyPressing;
    public static event UnityAction ThrowGunKeyPressed;
    public static event UnityAction InteractKeyPressed;
    public static event UnityAction RestartKeyPressed;
    public static event UnityAction OpenInGameMenuKeyPressed;

    private void Update()
    {
        if (Time.timeScale != 0)
        {
            CheckMovementKeys();
            CheckShootKey();
            CheckThrowGunKey();
            CheckInteractKey();
            CheckLookKey();
        }

        CheckRestartKey();
        CheckOpenMenuKey();
    }

    private void CheckMovementKeys()
    {
        Vector2 direction = Vector2.zero;

        if (Input.GetKey(_moveUpKey))
        {
            direction += Vector2.up;
        }

        if (Input.GetKey(_moveRightKey))
        {
            direction += Vector2.right;
        }

        if (Input.GetKey(_moveDownKey))
        {
            direction += Vector2.down;
        }

        if (Input.GetKey(_moveLeftKey))
        {
            direction += Vector2.left;
        }

        MoveKeyPressing?.Invoke(direction);
    }

    private void CheckThrowGunKey()
    {
        if (Input.GetKeyDown(_throwGunKey))
        {
            ThrowGunKeyPressed?.Invoke();
        }
    }

    private void CheckInteractKey()
    {
        if (Input.GetKeyDown(_interactKey))
        {
            InteractKeyPressed?.Invoke();
        }
    }

    private void CheckShootKey()
    {
        if (Input.GetKey(_shootKey))
        {
            ShootKeyPressing?.Invoke();
        }
    }

    private void CheckLookKey()
    {
        if (Input.GetKeyDown(_lookKey))
        {
            LookKeyPressed?.Invoke(true);
        }
        else if (Input.GetKeyUp(_lookKey))
        {
            LookKeyPressed?.Invoke(false);
        }
    }

    private void CheckRestartKey()
    {
        if (Input.GetKey(_restartKey))
        {
            RestartKeyPressed?.Invoke();
        }
    }

    private void CheckOpenMenuKey()
    {
        if (Input.GetKeyDown(_openInGameMenuKey))
        {
            OpenInGameMenuKeyPressed?.Invoke();         
        }
    }
}
