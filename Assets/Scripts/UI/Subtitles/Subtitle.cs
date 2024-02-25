using System;
using TMPro;
using UnityEngine;

[Serializable]
public class Subtitle
{
    public string text;
    public float duration;
    public Color color;
    public FontStyles fontStyle;
    public AudioClip audioClip;

    public Subtitle()
    {
    }

    public Subtitle(string text, float duration, Color color, AudioClip audioClip)
    {
        this.text = text;
        this.duration = duration;
        this.color = color;
        this.audioClip = audioClip;
    }
}