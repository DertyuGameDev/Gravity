using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    private Animator _animator;

    public float cooldown = 2f;
    private float _cooldownTimer;

    public event Action OnButtonTriggered;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _cooldownTimer -= Time.deltaTime;
    }

    public void TriggerButton()
    {
        if (_cooldownTimer > 0) return;

        Debug.Log($"{transform.gameObject.name} pressed!");
        _animator.SetTrigger(ButtonAnimatorParameters.Activate);
        OnButtonTriggered?.Invoke();
        _cooldownTimer = cooldown;
    }
}

public static class ButtonAnimatorParameters
{
    public static readonly int Activate = Animator.StringToHash("Activate");
}