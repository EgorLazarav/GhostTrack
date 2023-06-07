using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [SerializeField] private Transform _player;

    private Coroutine _followingCoroutine;
    private Coroutine _lookingCoroutine;

    private const float BasePositionZ = -10;

    private void Start()
    {
        _followingCoroutine = StartCoroutine(FollowingTarget(_player));
    }

    private void OnEnable()
    {
        PlayerInput.LookKeyPressed += OnLookKeyPressed;
    }

    private void OnDisable()
    {
        PlayerInput.LookKeyPressed -= OnLookKeyPressed;
    }

    private IEnumerator FollowingTarget(Transform target)
    {
        while (true)
        {
            transform.position = new Vector3(target.position.x, target.position.y, BasePositionZ);
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
            _followingCoroutine = StartCoroutine(FollowingTarget(_player));
        }
    }
}
