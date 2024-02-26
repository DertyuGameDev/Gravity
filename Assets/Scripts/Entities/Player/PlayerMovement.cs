using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("External References")]
    [SerializeField] PlayerInputs inputs;
    [SerializeField] Rigidbody rb;
    [SerializeField] Animator animator;

    [Header("Move Config")]
    [SerializeField] float walkSpeed;
    [SerializeField] float maxWalkSpeed;

    [SerializeField] float runSpeed;
    [SerializeField] float maxRunSpeed;

    [SerializeField] float decelerateAmount;

    [Header("Check Ground")]
    public bool isGrounded;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float playerHeight;
    [SerializeField] float groundDrag;

    [Header("Jump Config")]
    [SerializeField] float jumpForce;
    [SerializeField] float jumpCooldown;
    [SerializeField] float airMultiplier;
    [SerializeField] float fallMultiplier;
    [SerializeField] bool isReadyToJump;

    Vector3 moveDir;
    
    void FixedUpdate()
    {
        //This help when the V-Sync is desactivated
        float newDelta = 1.0f - (float)System.Math.Pow(0.95, Time.deltaTime * 60.0f);

        //Check Ground
        CheckGround();

        //Limit rigidbody velocity
        LimitVel();

        //Check the current velocity of Rigidbody
        Vector2 moveVel = new Vector2(rb.velocity.x, rb.velocity.z);

        //Movement
        if (inputs.movement != Vector2.zero)
            MovePlayer(newDelta);
        else if (moveVel.magnitude != 0)
            Decelerate();

        //Jump
        if (inputs.jump)
            StartJump();

        //Increase falling speed while the player is falling
        if(!isGrounded)
            FallAmplify(newDelta);
    }
    #region - MOVEMENT -
    /// <summary>
    /// Function that allow player move
    /// </summary>
    /// <param name="delta"></param>
    void MovePlayer(float delta)
    {
        Vector3 horizontal = inputs.movement.x * Camera.main.transform.right;
        Vector3 vertical = inputs.movement.y * Camera.main.transform.forward;

        horizontal.y = 0; 
        vertical.y = 0;

        moveDir = horizontal + vertical;
        moveDir.Normalize();

        moveDir = inputs.sprint? moveDir * runSpeed : moveDir * walkSpeed;

        if (isGrounded)
            rb.AddForce(moveDir * delta,ForceMode.Force);
        else
            rb.AddForce(moveDir * delta * airMultiplier,ForceMode.Force);
    }
    /// <summary>
    /// Set the player's speed limit
    /// </summary>
    void LimitVel()
    {
        Vector3 actualVel = new Vector3(rb.velocity.x, 0, rb.velocity.z);

        float limit = inputs.sprint ? maxRunSpeed : maxWalkSpeed;

        if (actualVel.magnitude > limit)
        {
            Vector3 limitVel = actualVel.normalized * limit;
            rb.velocity = new Vector3(limitVel.x,rb.velocity.y,limitVel.z);
        }
    }
    void Decelerate()
    {
        rb.velocity -= rb.velocity*decelerateAmount;
    }
    #endregion
    #region - CHECK GROUND -
    void CheckGround()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, groundLayer);
    }
    #endregion
    #region - JUMP -
    public void StartJump()
    {
        if (!isReadyToJump || !isGrounded)
            return;

        Jump();
        isReadyToJump = false;
        animator.SetTrigger("Jump");
        Invoke(nameof(ResetJump), jumpCooldown);
    }
    void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    void ResetJump()
    {
        isReadyToJump = true;
        animator.SetTrigger("Landed");
    }
    void FallAmplify(float delta)
    {
        if (rb.velocity.y < 0)
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * delta;
    }
    #endregion
}
