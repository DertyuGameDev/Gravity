using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameObject player;
    public static Animator fade;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        //fade = GameObject.FindGameObjectWithTag("fade").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
