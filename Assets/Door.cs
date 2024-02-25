using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] GameObject but;
    void Update()
    {
        this.GetComponent<Animator>().SetBool("IsActivated", but.GetComponent<Animator>().GetBool("isActivated"));
    }
}
