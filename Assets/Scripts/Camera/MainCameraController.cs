using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class MainCameraController : MonoBehaviour
{
    private Transform _followTarget;
    private Camera _camera;

    private Coroutine _followingCoroutine;
    private Coroutine _lookingCoroutine;

    private const float BasePositionZ = -10;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            OnLookKeyPressed(true);
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            OnLookKeyPressed(false);
        }
    }

    public void Init(Transform followTarget)
    {
        _camera = GetComponent<Camera>();

        _followTarget = followTarget;
        _followingCoroutine = StartCoroutine(Following(_followTarget));
    }

    private IEnumerator Following(Transform target)
    {
        while (true)
        {
            _camera.transform.FollowTargetXY(target);
            yield return null;
        }
    }

    private IEnumerator Looking()
    {
        float sizeOfLookingArea = 3;
        Vector3 topRightPoint = transform.position + new Vector3(sizeOfLookingArea, sizeOfLookingArea, 0);
        Vector3 bottomLeftPoint = transform.position - new Vector3(sizeOfLookingArea, sizeOfLookingArea, 0);

        while (true)
        {
            float mouseInputX = Input.GetAxis("Mouse X");
            float mouseInputY = Input.GetAxis("Mouse Y");
            transform.Translate(mouseInputX, mouseInputY, 0);

            if (transform.position.y > topRightPoint.y)
            {
                transform.position = new Vector3(transform.position.x, topRightPoint.y, BasePositionZ);
            }

            if (transform.position.y < bottomLeftPoint.y)
            {
                transform.position = new Vector3(transform.position.x, bottomLeftPoint.y, BasePositionZ);
            }

            if (transform.position.x > topRightPoint.x)
            {
                transform.position = new Vector3(topRightPoint.x, transform.position.y, BasePositionZ);
            }

            if (transform.position.x < bottomLeftPoint.x)
            {
                transform.position = new Vector3(bottomLeftPoint.x, transform.position.y, BasePositionZ);
            }

            yield return null;
        }
    }

    private void OnLookKeyPressed(bool isKeyDown)
    {
        if (isKeyDown)
        {
            StopCoroutine(_followingCoroutine);
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _lookingCoroutine = StartCoroutine(Looking());
        }
        else
        {
            StopCoroutine(_lookingCoroutine);
            _followingCoroutine = StartCoroutine(Following(_followTarget));
        }
    }
}
