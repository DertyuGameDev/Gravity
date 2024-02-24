using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushMode1 : AllModes
{
    [Header("External References")]
    [SerializeField] GravityGunScript gravityGunScript;

    [Header("Config")]
    [SerializeField] float force;
    [SerializeField] float range;
    [SerializeField] int[] layersAffect;
    [SerializeField] Collider[] collidersTarget;
    [SerializeField] List<Detector> targets;

    private void Update()
    {
        targets = ObjectDetection.getInView();
    }
    public void Fire()
    {
        if (gravityGunScript.actualTarget == null)
        {
            DetectEntity();
            return;
        }

        GameObject obj = gravityGunScript.actualTarget;
        gravityGunScript.DeselectTarget();
        Push(obj);    
        
    }
    void DetectEntity()
    {
        targets = ObjectDetection.getInView();

        foreach (Detector item in targets)
        {
            Push(item.gameObject);
        }
        /*
        collidersTarget = Physics.OverlapCapsule(transform.position + Camera.main.transform.forward * 1
                                                , transform.position + Camera.main.transform.forward * range
                                                , 3);

        foreach (Collider collider in collidersTarget)
        {
            foreach (int layer in layersAffect)
            {
                if (collider.gameObject.layer == layer)
                {
                    Push(collider.gameObject);
                }
            }
        }*/
    }
    void Push(GameObject target)
    {
        target.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * force, ForceMode.Impulse);
    }
}
