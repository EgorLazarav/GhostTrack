using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class MainCameraController : MonoBehaviour
{
    [SerializeField] private Transform _followTarget;

    private Camera _camera;

    private void Start()
    {
        _camera = GetComponent<Camera>();
        StartCoroutine(Following());
    }

    private IEnumerator Following()
    {
        while (true)
        {
            _camera.transform.position = new Vector3(_followTarget.position.x, _followTarget.position.y, _camera.transform.position.z);
            yield return null;
        }
    }
}
