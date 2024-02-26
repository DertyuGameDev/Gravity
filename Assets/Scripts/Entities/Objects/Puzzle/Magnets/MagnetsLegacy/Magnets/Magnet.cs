using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    public Pole[] poles;
    public float interactDistance;
    public static List<Magnet> magnets = new List<Magnet>();
    public Rigidbody rb;
    private Vector3[] iresultant = { new Vector3(0, 0, 0), new Vector3(0, 0, 0) };
    // Start is called before the first frame update
    void Start()
    {
        magnets.Add(this);
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        float mag;
        float dist;
        iresultant[0] = new Vector3(0, 0, 0);
        iresultant[1] = new Vector3(0, 0, 0);
        foreach (Magnet m in magnets)
        {
            if (m == this)
            {
                continue;
            }
            dist = Vector3.Distance(this.transform.position, m.transform.position);
            if (dist > interactDistance)
            {
                continue;
            }
            float d = ((12 * dist * dist));
            float denom = poles[0].strength / d;
            float denom1 = poles[1].strength / d;

            iresultant[0] +=  m.poles[0].strength * denom * Vector3.Normalize(poles[0].transform.position - m.poles[0].transform.position);
            iresultant[1] +=  m.poles[1].strength * denom1 * Vector3.Normalize(poles[1].transform.position - m.poles[1].transform.position);
            iresultant[1] +=  m.poles[0].strength * denom1 * Vector3.Normalize(poles[1].transform.position - m.poles[0].transform.position);
            iresultant[0] +=  m.poles[1].strength * denom * Vector3.Normalize(poles[0].transform.position - m.poles[1].transform.position);


        }

            rb.AddForceAtPosition(iresultant[0] * Time.smoothDeltaTime, poles[0].transform.position);
            rb.AddForceAtPosition(iresultant[1] * Time.smoothDeltaTime, poles[1].transform.position);
    }
}
