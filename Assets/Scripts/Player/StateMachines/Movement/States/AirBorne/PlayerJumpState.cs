using System;
using UnityEngine;

public class PlayerJumpState : PlayerAirBorneState
{
    private float jumpingTime;
    private bool isLowJump;
    public PlayerJumpState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
        
    }

    public override void Enter()
    {
        base.Enter();
        Jump();
        stateMachine.PlayerController.InputController.JumpCanceledEvent += JumpCanceled;
    }

    private void JumpCanceled()
    {
        // high jump or low jump?
        if (jumpingTime < stateMachine.PlayerController.PlayerSettings.JumpInputThreshold)
        {
            isLowJump = true;
        }

    }

    public override void Update()
    {
        base.Update();
        jumpingTime += Time.deltaTime;

        // character will keep jumping till minimum jump time
        if (isLowJump && jumpingTime > stateMachine.PlayerController.PlayerSettings.JumpInputThreshold)
        {
            // add a extra gravity
            stateMachine.PlayerController.velocity.y -= stateMachine.PlayerController.PlayerSettings.Gravity * Time.deltaTime * 2;
        }

        if (stateMachine.PlayerController.velocity.y < 0)
        {
            IsJumping = false;
            stateMachine.ChangeState(stateMachine.FallState);
        }
    }

    private void Jump()
    {
        float jumpForwardForce;
        if (stateMachine.PlayerController.moveInput.magnitude < stateMachine.PlayerController.PlayerSettings.WalkingToRunningThreshold)
        {
            jumpForwardForce = stateMachine.PlayerController.PlayerSettings.JumpForwardForceWalking;
        }
        else
        {
            jumpForwardForce = stateMachine.PlayerController.PlayerSettings.JumpForwardForceRunning;
        }

        jumpingTime = 0;
        Vector3 jumpDirection = stateMachine.PlayerController.GetInputDirection();
        stateMachine.PlayerController.velocity = jumpDirection * jumpForwardForce;
        stateMachine.PlayerController.velocity.y = stateMachine.PlayerController.PlayerSettings.JumpForce;
        stateMachine.PlayerController.Animator.SetTrigger("Jump");
        IsJumping = true;
        isLowJump = false;
    }

    public override void Exit()
    {
        base.Exit();
        stateMachine.PlayerController.InputController.JumpCanceledEvent -= JumpCanceled;
    }
}
