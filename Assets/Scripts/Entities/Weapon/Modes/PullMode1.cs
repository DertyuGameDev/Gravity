using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullMode1 : AllModes
{
    [Header("External References")]
    [SerializeField] GravityGunScript gravityGunScript;

    [Header("Config")]
    public GameObject target;

    [Header("Pull Config")]
    [SerializeField] Transform finalPos;
    [SerializeField] float smooth;
    Vector3 velRef;

    private void LateUpdate()
    {
        //This help when the V-Sync is desactivated
        float newDelta = 1.0f - (float)System.Math.Pow(0.95, Time.deltaTime * 60.0f);

        if (target != null)
            PullTarget(newDelta);
    }
    public void Fire()
    {
        if (target == null)
            SelectTarget();
        else
            DeselectTarget();
    }
    void SelectTarget()
    {
        if (ObjectDetection.getSelected() == null)
            return;

        target = ObjectDetection.getSelected().gameObject;
        target.transform.parent = null;
        target.GetComponent<Rigidbody>().useGravity = false;

        gravityGunScript.actualTarget = target;
        gravityGunScript.smooth = smooth;
        gravityGunScript.finalPos = finalPos;
    }
    void DeselectTarget()
    {
        if (target != null)
            target.GetComponent<Rigidbody>().useGravity = true;
        target = null;
    }
    void PullTarget(float delta)
    {
        target.transform.position = Vector3.SmoothDamp(target.transform.position, finalPos.transform.position, ref velRef, delta * smooth);
    }
}
