using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushMode : MonoBehaviour
{
    [Header("External References")]
    [SerializeField] GravityGunScript gravityGunScript;

    [Header("Config")]
    [SerializeField] float force;
    [SerializeField] List<Detector> targets;
    [SerializeField] int limit;

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
        gravityGunScript.target = null;
        Push(obj);    
        
    }
    void DetectEntity()
    {
        targets = ObjectDetection.getInView();

        foreach (Detector item in targets)
        {
            if (Mathf.Abs(Vector3.Distance(this.transform.position, item.transform.position)) < limit)
            {
                Push(item.gameObject);
            }
        }
    }
    void Push(GameObject target)
    {
        Rigidbody rb = target.GetComponent<Rigidbody>();
        rb.AddForce(Camera.main.transform.forward * force, ForceMode.Impulse);
        rb.useGravity = true;
    }
    public void ResetValues()
    {
        targets = null;
    }
}
