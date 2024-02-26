using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicMan : MonoBehaviour
{
    // Start is called before the first frame update
    public static List<AudioSource> music;
    public static List<AudioSource> sfx;

    public static MusicMan instance;

    public static float musicVolume = 0.4f;
    public static float sfxVolume = 0.5f;
    void Start()
    {
        if (instance != null & instance != this) {
            Destroy(this.gameObject);
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void UpdateLists(AudioSource a, int t) {
        if (t == 0 && !music.Contains(a)) {
            music.Add(a);
        }
        else if (t == 1 && !sfx.Contains(a))
        {
            sfx.Add(a);
        }
    }

    public static void setMusicVol(float a) {
        musicVolume = a;
        reload();
    }

    public static void setSFXVol(float a)
    {
        sfxVolume = a;
        reload();
    }

    private static void reload() {
        foreach (AudioSource a in music) {
            a.volume = musicVolume;
        }
        foreach (AudioSource a in sfx)
        {
            a.volume = sfxVolume;
        }
    }
}
