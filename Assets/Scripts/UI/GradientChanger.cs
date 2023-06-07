using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Collections;

public class GradientChanger : MonoBehaviour
{
    [SerializeField] private List<Color> _vertexColors;

    private TMP_Text _text;

    void Start()
    {
        _text = GetComponent<TMP_Text>();

        _vertexColors.Add(_text.colorGradient.topRight);
        _vertexColors.Add(_text.colorGradient.bottomRight);
        _vertexColors.Add(_text.colorGradient.bottomLeft);
        _vertexColors.Add(_text.colorGradient.topLeft);

        // _text.colorGradient = new VertexGradient(Color.black, Color.black, Color.black, Color.black);

        StartCoroutine(ColorChanging());
    }

    private IEnumerator ColorChanging()
    {
        while (true)
        {
            Color[] colors = new Color[_vertexColors.Count];

            for (int i = 0; i < colors.Length; i++)
            {
                colors[i] = _vertexColors[i];
                yield return null;
            }

            yield return null;

            for (int i = 0; i < _vertexColors.Count; i++)
            {
                if (i == _vertexColors.Count - 1)
                {
                    _vertexColors[i] = colors[0];
                    continue;
                }

                _vertexColors[i] = colors[i + 1];

                yield return null;
            }

            yield return null;

            while (_text.colorGradient.topRight != _vertexColors[0]
                || _text.colorGradient.bottomRight != _vertexColors[1] 
                || _text.colorGradient.bottomLeft != _vertexColors[2] 
                || _text.colorGradient.topLeft != _vertexColors[3]
                )
            {
                var topRightColor = Color.Lerp(_text.colorGradient.topRight, _vertexColors[0], Time.deltaTime);
                var bottomRightColor = Color.Lerp(_text.colorGradient.bottomRight, _vertexColors[1], Time.deltaTime);
                var bottomLeftColor = Color.Lerp(_text.colorGradient.bottomLeft, _vertexColors[2], Time.deltaTime);
                var topLeftColor = Color.Lerp(_text.colorGradient.topLeft, _vertexColors[3], Time.deltaTime);

                _text.colorGradient = new VertexGradient(topRightColor, bottomRightColor, bottomLeftColor, topLeftColor);
                yield return new WaitForSeconds(1f);
            }
        }
    }
}