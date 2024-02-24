using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook : MonoBehaviour
{
    [Header("External References")]
    [SerializeField] PlayerInputs input;

    [Header("Config")]
    [Tooltip("Is the max value")]
    [SerializeField] float maxY;
    [SerializeField] float sensX;
    [SerializeField] float sensY;
    [Tooltip("When this value is greater the camera moves faster")]
    [SerializeField] float smooth;

    float yRotation;
    float xRotation;
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    // Update is called once per frame
    void Update()
    {
        //This help when the V-Sync is desactivated
        float newDelta = 1.0f - (float)System.Math.Pow(0.95, Time.deltaTime * 60.0f);

        Look(newDelta);
    }
    void Look(float delta)
    {
        float horizontalVal = input.look.x * delta * sensX;
        float verticalVal = input.look.y * delta * sensY;

        xRotation -= verticalVal;
        yRotation += horizontalVal;

        xRotation = Mathf.Clamp(xRotation, -maxY, maxY);

        Quaternion finalRot = Quaternion.Euler(xRotation, yRotation, 0f);
        transform.rotation = Quaternion.Lerp(transform.rotation, finalRot, delta * smooth);
    }
}
