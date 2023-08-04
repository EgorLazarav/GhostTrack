using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CinematicDisplay : MonoBehaviour
{
    // костыль
    [SerializeField] private DialogTextDisplay _dialogTextDisplay;
    // костыль

    [Header("UI")]
    [SerializeField] private RectTransform _characterPanel;
    [SerializeField] private Image _characterPortait;
    [SerializeField] private RectTransform _topBorder;
    [SerializeField] private RectTransform _bottomBorder;

    private void OnEnable()
    {
        // костыль
        StartAnimations();
        // костыль
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameObject.SetActive(false);
        }
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

    private void StartAnimations()
    {
        StartCoroutine(Scaling(_topBorder));
        StartCoroutine(Scaling(_bottomBorder));
        StartCoroutine(Scaling(_characterPanel));

        Time.timeScale = 0;

        _dialogTextDisplay.PrintText(new string[]
        {
            "I exterminate every single one of them...",
            "Fucking bastards...",
            "Prepare to die...",
        });

        _dialogTextDisplay.DialogOver += OnDialogOver;
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
