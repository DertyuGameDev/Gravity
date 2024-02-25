using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullMode : MonoBehaviour
{
    [Header("External References")]
    [SerializeField] GravityGunScript gravityGunScript;

    [Header("Config")]
    public GameObject target;

    [Header("Pull Config")]
    [SerializeField] Transform finalPos;
    [SerializeField] float smooth;
    Vector3 velRef;
    Rigidbody targetRB;

    private void Update()
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

        if (gravityGunScript.TargetIsFreeze())
            gravityGunScript.target.GetComponent<FreezeCheck>().Defrost();


        target = ObjectDetection.getSelected().gameObject;
        target.transform.parent = null;
        target.GetComponent<Rigidbody>().useGravity = false;
        target.GetComponent<Rigidbody>().velocity = Vector3.zero;
        targetRB = target.GetComponent<Rigidbody>();

        
        gravityGunScript.target = target;
        gravityGunScript.smooth = smooth;
        gravityGunScript.finalPos = finalPos;
        gravityGunScript.targetRB = targetRB;
    }
    void DeselectTarget()
    {
        if (target != null)
            target.GetComponent<Rigidbody>().useGravity = true;
        gravityGunScript.target = null;
        target = null;
    }
    void PullTarget(float delta)
    {
        if (Vector3.Distance(finalPos.transform.position, target.transform.position) > 0.5f)
            targetRB.velocity = (finalPos.transform.position - target.transform.position) * smooth * 2 * delta;
        else
        {
            targetRB.velocity = Vector3.zero;
            target.transform.position = Vector3.SmoothDamp(target.transform.position, finalPos.transform.position, ref velRef, delta * smooth / 10);
        }
            
        //target.transform.position = Vector3.SmoothDamp(target.transform.position, finalPos.transform.position, ref velRef, delta * smooth);
        target.transform.rotation = Quaternion.Lerp(target.transform.rotation, finalPos.rotation, delta * smooth/2);
    }
    
    public void ResetValues()
    {
        target = null;
        targetRB = null;
    }
}
