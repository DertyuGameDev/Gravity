﻿using UnityEngine;

public class SubtitleTest : MonoBehaviour
{
    private SubtitleManager _subtitleManager;

    private void Awake()
    {
        _subtitleManager = GetComponent<SubtitleManager>();
    }

    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.T)) return;

        _subtitleManager.DisplaySubtitle(new Subtitle("TEST-" + Time.deltaTime, 2f, Color.white, null));
    }
}