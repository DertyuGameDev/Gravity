using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] GameObject but;
    [SerializeField] bool once;
    private bool start;
    private void Awake()
    {
        start = false;
    }
    void Update()
    {
        if (!start)
        {
            this.GetComponent<Animator>().SetBool("IsActivated", but.GetComponent<Animator>().GetBool("isActivated"));
        }
        if(but.GetComponent<Animator>().GetBool("isActivated") == true && once)
        {
            StartCoroutine(OpenOnce());
        }
    }
    private IEnumerator OpenOnce()
    {
        this.GetComponent<Animator>().SetBool("IsActivated", true);
        start = true;
        yield return new WaitForSeconds(3);
        this.GetComponent<Door>().enabled = false;
    }
}
