using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class CanvaMain : MonoBehaviour
{
    public static bool menuOpen;

    public GameObject crosshairCanvas;
    public GameObject interactionCanvas;
    public GameObject menuCanvas;
    public GameObject VolumeCanvas;
    public TextMeshProUGUI MusicVal;
    public TextMeshProUGUI SFXVal;
    public AudioMixer mixer;

    public void OpenMenu()
    {
        menuOpen = true;
        crosshairCanvas.SetActive(false);
        interactionCanvas.SetActive(false);
        menuCanvas.SetActive(true);

        Time.timeScale = 0;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void CloseMenu()
    {
        if (VolumeCanvas.activeSelf)
        {
            CloseVolume();
            return;
        }
        menuOpen = false;
        crosshairCanvas.SetActive(true);
        interactionCanvas.SetActive(true);
        menuCanvas.SetActive(false);
        VolumeCanvas.SetActive(false);

        Time.timeScale = 1;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void OpenVolume()
    {
        menuCanvas.SetActive(false);
        VolumeCanvas.SetActive(true);
        ShowMusicValue();
    }
    public void CloseVolume()
    {
        VolumeCanvas.SetActive(false);
        menuCanvas.SetActive(true);
    }
    public void QuitToMainMenu()
    {
        SceneManager.LoadScene(0);
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
