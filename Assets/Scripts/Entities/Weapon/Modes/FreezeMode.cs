using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FreezeMode : MonoBehaviour
{
    [Header("Config")]
    public bool freezeMode;
    [SerializeField] GameObject target;
    [SerializeField] float secondsFreeze;
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

        FreezeCheck script = target.GetComponent<FreezeCheck>();
        script.enabled = true;
        script.FreezeTarget(secondsFreeze);
    }
}
