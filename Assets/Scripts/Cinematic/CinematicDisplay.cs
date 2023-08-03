using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicDisplay : MonoBehaviour
{
    // костыль
    [SerializeField] private DialogTextDisplay _dialogTextDisplay;
    // костыль

    [Header("UI")]
    [SerializeField] private RectTransform _characterPanel;
    [SerializeField] private RectTransform _topBorder;
    [SerializeField] private RectTransform _bottomBorder;

    private void OnEnable()
    {
        // костыль
        StartAnimations();
        // костыль
    }

    private void StartAnimations()
    {
        StartCoroutine(CharacterPanelShowing());
        StartCoroutine(BorderShowing(_topBorder));
        StartCoroutine(BorderShowing(_bottomBorder));
    }

    private IEnumerator BorderShowing(RectTransform rectTransform)
    {
        var speed = Mathf.Abs(rectTransform.transform.localPosition.y);
        var startPosition = rectTransform.transform.localPosition;
        var positionYMultiplier = 2f;
        var delay = 0.1f;

        rectTransform.transform.localPosition = new Vector3(rectTransform.transform.localPosition.x, rectTransform.transform.localPosition.y * positionYMultiplier);

        yield return new WaitForSeconds(delay);

        while (Mathf.Abs(rectTransform.transform.localPosition.y) > Mathf.Abs(startPosition.y))
        {
            rectTransform.transform.localPosition = Vector2.MoveTowards(rectTransform.transform.localPosition, startPosition, Time.unscaledDeltaTime * speed);
            yield return null;
        }
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

        // костыль
        Time.timeScale = 0;
        _dialogTextDisplay.PrintText(new string[] 
        {
            "I exterminate every single one of them...",
            "Fucking bastards...",
            "Prepare to die...",
        });

        _dialogTextDisplay.DialogOver += OnDialogOver;
        // костыль
    }

    // костыль
    private void OnDialogOver()
    {
        _dialogTextDisplay.DialogOver -= OnDialogOver;
        GetComponent<CanvasGroup>().alpha = 0;
        Time.timeScale = 1;
    }
    // костыль
}
