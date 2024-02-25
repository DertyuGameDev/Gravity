using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FreezeMode : MonoBehaviour
{
    [Header("External References")]
    [SerializeField] GravityGunScript gravityGunScript;

    [Header("Config")]
    [SerializeField] GameObject target;
    [SerializeField] float secondsFreeze;
    
    [Header("Audio")]
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip freezeClip;
    public void Fire()
    {
        SelectTarget();
    }
    void SelectTarget()
    {
        if (ObjectDetection.getSelected() == null)
            return;

        audioSource.clip = freezeClip;
        audioSource.Play();
        
        target = ObjectDetection.getSelected().gameObject;

        gravityGunScript.target = target;

        FreezeCheck script = target.GetComponent<FreezeCheck>();
        script.gravityGunScript = gravityGunScript;
        script.enabled = true;
        script.FreezeTarget(secondsFreeze);
    }
    public void ResetValues()
    {
        target = null;
    }
}
