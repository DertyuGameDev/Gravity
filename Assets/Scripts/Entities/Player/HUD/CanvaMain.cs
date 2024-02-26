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
        MusicVal.text = (MusicMan.getMusicVol() * 100f).ToString();
        SFXVal.text = (MusicMan.getSFXVol() * 100f).ToString();
    }
    public void IncreaseMusic()
    {
        MusicMan.setMusicVol(MusicMan.getMusicVol() + 0.1f);
        ShowMusicValue();
    }
    public void DecreaseMusic()
    {
        MusicMan.setMusicVol(MusicMan.getMusicVol() - 0.1f);
        ShowMusicValue();
    }
    public void IncreaseSFX()
    {
        MusicMan.setSFXVol(MusicMan.getSFXVol() + 0.1f);
        ShowMusicValue();
    }
    public void DecreaseSFX()
    {
        MusicMan.setSFXVol(MusicMan.getSFXVol() - 0.1f);
        ShowMusicValue();
    }
}
