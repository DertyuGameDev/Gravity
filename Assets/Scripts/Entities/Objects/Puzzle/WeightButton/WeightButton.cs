using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class WeightButton : MonoBehaviour
{
    private WeightButtonObjectDetector _weightButtonObjectDetector;
    private Animator _animator;

    [SerializeField] private float activationWeight;

    private List<GameObject> _objectsOnPiston = new();
    private float _currentWeight;
    private bool _isActivated;

    public UnityEvent<bool> onWeightButtonChangeState;

    private void Awake()
    {
        _weightButtonObjectDetector = GetComponentInChildren<WeightButtonObjectDetector>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _weightButtonObjectDetector.OnObjectEnterPistonArea += WeightButtonObjectDetectorOnOnObjectEnterPistonArea;
        _weightButtonObjectDetector.OnObjectLeavePistonArea += WeightButtonObjectDetectorOnOnObjectLeavePistonArea;
    }

    private void WeightButtonObjectDetectorOnOnObjectEnterPistonArea(GameObject obj)
    {
        _objectsOnPiston.Add(obj);
        UpdateCurrentWeight();
    }

    private void WeightButtonObjectDetectorOnOnObjectLeavePistonArea(GameObject obj)
    {
        _objectsOnPiston.Remove(obj);
        UpdateCurrentWeight();
    }

    private void UpdateCurrentWeight()
    {
        _currentWeight = _objectsOnPiston.Sum(obj =>
        {
            obj.TryGetComponent(out Rigidbody rigidBody);
            return rigidBody ? rigidBody.mass : 0;
        });

        _animator.SetFloat(WeightButtonAnimatorParameters.ActivationPercentage, _currentWeight / activationWeight);

        UpdateActivationState();
    }

    private void UpdateActivationState()
    {
        var newActivationState = _currentWeight >= activationWeight;

        if (newActivationState == _isActivated) return;

        _isActivated = newActivationState;

        Debug.Log($"[WEIGHT BUTTON] - {gameObject} changed state -> {_isActivated}");
        onWeightButtonChangeState?.Invoke(_isActivated);
    }
}

public static class WeightButtonAnimatorParameters
{
    public static readonly int ActivationPercentage = Animator.StringToHash("ActivationPercentage");
}