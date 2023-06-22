using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float _current = 1;

    public void ApplyDamage(float amount)
    {
        _current = Mathf.Clamp(_current - amount, 0, _current);

        if (_current <= 0)
            Destroy(gameObject);
    }
}