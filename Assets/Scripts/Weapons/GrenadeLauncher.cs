using System.Collections;
using UnityEngine;

public class GrenadeLauncher : Weapon
{
    [SerializeField] private float _minHoldingTime = 1;
    [SerializeField] private float _maxHoldingTime = 3;

    protected override void CreateBullet(Quaternion rotation, float shotPower)
    {
        print("charging");
        StartCoroutine(Charging(rotation, shotPower));
    }

    private IEnumerator Charging(Quaternion rotation, float shotPower)
    {
        float timer = _minHoldingTime;

        while (timer < _maxHoldingTime)
        {
            timer += Time.deltaTime;
            yield return null;

            if (Input.GetKeyUp(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.Q))
                break;
        }

        base.CreateBullet(rotation, shotPower * timer);
    }
}