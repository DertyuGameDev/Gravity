using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushMode : AllModes
{
    [Header("External References")]
    [SerializeField] GravityGunScript gravityGunScript;

    [Header("Config")]
    [SerializeField] float force;
    [SerializeField] List<Detector> targets;

    private void Update()
    {
        targets = ObjectDetection.getInView();
    }
    public void Fire()
    {
        if (gravityGunScript.target == null)
        {
            DetectEntity();
            return;
        }

        GameObject obj = gravityGunScript.target;
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
    }
    void Push(GameObject target)
    {
        Rigidbody rb = target.GetComponent<Rigidbody>();
        rb.AddForce(Camera.main.transform.forward * force, ForceMode.Impulse);
        rb.useGravity = true;
    }
}
