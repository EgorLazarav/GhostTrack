using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyBinder : MonoBehaviour
{
    [SerializeField] private VerticalLayoutGroup _container;
    [SerializeField] private KeyBindDisplay _keyBindDisplayPrefab;
    [SerializeField] private Button _applyButton;

    private void OnEnable()
    {
        Draw();

        _applyButton.onClick.AddListener(OnApplyButtonClicked);
    }

    private void OnDisable()
    {
        _applyButton.onClick.RemoveListener(OnApplyButtonClicked);
    }

    private void OnApplyButtonClicked()
    {
        ConfirmModalWindow.Instance.Show("Save binds?", ApplyBinds);
    }

    private void ApplyBinds()
    {
        foreach (var el in _container.transform.GetComponentsInChildren<KeyBindDisplay>())
        {
            PlayerInput.Instance.TryBindKey(el.BindingKey, el.CurrentKeyBind);
        }

        print(123456);
        gameObject.SetActive(false);
    }

    private void Draw()
    {
        foreach (var el in _container.transform.GetComponentsInChildren<KeyBindDisplay>())
        {
            Destroy(el.gameObject);
        }

        foreach (var el in PlayerInput.Instance.KeysMap)
        {
            var item = Instantiate(_keyBindDisplayPrefab, _container.transform);
            item.Init(el.Value, el.Key);
        }
    }
}
