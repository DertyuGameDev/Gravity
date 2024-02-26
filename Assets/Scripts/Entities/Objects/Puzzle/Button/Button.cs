using UnityEngine;
using UnityEngine.Events;

public class Button : MonoBehaviour
{
    private Animator _animator;

    public float cooldown = 2f;
    private float _cooldownTimer;

    public UnityEvent onButtonTriggered;

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

        Debug.Log($"[SIMPLE BUTTON] {gameObject} pressed!");
        _animator.SetTrigger(ButtonAnimatorParameters.Activate);
        onButtonTriggered?.Invoke();
        _cooldownTimer = cooldown;
    }
}

public static class ButtonAnimatorParameters
{
    public static readonly int Activate = Animator.StringToHash("Activate");
}