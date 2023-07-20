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

    private float _sizeOfLookingArea;

    private const float BasePositionZ = -10;

    private void OnEnable()
    {
        PlayerInput.LookKeyPressed += OnLookKeyPressed;
    }

    private void OnDisable()
    {
        PlayerInput.LookKeyPressed -= OnLookKeyPressed;
    }

    private void Start()
    {
        Vector3 basePosition = transform.position;
        basePosition.z = BasePositionZ;
        transform.position = basePosition;
    }

    public void Init(Transform followTarget, float sizeOfLookingArea)
    {
        _camera = GetComponent<Camera>();

        _followTarget = followTarget;
        _followingCoroutine = StartCoroutine(Following(_followTarget));

        _sizeOfLookingArea = sizeOfLookingArea;
    }

    private IEnumerator Following(Transform target)
    {
        while (true)
        {
            if (target == null)
                break;

            _camera.transform.SetPositionToTargetXY(target);
            yield return null;
        }
    }

    private void OnLookKeyPressed(bool isKeyDown)
    {
        if (isKeyDown)
        {
            StopCoroutine(_followingCoroutine);
            _lookingCoroutine = StartCoroutine(Looking(_sizeOfLookingArea));
        }
        else
        {
            StopCoroutine(_lookingCoroutine);
            _followingCoroutine = StartCoroutine(Following(_followTarget));
        }
    }

    private IEnumerator Looking(float sizeOfLookingArea)
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 topRightPoint = transform.position + new Vector3(sizeOfLookingArea, sizeOfLookingArea, 0);
        Vector3 bottomLeftPoint = transform.position - new Vector3(sizeOfLookingArea, sizeOfLookingArea, 0);

        while (true)
        {
            float mouseInputX = Input.GetAxis("Mouse X");
            float mouseInputY = Input.GetAxis("Mouse Y");

            transform.Translate(mouseInputX, mouseInputY, 0);
            ClampPosition(topRightPoint, bottomLeftPoint);

            yield return null;
        }
    }

    private void ClampPosition(Vector3 topRightPoint, Vector3 bottomLeftPoint)
    {
        if (transform.position.y > topRightPoint.y)
            transform.position = new Vector3(transform.position.x, topRightPoint.y, BasePositionZ);

        if (transform.position.y < bottomLeftPoint.y)
            transform.position = new Vector3(transform.position.x, bottomLeftPoint.y, BasePositionZ);

        if (transform.position.x > topRightPoint.x)
            transform.position = new Vector3(topRightPoint.x, transform.position.y, BasePositionZ);

        if (transform.position.x < bottomLeftPoint.x)
            transform.position = new Vector3(bottomLeftPoint.x, transform.position.y, BasePositionZ);
    }
}
