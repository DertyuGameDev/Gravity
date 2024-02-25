using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempleDoor : MonoBehaviour
{
    private Animator _animator;

    private bool _isOpen;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void SwitchDoorState()
    {
        _isOpen = !_isOpen;
        UpdateAnimatorParameter();
    }

    public void SetDoorState(bool isOpen)
    {
        _isOpen = isOpen;
        UpdateAnimatorParameter();
    }

    private void UpdateAnimatorParameter()
    {
        _animator.SetBool(TempleDoorAnimatorParameters.IsOpen, _isOpen);
    }
}

public static class TempleDoorAnimatorParameters
{
    public static readonly int IsOpen = Animator.StringToHash("isOpen");
}