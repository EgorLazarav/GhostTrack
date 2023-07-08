using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour
{
    public static event UnityAction<Vector2> MoveKeyPressing;
    public static event UnityAction ShootKeyPressing;
    public static event UnityAction PuncKeyPressed;
    public static event UnityAction DropWeaponKeyPressed;
    public static event UnityAction PickUpWeaponKeyPressed;

    public static PlayerInput Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void FixedUpdate()
    {
        CheckMoveKeysPressing();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            PuncKeyPressed?.Invoke();
        }

        if (Input.GetKey(KeyCode.Mouse0))
        {
            ShootKeyPressing?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            DropWeaponKeyPressed?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            PickUpWeaponKeyPressed?.Invoke();
        }
    }

    private static void CheckMoveKeysPressing()
    {
        Vector2 moveVector = Vector2.zero;

        if (Input.GetKey(KeyCode.W))
        {
            moveVector += Vector2.up;
        }

        if (Input.GetKey(KeyCode.A))
        {
            moveVector += Vector2.left;
        }

        if (Input.GetKey(KeyCode.S))
        {
            moveVector += Vector2.down;
        }

        if (Input.GetKey(KeyCode.D))
        {
            moveVector += Vector2.right;
        }

        MoveKeyPressing?.Invoke(moveVector);
    }
}