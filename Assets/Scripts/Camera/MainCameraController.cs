using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class MainCameraController : MonoBehaviour
{
    private Transform _followTarget;
    private Camera _camera;

    public void Init(Transform followTarget)
    {
        _camera = GetComponent<Camera>();

        _followTarget = followTarget;
        StartCoroutine(Following(_followTarget));
    }

    private IEnumerator Following(Transform target)
    {
        while (true)
        {
            _camera.transform.FollowTargetXY(target);
            yield return null;
        }
    }
}
