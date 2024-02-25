using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityGunScript1 : MonoBehaviour
{
    [Header("External References")]
    [SerializeField] PlayerInputs input;
    [SerializeField] Animator animator;
    [HideInInspector] public GameObject actualTarget;
    [HideInInspector] public Transform finalPos;
    Vector3 velRef = Vector3.zero;
    [HideInInspector] public float smooth;
    

    [Header("References Modes")]
    [SerializeField] PullMode pullModeScript;
    [SerializeField] PushMode pushModeScript;
    [SerializeField] FreezeMode freezeModeScript;

    [Header("Config")]
    [Tooltip("0 : Pull ; 1 : Push ; 2 : Freeze")]
    [SerializeField] bool[] actualMode;
    public int actualModeIndex;
    [SerializeField] bool readyToUse;
    [SerializeField] float useCooldown;
    [SerializeField] float timeToPreserveTarget;

    private void LateUpdate()
    {
        //This help when the V-Sync is desactivated
        float newDelta = 1.0f - (float)System.Math.Pow(0.95, Time.deltaTime * 60.0f);

        if (actualTarget != null && !pullModeScript.enabled)
            PullTarget(newDelta);
    }


    #region - CHANGE MODE -
    public void DefineIndex(int index)
    {
        for (int i = 0; i < actualMode.Length; i++)
        {
            actualMode[i] = false;
        }
        actualModeIndex = index;
        actualMode[index] = true;
        ActivateModeScript(index);
    }
    public void IncreaseIndex()
    {
        actualMode[actualModeIndex] = false;

        if (actualModeIndex + 1 > 2)
            actualModeIndex = 0;
        else
            actualModeIndex++;

        actualMode[actualModeIndex] = true;

        ActivateModeScript(actualModeIndex);
    }
    public void DecreaseIndex()
    {
        actualMode[actualModeIndex] = false;

        if (actualModeIndex - 1 < 0)
            actualModeIndex = 2;
        else 
            actualModeIndex--;

        actualMode[actualModeIndex] = true;

        ActivateModeScript(actualModeIndex);
    }
    void ActivateModeScript(int index)
    {
        switch (index)
        {
            case 0:
                pullModeScript.enabled = true;
                pushModeScript.enabled = false;
                freezeModeScript.enabled = false;
                break;
            case 1:
                pullModeScript.enabled = false;
                pushModeScript.enabled = true;
                freezeModeScript.enabled = false;
                Invoke(nameof(DeselectTarget), timeToPreserveTarget);
                break;
            case 2:
                pullModeScript.enabled = false;
                pushModeScript.enabled = false;
                freezeModeScript.enabled = true;
                Invoke(nameof(DeselectTarget), timeToPreserveTarget);
                break;
        }
    }
    #endregion
    #region - USE -
    public void Fire()
    {
        if (!readyToUse)
            return;

        switch (actualModeIndex)
        {
            case 0:
                pullModeScript.Fire();
                break;
            case 1:
                pushModeScript.Fire();
                break;
            case 2:
                freezeModeScript.Fire();
                break;
        }

        readyToUse = false;
        Invoke(nameof(ResetUse), useCooldown);
    }
    void ResetUse()
    {
        readyToUse = true;
    }
    #endregion
    #region - TARGET -
    void PullTarget(float delta)
    {
        actualTarget.transform.position = Vector3.SmoothDamp(actualTarget.transform.position, finalPos.transform.position, ref velRef, delta * smooth);
    }
    public void DeselectTarget()
    {
        if (actualTarget != null)
            actualTarget.GetComponent<Rigidbody>().useGravity = true;
        actualTarget = null;
        pullModeScript.target = null;
    }
    #endregion
}
