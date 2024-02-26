using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class Magnets : MonoBehaviour
{
    [Range(-1, 1)]
    public int direction;
    public float strength;
    public Transform t;
    private Rigidbody rb;
    public static List<Magnets> mags = new List<Magnets>();
    public float interactDist;
    // Start is called before the first frame update
    void Start()
    {
        mags.Add(this);
        t = this.transform;
        rb = this.transform.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 res = Vector3.zero;
        foreach(Magnets m in mags) {
            if (m == this) {
                continue;
            }
            float d = Vector3.Distance(t.position, m.t.position);
            if (Mathf.Abs(d) > interactDist) {
                continue;
            }

            res += (direction*strength+m.direction*m.strength)*(1/(12*d*d))*Vector3.Normalize(t.position - m.t.position);
        }
        rb.AddForce(-res, ForceMode.Force);
    }
}
