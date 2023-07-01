using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class Car : MonoBehaviour
{
    public static Collider2D Collider { get; private set; }

    public static event UnityAction PlayerEntered;

    private void Start()
    {
        Collider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerController player))
            PlayerEntered?.Invoke();
    }
}
