using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FreezeMode : AllModes
{
    [Header("Config")]
    public bool freezeMode;
    [SerializeField] GameObject target;
    [SerializeField] float secondsFreeze;
    Vector3 previousVel;
    Vector3 previousRot;
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
        Invoke(nameof(Defrost), secondsFreeze);
    }
    void FreezeTarget(GameObject obj)
    {
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        previousVel = rb.velocity;
        previousRot = rb.angularVelocity;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.freezeRotation = true;
        rb.useGravity = false;
    }
    void Defrost()
    {
        Rigidbody rb = target.GetComponent<Rigidbody>();
        rb.freezeRotation = false;
        rb.angularVelocity = previousRot;
        rb.velocity = previousVel;
        rb.useGravity = true;
    }
}
