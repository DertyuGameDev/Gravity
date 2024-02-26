using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMusicMen : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource a;
    public int mode;
    void Start()
    {
        MusicMan.UpdateLists(a, mode);
    }

}
