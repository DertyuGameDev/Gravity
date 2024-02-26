using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VolumeMainMenu : MonoBehaviour
{
    public TextMeshProUGUI MusicVal;
    public TextMeshProUGUI SFXVal;

    private void Awake()
    {
        ShowMusicValue();
    }
    void ShowMusicValue()
    {
        MusicVal.text = (MusicMan.getMusicVol() * 100f).ToString("F0");
        SFXVal.text = (MusicMan.getSFXVol() * 100f).ToString("F0");
    }
    public void IncreaseMusic()
    {
        if (MusicMan.getMusicVol() + 0.1f > 1)
        {
            MusicMan.setMusicVol(1);
            ShowMusicValue();
            return;
        }

        MusicMan.setMusicVol(MusicMan.getMusicVol() + 0.1f);
        ShowMusicValue();
    }
    public void DecreaseMusic()
    {
        if (MusicMan.getMusicVol() - 0.1f < 0)
        {
            MusicMan.setMusicVol(0);
            ShowMusicValue();
            return;
        }

        MusicMan.setMusicVol(MusicMan.getMusicVol() - 0.1f);
        ShowMusicValue();
    }
    public void IncreaseSFX()
    {
        if (MusicMan.getSFXVol() + 0.1f > 1)
        {
            MusicMan.setSFXVol(1);
            ShowMusicValue();
            return;
        }


        MusicMan.setSFXVol(MusicMan.getSFXVol() + 0.1f);
        ShowMusicValue();
    }
    public void DecreaseSFX()
    {
        if (MusicMan.getSFXVol() - 0.1f < 0)
        {
            MusicMan.setSFXVol(0);
            ShowMusicValue();
            return;
        }

        MusicMan.setSFXVol(MusicMan.getSFXVol() - 0.1f);
        ShowMusicValue();
    }
}
