using System;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public string message;
    public UnityEvent onInteraction;

    public void Interact()
    {
        onInteraction?.Invoke();
    }
}