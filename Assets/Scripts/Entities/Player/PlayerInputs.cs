using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputs : MonoBehaviour
{
    [Header("External References")]
    [SerializeField] PlayerInput input;

    [Header("Flags")]
    public Vector2 movement;
    public Vector2 look;

    void OnLook(InputValue value)
    {
        look = value.Get<Vector2>();
    }
}
