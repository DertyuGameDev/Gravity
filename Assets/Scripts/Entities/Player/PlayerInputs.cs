using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;

public class PlayerInputs : MonoBehaviour
{
    [Header("External References")]
    [SerializeField] PlayerInput input;
    [SerializeField] GravityGunScript gravityGunScript;
    [SerializeField] PlayerInteraction playerInteraction;

    [Header("Flags")]
    public Vector2 movement;
    public Vector2 look;
    public bool sprint;
    public bool jump;
    public Vector2 mouseWheel;
    public bool[] numericKeyPressed;
    public bool leftClick;
    public bool interact;

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
    void OnMouseWheel(InputValue value)
    {
        mouseWheel = value.Get<Vector2>();
        print(mouseWheel);
        if (mouseWheel.y > 0)
            gravityGunScript.IncreaseIndex();
        else if (mouseWheel.y < 0)
            gravityGunScript.DecreaseIndex();
    }
    void OnNumericKeys(InputValue value)
    {
        if (value.Get<Vector2>().y == 1) //1
        {
            gravityGunScript.DefineIndex(0);
        }
        else if (value.Get<Vector2>().y == -1) //2
        {
            gravityGunScript.DefineIndex(1);
        }
        else if (value.Get<Vector2>().x == -1) //3
        {
            gravityGunScript.DefineIndex(2);
        }
    }
    void OnLeftClick(InputValue value)
    {
        leftClick = value.Get<float>() > 0 ? true : false;

        if (leftClick)
            gravityGunScript.Fire();
    }
    void OnInteract(InputValue value)
    {
        interact = value.Get<float>() > 0 ? true : false;

        if (interact)
            playerInteraction.Interact();
    }
}
