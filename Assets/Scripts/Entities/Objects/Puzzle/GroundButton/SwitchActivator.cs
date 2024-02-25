using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchActivator : MonoBehaviour
{
    public event Action<GameObject> OnObjectPlaced;
    public event Action<GameObject> OnObjectRemoved;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"[GROUND BUTTON] {other.gameObject} placed on {transform.parent.gameObject}");
        OnObjectPlaced?.Invoke(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log($"[GROUND BUTTON] {other.gameObject} removed from {transform.parent.gameObject}");
        OnObjectRemoved?.Invoke(other.gameObject);
    }
}