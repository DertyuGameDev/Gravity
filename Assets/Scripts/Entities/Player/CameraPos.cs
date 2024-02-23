using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPos : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] Transform cameraPos;
    [SerializeField] [Tooltip("When this value is greater the camera moves faster")] float smooth;
    Vector3 refVel = Vector3.zero;
    private void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, cameraPos.position, ref refVel, Time.deltaTime * smooth);
    }
}
