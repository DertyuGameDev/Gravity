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
    [SerializeField] CanvaMain canvaMain;

    [Header("Flags")]
    public Vector2 movement;
    public Vector2 look;
    public bool sprint;
    public bool jump;
    public Vector2 mouseWheel;
    public bool[] numericKeyPressed;
    public Vector3 rotate;
    public bool leftClick;
    public bool interact;


    public static PlayerInputs instance;

    private void Awake()
    {
        instance = this;
    }
    public void OnLook(InputAction.CallbackContext ctx)
    {
        look = ctx.ReadValue<Vector2>();
    }
    public void OnMove(InputAction.CallbackContext ctx)
    {
        movement = ctx.ReadValue<Vector2>();
    }
    public void OnSprint(InputAction.CallbackContext ctx)
    {
        sprint = ctx.ReadValueAsButton();
    }
    public void OnJump(InputAction.CallbackContext ctx)
    {
        jump = ctx.ReadValueAsButton();
    }
    public void OnMouseWheel(InputAction.CallbackContext ctx)
    {
        if (CanvaMain.menuOpen)
            return;

        mouseWheel = ctx.ReadValue<Vector2>();
        print(mouseWheel);
        if (mouseWheel.y > 0)
            gravityGunScript.IncreaseIndex();
        else if (mouseWheel.y < 0)
            gravityGunScript.DecreaseIndex();
    }
    public void OnNumericKeys(InputAction.CallbackContext ctx)
    {
        if (CanvaMain.menuOpen)
            return;
        Vector2 inp = ctx.ReadValue<Vector2>();
        if (inp.y == 1) //1
        {
            gravityGunScript.DefineIndex(0);
        }
        else if (inp.y == -1) //2
        {
            gravityGunScript.DefineIndex(1);
        }
        else if (inp.x == -1) //3
        {
            gravityGunScript.DefineIndex(2);
        }
    }
    public void OnLeftClick(InputAction.CallbackContext ctx)
    {
        leftClick = ctx.ReadValueAsButton();

        if (CanvaMain.menuOpen)
            return;

        if (leftClick)
            gravityGunScript.Fire();
    }
    public void OnInteract(InputAction.CallbackContext ctx)
    {
        interact = ctx.ReadValueAsButton();

        if (CanvaMain.menuOpen)
            return;

        if (interact)
            playerInteraction.Interact();
    }

    public void OnRotate(InputAction.CallbackContext ctx)
    {
        rotate = ctx.ReadValue<Vector3>();
    }
    public void OnEsc(InputAction.CallbackContext ctx)
    {
        bool esc = ctx.ReadValueAsButton();
        if (esc)
            if (!CanvaMain.menuOpen)
                canvaMain.OpenMenu();
            else 
                canvaMain.CloseMenu();
    }
}
