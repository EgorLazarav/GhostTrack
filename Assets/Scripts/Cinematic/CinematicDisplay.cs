using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicDisplay : MonoBehaviour
{
    [SerializeField] private RectTransform _characterPanel;

    private void OnEnable()
    {
        StartCoroutine(CharacterPanelShowing());
    }

    private IEnumerator CharacterPanelShowing()
    {
        var speed = _characterPanel.transform.localPosition.x;
        var startPosition = _characterPanel.transform.localPosition;
        var positionXMultiplier = 2f;
        var delay = 0.1f;

        _characterPanel.transform.localPosition = new Vector3(_characterPanel.transform.localPosition.x * positionXMultiplier, _characterPanel.transform.localPosition.y);

        yield return new WaitForSeconds(delay);

        while (_characterPanel.transform.localPosition.x > startPosition.x)
        {
            _characterPanel.transform.localPosition = Vector2.MoveTowards(_characterPanel.transform.localPosition, startPosition, Time.unscaledDeltaTime * speed);
            yield return null;
        }
    }
}
