using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DetectionSystem : MonoBehaviour
{
    [Header("Hearing")]
    [SerializeField] private FieldOfHearing _fieldOfHearing;
    [SerializeField] private Vector2 _hearingAreaSize;

    [Header("Vision")]
    [SerializeField] private FieldOfView _fieldOfView;
    [SerializeField] private Vector2 _viewAreaSize;
    [SerializeField] private LayerMask _playerMask;
    [SerializeField] private LayerMask _obstacleMask;
    [SerializeField] private float _viewAngle = 180;

    public event UnityAction PlayerDetected;

    private void OnEnable()
    {
        _fieldOfHearing.PlayerDetected += OnPlayerDetected;
        _fieldOfView.PlayerDetected += OnPlayerDetected;
    }

    private void OnDisable()
    {
        _fieldOfHearing.PlayerDetected -= OnPlayerDetected;
        _fieldOfView.PlayerDetected -= OnPlayerDetected;
    }

    private void Start()
    {
        _fieldOfView.Init(_viewAreaSize, _viewAngle, _playerMask, _obstacleMask);
        _fieldOfHearing.Init(_hearingAreaSize);
    }

    private void OnPlayerDetected()
    {
        PlayerDetected?.Invoke();
    }

    public void SwitchHearingState(bool newState)
    {
        _fieldOfHearing.gameObject.SetActive(newState);
    }

    public void SwitchVisionState(bool newState)
    {
        _fieldOfView.gameObject.SetActive(newState);
    }
}
