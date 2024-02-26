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

    [Header("Audio")]
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip pushClip;

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

        if (targets.Count > 0)
        {
            audioSource.clip = pushClip;
            audioSource.Play();
        }

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
        Rigidbody playerRB = GameManager.player.transform.GetChild(0).GetComponent<Rigidbody>();
        
        FreezeCheck freezeCheck;
        
        if (target.TryGetComponent<FreezeCheck>(out freezeCheck))
        {
            if (freezeCheck.alreadyFreeze)
                freezeCheck.Defrost();
        }

        rb.velocity = Vector3.zero;
        rb.AddForce(Camera.main.transform.forward * force, ForceMode.Impulse);
        rb.useGravity = true;

        playerRB.AddForce(-Camera.main.transform.forward * 2, ForceMode.Impulse);
    }

    public void ResetValues()
    {
        targets = null;
    }
}