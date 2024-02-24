using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeMode : AllModes
{
    [Header("Config")]
    [SerializeField] GameObject target;
    [SerializeField] float range;
    [SerializeField] int[] layersAffect;
    public void Fire()
    {
        SelectTarget();


    }
    void SelectTarget()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, range))
        {
            foreach (var layer in layersAffect)
            {
                if (hit.transform.gameObject.layer == layer)
                {
                    target = hit.transform.gameObject;
                }
            }
        }
    }
}
