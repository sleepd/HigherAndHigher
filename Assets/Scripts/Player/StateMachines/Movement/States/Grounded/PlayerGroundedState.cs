using System;
using UnityEngine;

public class PlayerGroundedState : PlayerMovementState
{
    public PlayerGroundedState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateMachine.PlayerController.InputController.JumpEvent += HandleJumpInput;
        stateMachine.PlayerController.InputController.AimEvent += HandleAim;
    }



    public override void Exit()
    {
        base.Exit();
        stateMachine.PlayerController.InputController.JumpEvent -= HandleJumpInput;
        stateMachine.PlayerController.InputController.AimEvent -= HandleAim;
    }

    public override void Update()
    {
        base.Update();
        if (!stateMachine.PlayerController.IsGrounded)
        {
            stateMachine.ChangeState(stateMachine.FallState);
        }
        stateMachine.PlayerController.velocity.y = -1f;
    }

    public override void HandleInput()
    {
        stateMachine.PlayerController.Animator.SetFloat("Velocity", stateMachine.PlayerController.moveInput.sqrMagnitude);
    }

    public virtual void HandleJumpInput()
    {
        stateMachine.ChangeState(stateMachine.JumpState);
    }
    
    protected void HandleAim()
    {
        stateMachine.ChangeState(stateMachine.AimingState);
    }

}
