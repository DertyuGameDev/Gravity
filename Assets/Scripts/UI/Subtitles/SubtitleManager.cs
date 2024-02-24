using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SubtitleManager : MonoBehaviour
{
    private TextMeshProUGUI _textBox;
    private Image _textBackground;

    private readonly Queue<Subtitle> _subtitlesQueue = new();
    private bool _isProcessing;

    private void Awake()
    {
        _textBox = GetComponentInChildren<TextMeshProUGUI>();
        _textBackground = GetComponentInChildren<Image>();
    }

    private void Start()
    {
        ClearSubtitleText();
        SetOpacity(0f);
    }

    public void DisplaySubtitle(Subtitle subtitle)
    {
        _subtitlesQueue.Enqueue(subtitle);

        if (!_isProcessing)
        {
            StartCoroutine(ProcessSubtitlesQueue());
        }
    }

    private IEnumerator ProcessSubtitlesQueue()
    {
        _isProcessing = true;
        while (_subtitlesQueue.Count > 0)
        {
            var currentSubtitle = _subtitlesQueue.Dequeue();

            _textBox.text = currentSubtitle.text;
            _textBox.color = currentSubtitle.color;
            _textBox.fontStyle = currentSubtitle.fontStyle;

            SetOpacity(1f);

            yield return new WaitForSeconds(currentSubtitle.duration);
        }

        SetOpacity(0f);
        ClearSubtitleText();
        _isProcessing = false;
    }

    private void ClearSubtitleText()
    {
        _textBox.text = "";
    }

    private void SetOpacity(float value)
    {
        SetGraphicOpacity(_textBox, value);
        SetGraphicOpacity(_textBackground, value / 3f);
    }

    private static void SetGraphicOpacity(Graphic graphic, float value)
    {
        var color = graphic.color;
        color.a = value;
        graphic.color = color;
    }
}