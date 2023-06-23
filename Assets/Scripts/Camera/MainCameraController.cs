using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class MainCameraController : MonoBehaviour
{
    [SerializeField] private Transform _followTarget;

    private Camera _camera;

    public void Init()
    {
        _camera = GetComponent<Camera>();
        StartCoroutine(Following());
    }

    private IEnumerator Following()
    {
        while (true)
        {
            _camera.transform.FollowTargetXY(_followTarget);
            yield return null;
        }
    }
}
