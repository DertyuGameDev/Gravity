using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class GroundButton : MonoBehaviour
{
    private Animator _animator;
    private SwitchActivator _switchActivator;

    private readonly List<GameObject> _objectsOnButton = new();

    public UnityEvent<bool> onButtonChangeState;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _switchActivator = GetComponentInChildren<SwitchActivator>();
    }

    private void Start()
    {
        _switchActivator.OnObjectPlaced += SwitchActivatorOnOnObjectPlaced;
        _switchActivator.OnObjectRemoved += SwitchActivatorOnOnObjectRemoved;
    }

    private void SwitchActivatorOnOnObjectPlaced(GameObject obj)
    {
        AddObject(obj);
    }

    private void SwitchActivatorOnOnObjectRemoved(GameObject obj)
    {
        RemoveObject(obj);
    }

    private void AddObject(GameObject obj)
    {
        _objectsOnButton.Add(obj);

        if (_objectsOnButton.Count == 1)
        {
            SetActivation(true);
        }
    }

    private void RemoveObject(GameObject obj)
    {
        _objectsOnButton.Remove(obj);

        if (_objectsOnButton.Count == 0)
        {
            SetActivation(false);
        }
    }

    private void SetActivation(bool activation)
    {
        Debug.Log($"Button activation: {activation}");
        onButtonChangeState?.Invoke(activation);
        _animator.SetBool(GroundButtonAnimatorParameters.IsActivated, activation);
    }
}

public static class GroundButtonAnimatorParameters
{
    public static readonly int IsActivated = Animator.StringToHash("isActivated");
}