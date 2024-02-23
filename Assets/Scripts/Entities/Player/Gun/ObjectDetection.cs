using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDetection : MonoBehaviour
{
    private static List<Detector> inView = new List<Detector>();
    private Detector selected;
    [SerializeField]
    [Range(0,100)]
    private float reachDistance;
    private int lm;
    RaycastHit hit;
    Ray r;
    bool hits;
    public static ObjectDetection od;
// Start is called before the first frame update
void Start()
    {
        Detector.reach = reachDistance;
        lm = LayerMask.GetMask("Objects");
        od = this;
    }

    // Update is called once per frame
    void Update()
    {
        r = Camera.main.ViewportPointToRay(new Vector3(0.5f,0.5f,0));
        hits = Physics.Raycast(r, out hit, reachDistance, lm, QueryTriggerInteraction.Collide);

        if (hits)
        {
            selected = hit.transform.gameObject.GetComponent<Detector>();
            print("hello " + selected.obj.name);
        }
        else {
            selected = null;
        }
    }

    public static void toggleView(Detector d, bool t)
    {
        if (t)
        {
            if (!inView.Contains(d))
            {
                inView.Add(d);
            }
            print("hi");
        }
        else {
            inView.Remove(d);
            print("bye");
        }
    }


    public static List<Detector> getInView() {
        return inView;
    }

    public static bool isInView(Detector d)
    {
        return inView.Contains(d);
    }

    public static bool isSelected(Detector d)
    {
        return d == od.selected;
    }

    public static Detector getSelected()
    {
        return od.selected;
    }
}
