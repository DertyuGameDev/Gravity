using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour
{
    private bool isVisible = false;
    private bool visCheck;
    public GameObject obj;
    public static float reach;
    [SerializeField]
    private Renderer rend;
    // Start is called before the first frame update
    void Start()
    {
        obj = this.transform.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(Vector3.Distance(this.transform.position, GameManager.player.transform.position)) <= reach)
        {
            visCheck = Camera.main.IsObjectVisible(rend);
            if (visCheck != isVisible)
            {
                ObjectDetection.toggleView(this, visCheck);
                isVisible = visCheck;
            }
        }
        else if (isVisible)
        {


            ObjectDetection.toggleView(this, false);
            isVisible = false ;

        }
    }

    public bool amIVisible()
    {
        return isVisible;
    }
}
