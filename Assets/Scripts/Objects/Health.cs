using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    private int _currentPercent = 100;
    private DamageReducer _damageReducer;

    private void Start()
    {
        if (TryGetComponent(out DamageReducer damageReducer))
            _damageReducer = damageReducer;
    }

    public void ApplyDamage(int amount = 100)
    {
        if (_damageReducer != null)
            amount = _damageReducer.Reduce(amount);

        _currentPercent = Mathf.Clamp(_currentPercent - amount, 0, _currentPercent);

        if (_currentPercent <= 0)
            Destroy(gameObject);
    }
}
