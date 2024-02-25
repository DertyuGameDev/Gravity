using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeightButtonObjectDetector : MonoBehaviour
{
    public event Action<GameObject> OnObjectEnterPistonArea;
    public event Action<GameObject> OnObjectLeavePistonArea;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Interactable")) return;

        Debug.Log($"[WEIGHT BUTTON] - {other.gameObject} put on {transform.parent.gameObject}");
        OnObjectEnterPistonArea?.Invoke(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag("Interactable")) return;

        Debug.Log($"[WEIGHT BUTTON] - {other.gameObject} removed from {transform.parent.gameObject}");
        OnObjectLeavePistonArea?.Invoke(other.gameObject);
    }
}