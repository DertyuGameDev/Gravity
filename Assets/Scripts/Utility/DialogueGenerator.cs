using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueGenerator : MonoBehaviour
{
    [SerializeField] private SubtitleManager subtitleManager;
    [SerializeField] private List<Subtitle> subtitles = new();

    public void PlayDialogue()
    {
        subtitles?.ForEach(subtitle => subtitleManager.DisplaySubtitle(subtitle));
    }
}