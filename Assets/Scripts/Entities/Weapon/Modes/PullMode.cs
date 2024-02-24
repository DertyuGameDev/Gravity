using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullMode : AllModes
{
    [Header("External References")]
    [SerializeField] GravityGunScript gravityGunScript;

    [Header("Config")]
    public GameObject target;
    [SerializeField] float range;
    [SerializeField] int[] layersAffect;

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
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, range))
        {
            foreach (var layer in layersAffect)
            {
                if (hit.transform.gameObject.layer == layer)
                {
                    target = hit.transform.gameObject;
                    target.transform.parent = null;
                    target.GetComponent<Rigidbody>().useGravity = false;

                    gravityGunScript.actualTarget = target;
                    gravityGunScript.smooth = smooth;
                    gravityGunScript.finalPos = finalPos;
                }
            }
        }
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
