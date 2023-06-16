using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodSpawner : ObjectPool<BloodVFX>
{
    [SerializeField] private BloodVFX _bloodVFX;

    private void Start()
    {
        Init(_bloodVFX);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            var bloodVFX = GetItem();
            bloodVFX.transform.SetRandomPosition();
            bloodVFX.Play();
        }
    }
}
