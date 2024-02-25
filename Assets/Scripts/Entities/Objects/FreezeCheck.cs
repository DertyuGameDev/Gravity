using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeCheck : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    bool alreadyFreeze;
    public float timeToDefrost;
    Vector3 previousVel;
    Vector3 previousRot;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void LateUpdate()
    {
        if (timeToDefrost > 0)
        {
            timeToDefrost -= Time.deltaTime;
        }
        else
            Defrost();
    }
    public void FreezeTarget(float amount)
    {
        timeToDefrost = amount;

        if (!alreadyFreeze)
        {
            previousVel = rb.velocity;
            previousRot = rb.angularVelocity;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.freezeRotation = true;
            rb.useGravity = false;
            alreadyFreeze = true;
        }
    }
    void Defrost()
    {
        rb.freezeRotation = false;
        rb.angularVelocity = previousRot;
        rb.velocity = previousVel;
        rb.useGravity = true;
        alreadyFreeze = false;
        enabled = false;
    }
}
