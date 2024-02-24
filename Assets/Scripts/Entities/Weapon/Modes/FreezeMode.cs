using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FreezeMode : AllModes
{
    [Header("Config")]
    public bool freezeMode;
    [SerializeField] GameObject target;
    public void Fire()
    {
        SelectTarget();


    }
    void SelectTarget()
    {
        if (ObjectDetection.getSelected() == null)
            return;

        target = ObjectDetection.getSelected().gameObject;
        freezeMode = true;

        FreezeTarget(target);
    }
    void FreezeTarget(GameObject obj)
    {
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.useGravity = false;
    }
}
