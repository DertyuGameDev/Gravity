using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    public Pole[] poles;
    public float interactDistance;
    public static List<Magnet> magnets = new List<Magnet>();
    public Rigidbody rb;
    public Vector3 iresultant;
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
        foreach (Pole p in poles) {
            foreach (Magnet m in magnets) {
                if (m == this) {
                    continue;
                }

                dist = Vector3.Distance(this.transform.position, m.transform.position);
                if (dist > interactDistance)
                {
                    continue;
                }
                float denom = p.strength/((12 * dist * dist));
                if ( dist <= interactDistance) {
                    foreach (Pole po in m.poles) {
                        iresultant = -(-1^(p.polarity - po.polarity)) * po.strength * denom * Vector3.Normalize(p.transform.position - po.transform.position);
                    }
                }
            }
            rb.AddForceAtPosition(-iresultant, p.transform.position);

        }
        
    }
}
