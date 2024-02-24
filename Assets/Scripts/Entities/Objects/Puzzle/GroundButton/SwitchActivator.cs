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
        OnObjectPlaced?.Invoke(other.gameObject);
        Debug.Log($"{other.gameObject.name} placed on {transform.parent.gameObject.name}");
    }

    private void OnTriggerExit(Collider other)
    {
        OnObjectRemoved?.Invoke(other.gameObject);
        Debug.Log($"{other.gameObject.name} removed from {transform.parent.gameObject.name}");
    }
}