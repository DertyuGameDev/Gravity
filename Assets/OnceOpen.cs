using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnceOpen : MonoBehaviour
{
    [SerializeField] int delay;
    public void Open()
    {
        StopAllCoroutines();
        StartCoroutine(open());
    }
    private IEnumerator open()
    {
        Debug.Log(0);
        this.GetComponent<Animator>().SetBool("IsActivated", true);
        yield return new WaitForSeconds(delay);
        this.GetComponent<Animator>().SetBool("IsActivated", false);
    }
}
