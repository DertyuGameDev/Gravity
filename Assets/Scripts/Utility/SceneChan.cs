using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneChan : MonoBehaviour
{

    public string sc;


    public void change()
    {
        GameManager.fade.SetTrigger("fadeOut");
        StartCoroutine(chan(sc));

    }

    public IEnumerator chan(string stc)
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(stc);
    }
}
