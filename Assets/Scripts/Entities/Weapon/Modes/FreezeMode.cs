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

        

        FreezeCheck script;
        if (target.TryGetComponent<FreezeCheck>(out script))
        {
            gravityGunScript.target = target;

            script.gravityGunScript = gravityGunScript;
            script.enabled = true;
            script.FreezeTarget(secondsFreeze);
        }
        else
            print("It can't freeze");
    }
    public void ResetValues()
    {
        target = null;
    }
}
