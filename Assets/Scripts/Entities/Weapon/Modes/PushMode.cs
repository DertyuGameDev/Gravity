using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushMode : MonoBehaviour
{
    [Header("External References")]
    [SerializeField] GravityGunScript gravityGunScript;

    [Header("Config")]
    [SerializeField] float force;
    [SerializeField] List<GameObject> finalTargets;
    [SerializeField] Collider[] colTargets;
    [SerializeField] List<GameObject> targetsInView;
    [SerializeField] int limit;

    [Header("Audio")]
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip pushClip;

    private void Update()
    {
        colTargets = Physics.OverlapCapsule(transform.position, transform.position + transform.forward * 10f, 2.8f);
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
        targetsInView.Clear();
        finalTargets.Clear();

        // Debes añadir una capsula como colider para que detecte los posibles objetivos.
        List<Detector> detectors = ObjectDetection.getInView();

        foreach (Detector detector in detectors)
            targetsInView.Add(detector.gameObject);

        colTargets = Physics.OverlapCapsule(transform.position, transform.position + transform.forward * 10f, 1.5f);

        foreach (Collider var in colTargets)
            if (targetsInView.Contains(var.gameObject))
                finalTargets.Add(var.gameObject);




        if (targetsInView.Count > 0)
        {
            audioSource.clip = pushClip;
            audioSource.Play();
        }

       
        foreach (GameObject item in finalTargets)
            Push(item);
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
        finalTargets.Clear();
        targetsInView.Clear();
        colTargets = null;
    }
}