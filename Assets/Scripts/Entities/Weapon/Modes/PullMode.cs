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
    Vector3 velRef, rotationId = Vector3.zero;
    Rigidbody targetRB;

    [Header("Audio")]
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip pullClip;

    private void Update()
    {
        finalPos.rotation = Quaternion.Euler(rotationId);
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

        audioSource.clip = pullClip;
        audioSource.Play();

        target = ObjectDetection.getSelected().gameObject;

        FreezeCheck freezeCheck;

        if (target.TryGetComponent<FreezeCheck>(out freezeCheck))
        {
            if (freezeCheck.alreadyFreeze)
                freezeCheck.Defrost();
        }

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
        rotationId = Vector3.zero;
    }

    void PullTarget(float delta)
    {
        if (Vector3.Distance(finalPos.transform.position, target.transform.position) > 0.5f)
            targetRB.velocity = (finalPos.transform.position - target.transform.position) * smooth * 2 * delta;
        else
        {
            targetRB.velocity = Vector3.zero;
            target.transform.position = Vector3.SmoothDamp(target.transform.position, finalPos.transform.position,
                ref velRef, delta * smooth / 10);
        }

        //target.transform.position = Vector3.SmoothDamp(target.transform.position, finalPos.transform.position, ref velRef, delta * smooth);
    }

    public void ResetValues()
    {
        target = null;
        targetRB = null;
    }
}