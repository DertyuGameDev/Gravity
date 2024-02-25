using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityGunScript : MonoBehaviour
{
    [Header("External References")]
    [SerializeField] PlayerInputs input;
    [SerializeField] Animator animator;
    public GameObject target;
    [HideInInspector] public Transform finalPos;
    Vector3 velRef = Vector3.zero;
    [HideInInspector] public float smooth;
    [HideInInspector] public Rigidbody targetRB;

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

    [Header("Animation")]
    [SerializeField] float smoothAnim;
    float velX;
    float velY;

    private void LateUpdate()
    {
        //This help when the V-Sync is desactivated
        float newDelta = 1.0f - (float)System.Math.Pow(0.95, Time.deltaTime * 60.0f);

        SetAnimatorVariables();

        if (target != null && !pullModeScript.enabled && !TargetIsFreeze())
            PullTarget(newDelta);
    }


    #region - ANIMATIONS -
    void SetAnimatorVariables()
    {
        animator.SetBool("isMoving",input.movement.magnitude != 0 ? true : false);

        velX = Mathf.Lerp(velX,input.movement.x,Time.deltaTime * smoothAnim);
        velY = Mathf.Lerp(velY,input.movement.y,Time.deltaTime * smoothAnim);
        animator.SetFloat("VelX", velX);
        animator.SetFloat("VelY", velY);
    }
    #endregion
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
        animator.SetTrigger("ChangeMode");
        switch (index)
        {
            case 0:
                pullModeScript.enabled = true;

                pushModeScript.enabled = false;
                pushModeScript.ResetValues();

                freezeModeScript.enabled = false;
                freezeModeScript.ResetValues();
                break;
            case 1:
                pullModeScript.enabled = false;
                pullModeScript.ResetValues();

                pushModeScript.enabled = true;
                if (TargetIsFreeze())
                    target = null;

                freezeModeScript.enabled = false;
                freezeModeScript.ResetValues();

                Invoke(nameof(DeselectTarget), timeToPreserveTarget);
                break;
            case 2:
                pullModeScript.enabled = false;
                pullModeScript.ResetValues();

                pushModeScript.enabled = false;
                pushModeScript.ResetValues();

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

        if (Vector3.Distance(finalPos.transform.position, target.transform.position) > 0.5f)
            targetRB.velocity = (finalPos.transform.position - target.transform.position) * smooth * 2 * delta;
        else
        {
            targetRB.velocity = Vector3.zero;
            target.transform.position = Vector3.SmoothDamp(target.transform.position, finalPos.transform.position, ref velRef, delta * smooth / 10);
        }

        target.transform.rotation = Quaternion.Lerp(target.transform.rotation, finalPos.rotation, delta * smooth / 2);
    }
    public void DeselectTarget()
    {
        if (pullModeScript.enabled)
            return;
        if (TargetIsFreeze())
            return;

        if (target != null)
        {
            target.GetComponent<Rigidbody>().useGravity = true;
        }
            
        target = null;
        pullModeScript.target = null;
    }
    bool TargetIsFreeze()
    {
        if (freezeModeScript.enabled)
        {
            if (target == null)
                return false;

            FreezeCheck freezeCheck;

            if (target.TryGetComponent<FreezeCheck>(out freezeCheck))
                if (freezeCheck.alreadyFreeze)
                    return true;
        }
        return false;
    }
    #endregion
}
