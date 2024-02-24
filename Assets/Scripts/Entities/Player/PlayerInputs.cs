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
    public bool sprint;
    public bool jump;

    void OnLook(InputValue value)
    {
        look = value.Get<Vector2>();
    }
    void OnMove(InputValue value)
    {
        movement = value.Get<Vector2>();
    }
    void OnSprint(InputValue value)
    {
        sprint = value.Get<float>() > 0? true : false;
    }
    void OnJump(InputValue value)
    {
        jump = value.Get<float>() > 0 ? true : false;
    }
}