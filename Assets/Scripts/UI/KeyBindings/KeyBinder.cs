using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class KeyBinder : MonoBehaviour
{
    [SerializeField] private VerticalLayoutGroup _container;
    [SerializeField] private KeyBindDisplay _keyBindDisplayPrefab;
    [SerializeField] private Button _applyButton;

    private Dictionary<Keys, KeyCode> _copiedKeysMap;

    private void OnEnable()
    {
        _applyButton.onClick.AddListener(OnApplyButtonClicked);
        KeyBindDisplay.KeyBinded += OnKeyBinded;

        CopyKeysMap();
        Draw();
    }

    private void OnDisable()
    {
        _applyButton.onClick.RemoveListener(OnApplyButtonClicked);
        KeyBindDisplay.KeyBinded -= OnKeyBinded;
    }

    private void CopyKeysMap()
    {
        _copiedKeysMap = new Dictionary<Keys, KeyCode>();

        foreach (var i in PlayerInput.Instance.KeysMap)
        {
            _copiedKeysMap.Add(i.Key, i.Value);
        }
    }

    private void OnApplyButtonClicked()
    {
        ConfirmModalWindow.Instance.Show("Save binds?", ApplyBinds);
    }

    private void ApplyBinds()
    {
        for (int index = 0; index < _copiedKeysMap.Count; index++)
        {
            var item = _copiedKeysMap.ElementAt(index);
            PlayerInput.Instance.BindKey(item.Key, item.Value);
        }

        gameObject.SetActive(false);
    }

    private void Draw()
    {
        foreach (var el in _container.transform.GetComponentsInChildren<KeyBindDisplay>())
        {
            Destroy(el.gameObject);
        }

        print(_copiedKeysMap.Count);

        foreach (var el in _copiedKeysMap)
        {
            var item = Instantiate(_keyBindDisplayPrefab, _container.transform);
            item.Init(el.Value, el.Key);
        }
    }

    private void OnKeyBinded(Keys action, KeyCode keyCode)
    {
        _copiedKeysMap[action] = keyCode;
        var lastBindedKey = action;

        for (int index = 0; index < _copiedKeysMap.Count; index++)
        {
            var item = _copiedKeysMap.ElementAt(index);

            if (item.Key == lastBindedKey)
                continue;

            if (item.Value == keyCode)
                _copiedKeysMap[item.Key] = KeyCode.None;
        }

        Draw();
    }
}
