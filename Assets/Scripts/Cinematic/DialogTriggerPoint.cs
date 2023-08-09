using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class DialogTriggerPoint : MonoBehaviour
{
    [SerializeField] private string[] _messages;

    public static event UnityAction<string[]> Triggered;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerController player))
        {
            Triggered?.Invoke(_messages);
        }

        gameObject.SetActive(false);
    }
}