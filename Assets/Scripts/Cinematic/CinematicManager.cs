using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CinematicManager : MonoBehaviour
{
    [SerializeField] private DialogTextDisplay _dialogTextDisplay;

    [Header("UI")]
    [SerializeField] private RectTransform _characterPanel;
    [SerializeField] private RectTransform _topBorder;
    [SerializeField] private RectTransform _bottomBorder;

    private Vector3 _baseCharacterPanelScale;
    private Vector3 _baseTopBorderScale;
    private Vector3 _baseBottomBorderScale;

    private void Awake()
    {
        _baseCharacterPanelScale = _characterPanel.localScale;
        _baseTopBorderScale = _topBorder.localScale;
        _baseBottomBorderScale = _bottomBorder.localScale;
    }

    private void OnEnable()
    {
        _dialogTextDisplay.DialogOver += OnDialogOver;
        DialogTriggerPoint.Triggered += OnDialogTriggerPointTriggered;
    }

    private void OnDisable()
    {
        _dialogTextDisplay.DialogOver -= OnDialogOver;
        DialogTriggerPoint.Triggered -= OnDialogTriggerPointTriggered;
    }

    private void OnDialogTriggerPointTriggered(string[] messages)
    {
        StartCoroutine(Scaling(_topBorder));
        StartCoroutine(Scaling(_bottomBorder));
        StartCoroutine(Scaling(_characterPanel));

        _dialogTextDisplay.StartDialog(messages);
        GamePauseManager.Instance.Unpause(false);
    }

    private IEnumerator Scaling(RectTransform rectTransform)
    {
        yield return new WaitForEndOfFrame();

        float speed = 1f;

        while (rectTransform.localScale != Vector3.one)
        {
            rectTransform.localScale = Vector3.MoveTowards(rectTransform.localScale, Vector3.one, Time.unscaledDeltaTime * speed);

            yield return null;
        }
    }

    private void OnDialogOver()
    {
        _characterPanel.localScale = _baseCharacterPanelScale;
        _topBorder.localScale = _baseTopBorderScale;
        _bottomBorder.localScale = _baseBottomBorderScale;

        GamePauseManager.Instance.Unpause(true);
    }
}
